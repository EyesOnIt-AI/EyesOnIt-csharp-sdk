using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Elements
{
    public class EOIMotionDetection
    {
        [JsonPropertyName("periodic_check_enabled")]
        public bool PeriodicCheckEnabled { get; set; }

        [JsonPropertyName("periodic_check_seconds")]
        public float PeriodicCheckSeconds { get; set; }

        [JsonPropertyName("motion_detection_enabled")]
        public bool MotionDetectionEnabled { get; set; }

        [JsonPropertyName("motion_detection_seconds")]
        public float MotionDetectionSeconds { get; set; }

        [JsonPropertyName("motion_detection_threshold")]
        public int MotionDetectionThreshold { get; set; }

        public EOIMotionDetection()
        { 
        }

        public static EOIMotionDetection NoMotionDetection()
        {
            return new EOIMotionDetection() 
            { 
                PeriodicCheckEnabled = false, 
                MotionDetectionEnabled = false, 
                MotionDetectionSeconds = 1,
                MotionDetectionThreshold = 100,
                PeriodicCheckSeconds = 1 
            };
        }
    }
}
