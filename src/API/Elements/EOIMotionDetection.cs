using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIMotionDetection
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("regular_check_frame_interval")]
        public int RegularCheckFrameInterval { get; set; }

        [JsonPropertyName("backup_check_frame_interval")]
        public float? BackupCheckFrameInterval { get; set; }

        [JsonPropertyName("detection_threshold")]
        public int DetectionThreshold { get; set; }

        public EOIMotionDetection()
        { 
        }

        public static EOIMotionDetection NoMotionDetection()
        {
            return new EOIMotionDetection() 
            { 
                Enabled = false,
                RegularCheckFrameInterval = 1,
                BackupCheckFrameInterval = null,
                DetectionThreshold = 300 
            };
        }
    }
}
