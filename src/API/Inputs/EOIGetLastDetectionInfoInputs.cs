using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIGetLastDetectionInfoInputs
    {
        [JsonPropertyName("stream_id")]
        public string StreamId { get; set; }

        public EOIGetLastDetectionInfoInputs(string streamId)
        {
            StreamId = streamId;
        }
    }
}
