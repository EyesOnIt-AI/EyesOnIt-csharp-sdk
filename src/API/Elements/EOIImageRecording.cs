using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Elements
{
    public class EOIImageRecording
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("record_full_frame")]
        public bool RecordFullFrame { get; set; }

        [JsonPropertyName("record_object_bounds")]
        public bool RecordObjectBounds { get; set; }

        [JsonPropertyName("record_all_frames")]
        public bool RecordAllFrames { get; set; }

        [JsonPropertyName("frame_record_interval")]
        public int FrameRecordInterval { get; set; }
    }
}
