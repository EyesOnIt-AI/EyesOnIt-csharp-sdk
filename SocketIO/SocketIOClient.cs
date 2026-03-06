// SocketClient.cs
using Serilog;
using SocketIOClient;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EyesOnItSDK.SocketIO
{
    public class SocketClient
    {
        private readonly string url;
        private readonly SocketIOOptions options;
        private readonly object syncRoot = new object();
        private static int systemTextJsonResolverInitialized;
        private SocketIOClient.SocketIO socket;
        private bool handlersRegistered;

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
        //public event CountUpdateHandler OnCountUpdate;
        public event SubscribedHandler OnSubscribed;
        public event ConnectedHandler OnConnected;

        public SocketClient(string url, SocketIOOptions options = null)
        {
            this.url = url;
            this.options = options ?? new SocketIOOptions();
        }

        public async Task ConnectAsync()
        {
            EnsureSystemTextJsonAssemblyResolver();

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

            var currentSocket = EnsureConnectedSocket();
            await currentSocket.EmitAsync("subscribe", room).ConfigureAwait(false);
        }

        public async Task LeaveRoomAsync(string room)
        {
            if (string.IsNullOrWhiteSpace(room))
            {
                return;
            }

            var currentSocket = EnsureConnectedSocket();
            await currentSocket.EmitAsync("unsubscribe", room).ConfigureAwait(false);
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

        private static void EnsureSystemTextJsonAssemblyResolver()
        {
            if (Interlocked.Exchange(ref systemTextJsonResolverInitialized, 1) == 1)
            {
                return;
            }

            AppDomain.CurrentDomain.AssemblyResolve += ResolveSystemTextJsonAssembly;
        }

        private static Assembly ResolveSystemTextJsonAssembly(object sender, ResolveEventArgs args)
        {
            AssemblyName requestedAssemblyName;
            try
            {
                requestedAssemblyName = new AssemblyName(args.Name);
            }
            catch
            {
                return null;
            }

            if (!string.Equals(requestedAssemblyName.Name, "System.Text.Json", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var alreadyLoaded = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(assembly =>
                {
                    try
                    {
                        return string.Equals(assembly.GetName().Name, "System.Text.Json", StringComparison.OrdinalIgnoreCase);
                    }
                    catch
                    {
                        return false;
                    }
                });
            if (alreadyLoaded != null)
            {
                return alreadyLoaded;
            }

            var sdkDirectory = Path.GetDirectoryName(typeof(SocketClient).Assembly.Location);
            var candidatePaths = new[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "System.Text.Json.dll"),
                string.IsNullOrWhiteSpace(sdkDirectory) ? null : Path.Combine(sdkDirectory, "System.Text.Json.dll")
            }
            .Where(path => !string.IsNullOrWhiteSpace(path))
            .Distinct(StringComparer.OrdinalIgnoreCase);

            foreach (var candidatePath in candidatePaths)
            {
                try
                {
                    if (!File.Exists(candidatePath))
                    {
                        continue;
                    }

                    var candidateAssemblyName = AssemblyName.GetAssemblyName(candidatePath);
                    if (!string.Equals(candidateAssemblyName.Name, "System.Text.Json", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    return Assembly.LoadFrom(candidatePath);
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "SocketIOClient: Failed loading System.Text.Json from {CandidatePath}", candidatePath);
                }
            }

            return null;
        }

        private void RegisterLifecycleHandlers()
        {
            socket.OnConnected += (sender, args) =>
            {
                Log.Debug("SocketIOClient: Connected to server");

                try
                {
                    OnConnected?.Invoke();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "SocketIOClient: OnConnected handler failed");
                }
            };

            socket.OnError += (sender, message) => Log.Error($"SocketIOClient: Error: {message}");
            socket.OnDisconnected += (sender, reason) => Log.Warning($"SocketIOClient: Disconnected: {reason}");
            socket.OnAny((eventName, response) => Log.Debug($"SocketIOClient: Event: {eventName}, Data: {response}"));
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

            //socket.On("count_update", response =>
            //{
            //    Log.Debug($"SocketIOClient: count_update message received");

            //    JsonElement jsonElement = response.GetValue<JsonElement>(0);
            //    OnCountUpdate?.Invoke(jsonElement);
            //});

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
        }
    }
}
