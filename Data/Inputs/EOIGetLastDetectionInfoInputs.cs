using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Inputs
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
