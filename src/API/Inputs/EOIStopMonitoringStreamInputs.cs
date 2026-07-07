using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIStopMonitoringStreamInputs
    {
        [JsonPropertyName("stream_id")]
        public string StreamId { get; set; }

        public EOIStopMonitoringStreamInputs(string streamId)
        {
            StreamId = streamId;
        }
    }
}
