// SocketClient.cs
using Serilog;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EyesOnItSDK.SocketIO
{
    public class SocketClient
    {
        private readonly string url;
        private readonly SocketIOOptions options;
        private readonly object syncRoot = new object();
        private readonly object joinedRoomsSyncRoot = new object();
        private readonly HashSet<string> joinedRooms = new HashSet<string>(StringComparer.Ordinal);
        private SocketIOClient.SocketIO socket;
        private bool handlersRegistered;

        public delegate void StreamUpdateHandler(StreamUpdateData[] payload);
        public delegate void StreamDetectionHandler(StreamDetectionsData payload);
        public delegate void PerformanceUpdateHandler(PerformanceUpdateDataWrapper payload);
        public delegate void CountUpdateHandler(CountUpdateDataWrapper payload);
        public delegate void VideoProcessingUpdateHandler(VideoProcessingUpdateData[] payload);
        public delegate void LiveSearchUpdateHandler(LiveSearchUpdateData[] payload);
        public delegate void LiveSearchDetectionHandler(StreamDetectionsData payload);
        public delegate void SubscribedHandler(SubscribedData payload);
        public delegate void UnsubscribedHandler(SubscribedData payload);
        public delegate void ConnectedHandler();
        public delegate void DisconnectedHandler(string reason);
        public delegate void ReconnectedHandler(int attempts);
        public delegate void TransportErrorHandler(string message);

        public event StreamUpdateHandler OnStreamUpdate;
        public event StreamDetectionHandler OnStreamDetection;
        public event PerformanceUpdateHandler OnPerformanceUpdate;
        public event CountUpdateHandler OnCountUpdate;
        public event VideoProcessingUpdateHandler OnVideoProcessingUpdate;
        public event LiveSearchUpdateHandler OnLiveSearchUpdate;
        public event LiveSearchDetectionHandler OnLiveSearchDetection;
        public event SubscribedHandler OnSubscribed;
        public event UnsubscribedHandler OnUnsubscribed;
        public event ConnectedHandler OnConnected;
        public event DisconnectedHandler OnDisconnected;
        public event ReconnectedHandler OnReconnected;
        public event TransportErrorHandler OnTransportError;

        public SocketClient(string url, SocketIOOptions options = null)
        {
            this.url = url;
            this.options = options ?? new SocketIOOptions();
        }

        public async Task ConnectAsync()
        {
            SocketIOClient.SocketIO currentSocket;
            lock (syncRoot)
            {
                if (socket == null)
                {
                    socket = new SocketIOClient.SocketIO(url, options);
                }

                if (!handlersRegistered)
                {
                    RegisterEventHandlers();
                    RegisterLifecycleHandlers();
                    handlersRegistered = true;
                }

                currentSocket = socket;
            }

            if (!currentSocket.Connected)
            {
                await currentSocket.ConnectAsync().ConfigureAwait(false);
            }
        }

        public async Task DisconnectAsync()
        {
            Log.Debug("SocketIOClient: Disconnected from server");

            var currentSocket = socket;
            if (currentSocket != null && currentSocket.Connected)
            {
                await currentSocket.DisconnectAsync().ConfigureAwait(false);
            }
        }

        public async Task JoinRoomAsync(string room)
        {
            if (string.IsNullOrWhiteSpace(room))
            {
                return;
            }

            RememberJoinedRoom(room);

            var currentSocket = socket;
            if (currentSocket == null || !currentSocket.Connected)
            {
                return;
            }

            await currentSocket.EmitAsync("subscribe", new { room }).ConfigureAwait(false);
        }

        public async Task JoinRoomsAsync(IEnumerable<string> rooms)
        {
            if (rooms == null)
            {
                return;
            }

            foreach (var room in rooms)
            {
                await JoinRoomAsync(room).ConfigureAwait(false);
            }
        }

        public async Task<string> JoinLiveSearchDetectionsAsync(int searchId)
        {
            var room = EOISocketRooms.LiveSearchDetectionsForSearch(searchId);
            await JoinRoomAsync(room).ConfigureAwait(false);
            return room;
        }

        public async Task LeaveRoomAsync(string room)
        {
            if (string.IsNullOrWhiteSpace(room))
            {
                return;
            }

            ForgetJoinedRoom(room);

            var currentSocket = socket;
            if (currentSocket == null || !currentSocket.Connected)
            {
                return;
            }

            await currentSocket.EmitAsync("unsubscribe", new { room }).ConfigureAwait(false);
        }

        public async Task EmitAsync(string eventName, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(eventName))
            {
                return;
            }

            var currentSocket = EnsureConnectedSocket();
            await currentSocket.EmitAsync(eventName, args).ConfigureAwait(false);
        }

        public bool IsConnected()
        {
            return socket?.Connected ?? false;
        }

        private SocketIOClient.SocketIO EnsureConnectedSocket()
        {
            if (socket == null || !socket.Connected)
            {
                throw new InvalidOperationException("Socket.IO client is not connected.");
            }

            return socket;
        }

        private void RegisterLifecycleHandlers()
        {
            socket.OnConnected += async (sender, args) =>
            {
                Log.Debug("SocketIOClient: Connected to server");

                try
                {
                    await RejoinRoomsAsync().ConfigureAwait(false);
                    OnConnected?.Invoke();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: OnConnected handler failed");
                }
            };

            socket.OnError += (sender, message) =>
            {
                Log.Error($"SocketIOClient: Error: {message}");

                try
                {
                    OnTransportError?.Invoke(message);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: OnTransportError handler failed");
                }
            };

            socket.OnDisconnected += (sender, reason) =>
            {
                Log.Warning($"SocketIOClient: Disconnected: {reason}");

                try
                {
                    OnDisconnected?.Invoke(reason);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: OnDisconnected handler failed");
                }
            };

            socket.OnReconnected += (sender, attempts) =>
            {
                Log.Information($"SocketIOClient: Reconnected after {attempts} attempt(s)");

                try
                {
                    OnReconnected?.Invoke(attempts);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: OnReconnected handler failed");
                }
            };

            socket.OnAny((eventName, response) => Log.Debug($"SocketIOClient: Event: {eventName}, Data: {response}"));
        }

        private void RememberJoinedRoom(string room)
        {
            lock (joinedRoomsSyncRoot)
            {
                joinedRooms.Add(room);
            }
        }

        private void ForgetJoinedRoom(string room)
        {
            lock (joinedRoomsSyncRoot)
            {
                joinedRooms.Remove(room);
            }
        }

        private async Task RejoinRoomsAsync()
        {
            SocketIOClient.SocketIO currentSocket;
            string[] rooms;

            lock (joinedRoomsSyncRoot)
            {
                currentSocket = socket;
                rooms = joinedRooms.ToArray();
            }

            if (currentSocket == null || !currentSocket.Connected || rooms.Length == 0)
            {
                return;
            }

            foreach (var room in rooms)
            {
                await currentSocket.EmitAsync("subscribe", new { room }).ConfigureAwait(false);
            }
        }

        private void RegisterEventHandlers()
        {
            socket.On("stream_update", response =>
            {
                try
                {
                    Log.Debug("SocketIOClient: stream_update message received");

                    StreamUpdateData[] payload = response.GetValue<StreamUpdateData[]>(0);

                    OnStreamUpdate?.Invoke(payload);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: Failed to handle stream_update");
                }
            });

            socket.On("stream_detection", response =>
            {
                try
                {
                    Log.Debug("SocketIOClient: stream_detection message received");

                    StreamDetectionsData payload = response.GetValue<StreamDetectionsData>(0);

                    OnStreamDetection?.Invoke(payload);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: Failed to handle stream_detection");
                }
            });

            socket.On("performance_update", response =>
            {
                try
                {
                    Log.Debug("SocketIOClient: performance_update message received");

                    PerformanceUpdateDataWrapper payload = response.GetValue<PerformanceUpdateDataWrapper>(0);

                    OnPerformanceUpdate?.Invoke(payload);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: Failed to handle performance_update");
                }
            });

            socket.On("live_search_update", response =>
            {
                try
                {
                    Log.Debug("SocketIOClient: live_search_update message received");

                    LiveSearchUpdateData[] payload = response.GetValue<LiveSearchUpdateData[]>(0);

                    OnLiveSearchUpdate?.Invoke(payload);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: Failed to handle live_search_update");
                }
            });

            socket.On("count_update", response =>
            {
                try
                {
                    Log.Debug("SocketIOClient: count_update message received");

                    CountUpdateDataWrapper payload = response.GetValue<CountUpdateDataWrapper>(0);

                    OnCountUpdate?.Invoke(payload);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: Failed to handle count_update");
                }
            });

            socket.On("video_processing_update", response =>
            {
                try
                {
                    Log.Debug("SocketIOClient: video_processing_update message received");

                    VideoProcessingUpdateData[] payload = response.GetValue<VideoProcessingUpdateData[]>(0);

                    OnVideoProcessingUpdate?.Invoke(payload);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: Failed to handle video_processing_update");
                }
            });

            socket.On("live_search_detection", response =>
            {
                try
                {
                    Log.Debug("SocketIOClient: live_search_detection message received");

                    StreamDetectionsData payload = response.GetValue<StreamDetectionsData>(0);

                    OnLiveSearchDetection?.Invoke(payload);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: Failed to handle live_search_detection");
                }
            });

            socket.On("subscribed", response =>
            {
                try
                {
                    Log.Debug("SocketIOClient: subscribed message received");

                    SubscribedData payload = response.GetValue<SubscribedData>(0);

                    OnSubscribed?.Invoke(payload);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: Failed to handle subscribed");
                }
            });

            socket.On("unsubscribed", response =>
            {
                try
                {
                    Log.Debug("SocketIOClient: unsubscribed message received");

                    SubscribedData payload = response.GetValue<SubscribedData>(0);

                    OnUnsubscribed?.Invoke(payload);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: Failed to handle unsubscribed");
                }
            });
        }
    }
}
