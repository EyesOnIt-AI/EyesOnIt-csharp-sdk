// SocketClient.cs
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class SubscribedData
    {
        [JsonPropertyName("room")] 
        public string Room { get; set; }
    }

    public class SubscriptionErrorData : SubscribedData
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
