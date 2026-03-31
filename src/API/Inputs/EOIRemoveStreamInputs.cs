using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
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
