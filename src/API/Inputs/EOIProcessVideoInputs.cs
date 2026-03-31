using System;
using System.Text.Json.Serialization;
using EyesOnItSDK.API.Elements;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIProcessVideoInputs : EOIBaseVideoInputs
    {
        [JsonPropertyName("input_video_path")]
        public string InputVideoPath { get; set; }

        [JsonPropertyName("rotate_video")]
        public int RotateVideo { get; set; }

        [JsonPropertyName("output_video_path")]
        public string OutputVideoPath { get; set; }

        [JsonPropertyName("real_time")]
        public bool RealTime { get; set; }

        [JsonPropertyName("video_start_interval")]
        public int VideoStartInterval { get; set; }

        [JsonPropertyName("output_all_frames")]
        public bool OutputAllFrames { get; set; }

        [JsonPropertyName("start_seconds")]
        public int? StartSeconds { get; set; }

        [JsonPropertyName("end_seconds")]
        public int? EndSeconds { get; set; }

        [JsonPropertyName("video_start_local_time")]
        public string VideoStartLocalTime { get; set; }

        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("base_image_path")]
        public string BaseImagePath { get; set; }

        [JsonPropertyName("plugins")]
        public object Plugins { get; set; }

        [JsonPropertyName("validation")]
        public EOIValidation Validation { get; set; }

        public EOIProcessVideoInputs() : base()
        {
            InputVideoPath = null;
            OutputVideoPath = null;
            Mode = "KNOWN_OBJECT_DETECTION";
        }

        public EOIProcessVideoInputs(
            string inputVideoPath,
            string outputVideoPath,
            EOIRegion[] regions,
            int frameRate,
            EOIEffects effects) : base()
        {
            Regions = regions;
            InputVideoPath = inputVideoPath;
            OutputVideoPath = outputVideoPath;
            FrameRate = frameRate;
            Effects = effects;
            IndexForSearch = false;
            SearchIndexTypes = Array.Empty<string>();
            Mode = "KNOWN_OBJECT_DETECTION";
        }
    }
}
