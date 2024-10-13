using System.Text.Json.Serialization;
using EyesOnItSDK.Data.Elements;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIProcessVideosInputs : EOIBaseInputs
    {
        [JsonPropertyName("lines")]
        public EOILine[] Lines { get; set; }

        [JsonPropertyName("input_video_path_list")]
        public string[] InputVideoFiles { get; set; }

        [JsonPropertyName("output_video_path")]
        public string OutputVideoFile { get; set; }

        [JsonPropertyName("synchronous")]
        public bool Synchronous { get; set; }

        [JsonPropertyName("real_time")]
        public bool RealTime { get; set; }

        [JsonPropertyName("output_all_frames")]
        public bool OutputAllFrames { get; set; }

        [JsonPropertyName("start_seconds")]
        public int? StartSeconds { get; set; }

        [JsonPropertyName("end_seconds")]
        public int? EndSeconds { get; set; }

        [JsonPropertyName("frame_rate")]
        public int? FrameRate { get; set; }

        [JsonPropertyName("effects")]
        public EOIEffects Effects { get; set; }


        public EOIProcessVideosInputs() : base()
        {
            this.InputVideoFiles = new string[] { };
            this.OutputVideoFile = null;
        }

        public EOIProcessVideosInputs(
            string[] videoFiles,
            string outputFile,
            EOIRegion[] regions,
            int frame_rate) : base()
        {
            this.Regions = regions;
            this.InputVideoFiles = videoFiles;
            this.OutputVideoFile= outputFile;
            this.FrameRate = frame_rate;
        }
    }
}
