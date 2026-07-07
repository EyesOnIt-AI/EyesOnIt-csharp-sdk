using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIRemoveStreamInputs
    {
        [JsonPropertyName("stream_id")]
        public string StreamId { get; set; }

        public EOIRemoveStreamInputs(string streamId)
        {
            StreamId = streamId;
        }
    }
}
