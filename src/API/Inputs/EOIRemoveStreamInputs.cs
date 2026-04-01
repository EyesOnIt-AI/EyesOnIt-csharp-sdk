using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIRemoveStreamInputs
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        public EOIRemoveStreamInputs(string streamUrl)
        {
            StreamUrl = streamUrl;
        }
    }
}
