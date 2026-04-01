using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIVideoFrameInputs
    {
        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        public EOIVideoFrameInputs(string streamUrl)
        {
            StreamUrl = streamUrl;
        }
    }
}
