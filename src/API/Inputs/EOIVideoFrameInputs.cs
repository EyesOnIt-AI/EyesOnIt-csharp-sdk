using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIGetVideoFrameInputs
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        public EOIGetVideoFrameInputs(string streamUrl) {
            StreamUrl = streamUrl;
        }
    }
}
