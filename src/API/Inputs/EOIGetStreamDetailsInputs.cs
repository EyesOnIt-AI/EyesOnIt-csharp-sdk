using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
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
