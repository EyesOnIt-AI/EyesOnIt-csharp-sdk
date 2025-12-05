// SocketClient.cs
using Serilog;
using SocketIOClient;
using System.Text.Json;
using System.Threading.Tasks;

namespace EyesOnItSDK.SocketIO
{
    public class SocketClient
    {
        private readonly string url;
        private readonly SocketIOOptions options;
        private SocketIOClient.SocketIO socket;

        public delegate void StreamUpdateHandler(StreamUpdateData[] payload);
        public delegate void StreamDetectionHandler(StreamDetectionsData payload);
        public delegate void PerformanceUpdateHandler(PerformanceUpdateDataWrapper payload);
        public delegate void LiveSearchDetectionHandler(StreamDetectionsData payload);
        public delegate void CountUpdateHandler(object payload);
        public delegate void SubscribedHandler(SubscribedData payload);
        public delegate void ConnectedHandler();

        public event StreamUpdateHandler OnStreamUpdate;
        public event StreamDetectionHandler OnStreamDetection;
        public event PerformanceUpdateHandler OnPerformanceUpdate;
        public event LiveSearchDetectionHandler OnLiveSearchDetection;
        public event CountUpdateHandler OnCountUpdate;
        public event SubscribedHandler OnSubscribed;
        public event ConnectedHandler OnConnected;

        public SocketClient(string url, SocketIOOptions options = null)
        {
            this.url = url;
            this.options = options ?? new SocketIOOptions();
        }

        public async Task ConnectAsync()
        {
            if (socket == null)
            {
                socket = new SocketIOClient.SocketIO(url, options);
                RegisterEventHandlers();
            }

            socket.OnConnected += (sender, args) =>
            {
                Log.Debug($"SocketIOClient: Connected to server");
                OnConnected?.Invoke();
            };

            socket.OnError += (sender, message) => Log.Error($"SocketIOClient: Error: {message}");
            socket.OnDisconnected += (sender, reason) => Log.Warning($"SocketIOClient: Disconnected: {reason}");

            socket.OnAny((eventName, response) =>
            {
                Log.Debug($"SocketIOClient: Event: {eventName}, Data: {response}");
            });

            if (!socket.Connected)
            {
                await socket.ConnectAsync();
            }
        }

        public async Task DisconnectAsync()
        {
            Log.Debug($"SocketIOClient: Disconnected from server");

            if (socket != null && socket.Connected)
            {
                await socket.DisconnectAsync();
            }
        }

        public async Task JoinRoomAsync(string room)
        {
            await socket.EmitAsync("subscribe", room);
        }

        public async Task LeaveRoomAsync(string room)
        {
            await socket.EmitAsync("unsubscribe", room);
        }

        public async Task EmitAsync(string eventName, params object[] args)
        {
            await socket.EmitAsync(eventName, args);
        }

        public bool IsConnected()
        {
            return socket?.Connected ?? false;
        }

        private void RegisterEventHandlers()
        {
            socket.On("stream_update", response =>
            {
                Log.Debug($"SocketIOClient: stream_update message received");

                JsonElement jsonElement = response.GetValue<JsonElement>(0);
                StreamUpdateData[] payload = JsonSerializer.Deserialize<StreamUpdateData[]>(jsonElement);

                OnStreamUpdate?.Invoke(payload);
            });

            socket.On("stream_detection", response =>
            {
                Log.Debug($"SocketIOClient: stream_detection message received");

                JsonElement jsonElement = response.GetValue<JsonElement>(0);
                StreamDetectionsData payload = JsonSerializer.Deserialize<StreamDetectionsData>(jsonElement);

                OnStreamDetection?.Invoke(payload);
            });

            socket.On("performance_update", response =>
            {
                Log.Debug($"SocketIOClient: performance_update message received");

                JsonElement jsonElement = response.GetValue<JsonElement>(0);
                PerformanceUpdateDataWrapper payload = JsonSerializer.Deserialize<PerformanceUpdateDataWrapper>(jsonElement);

                OnPerformanceUpdate?.Invoke(payload);
            });

            socket.On("live_search_detection", response =>
            {
                Log.Debug($"SocketIOClient: live_search_detection message received");

                JsonElement jsonElement = response.GetValue<JsonElement>(0);
                StreamDetectionsData payload = JsonSerializer.Deserialize<StreamDetectionsData>(jsonElement);

                OnLiveSearchDetection?.Invoke(payload);
            });

            //socket.On("count_update", response =>
            //{
            //    var payload = response.GetValue<string>(1).ToString();
            //    OnCountUpdate?.Invoke(room, payload);
            //});

            socket.On("subscribed", response =>
            {
                Log.Debug($"SocketIOClient: subscribed message received");

                JsonElement jsonElement = response.GetValue<JsonElement>(0);
                SubscribedData payload = JsonSerializer.Deserialize<SubscribedData>(jsonElement);

                OnSubscribed?.Invoke(payload);
            });
        }
    }
}
