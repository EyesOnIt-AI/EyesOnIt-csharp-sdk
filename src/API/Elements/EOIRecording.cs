using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIRecording
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("record_with_alert")]
        public bool RecordWithAlert { get; set; }

        [JsonPropertyName("record_with_detection")]
        public bool RecordWithDetection { get; set; }

        [JsonPropertyName("record_with_motion")]
        public bool RecordWithMotion { get; set; }

        [JsonPropertyName("record_combined_confidence_threshold")]
        public int? RecordCombinedConfidenceThreshold { get; set; }

        [JsonPropertyName("save_detection_data")]
        public bool SaveDetectionData { get; set; }

        [JsonPropertyName("save_original_copy")]
        public bool SaveOriginalCopy { get; set; }

        [JsonPropertyName("recording_folder")]
        public string RecordingFolder { get; set; }

        [JsonPropertyName("output_file_name")]
        public string OutputFileName { get; set; }

        [JsonPropertyName("include_stream_name")]
        public string IncludeStreamName { get; set; }

        [JsonPropertyName("video_recording")]
        public EOIVideoRecording VideoRecording { get; set; }

        [JsonPropertyName("image_recording")]
        public EOIImageRecording ImageRecording { get; set; }

    }
}
