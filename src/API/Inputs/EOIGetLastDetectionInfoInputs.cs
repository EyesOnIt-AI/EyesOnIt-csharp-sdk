using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIGetLastDetectionInfoInputs
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        public EOIGetLastDetectionInfoInputs(string streamUrl)
        {
            StreamUrl = streamUrl;
        }
    }
}
