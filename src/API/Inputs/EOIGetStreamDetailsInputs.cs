using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIGetStreamDetailsInputs
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        public EOIGetStreamDetailsInputs(string streamUrl)
        {
            StreamUrl = streamUrl;
        }
    }
}
