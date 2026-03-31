using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIVideoRecording
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("record_all_frames")]
        public bool RecordAllFrames { get; set; }
    }
}
