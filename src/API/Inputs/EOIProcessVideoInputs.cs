using System;
using System.Text.Json.Serialization;
using EyesOnItSDK.Data.Elements;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIProcessVideosInputs : EOIBaseVideoInputs
    {
        [JsonPropertyName("input_video_path_list")]
        public string[] InputVideoFiles { get; set; }

        [JsonPropertyName("output_video_path")]
        public string OutputVideoFile { get; set; }

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



        public EOIProcessVideosInputs() : base()
        {
            this.InputVideoFiles = new string[] { };
            this.OutputVideoFile = null;
        }

        public EOIProcessVideosInputs(
            string[] videoFiles,
            string outputFile,
            EOIRegion[] regions,
            int frame_rate,
            EOIEffects effects) : base()
        {
            this.Regions = regions;
            this.InputVideoFiles = videoFiles;
            this.OutputVideoFile= outputFile;
            this.FrameRate = frame_rate;
            this.Effects = effects;
            this.IndexForSearch = false;
            this.SearchIndexTypes = new string[] { };
        }
    }
}
