using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIRecording
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("save_with_alert")]
        public bool SaveWithAlert { get; set; }

        [JsonPropertyName("save_with_detection")]
        public bool SaveWithDetection { get; set; }

        [JsonPropertyName("save_with_motion")]
        public bool SaveWithMotion { get; set; }

        [JsonPropertyName("save_original_copy")]
        public bool SaveOriginalCopy { get; set; }

        [JsonPropertyName("recording_folder")]
        public string RecordingFolder { get; set; }

    }
}
