using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIStopMonitoringStreamInputs
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        public EOIStopMonitoringStreamInputs(string streamUrl)
        {
            StreamUrl = streamUrl;
        }
    }
}
