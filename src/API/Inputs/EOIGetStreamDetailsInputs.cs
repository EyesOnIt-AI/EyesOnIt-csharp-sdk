using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIGetStreamDetailsInputs
    {
        [JsonPropertyName("schema_version")]
        public string SchemaVersion { get; set; } = EOIAddStreamInputs.CurrentSchemaVersion;

        [JsonPropertyName("stream_id")]
        public string StreamId { get; set; }

        public EOIGetStreamDetailsInputs(string streamId)
        {
            SchemaVersion = EOIAddStreamInputs.CurrentSchemaVersion;
            StreamId = streamId;
        }
    }
}
