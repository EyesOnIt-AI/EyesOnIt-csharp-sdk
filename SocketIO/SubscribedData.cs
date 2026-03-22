// SocketClient.cs
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class SubscribedData
    {
        [JsonProperty("room")]
        [JsonPropertyName("room")] 
        public string Room { get; set; }
    }
}
