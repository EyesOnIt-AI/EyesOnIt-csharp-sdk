// SocketClient.cs
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class SubscribedData
    {
        [JsonPropertyName("room")] 
        public string Room { get; set; }
    }
}
