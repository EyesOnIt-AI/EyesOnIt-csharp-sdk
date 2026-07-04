using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIGetStreamDetailsInputs
    {
        [JsonPropertyName("schema_version")]
        public string SchemaVersion { get; set; } = EOIAddStreamInputs.CurrentSchemaVersion;

        [JsonPropertyName("stream_url")]
        public string StreamUrl { get; set; }

        public EOIGetStreamDetailsInputs(string streamUrl)
        {
            SchemaVersion = EOIAddStreamInputs.CurrentSchemaVersion;
            StreamUrl = streamUrl;
        }
    }
}
