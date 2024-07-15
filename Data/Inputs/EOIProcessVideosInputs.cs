using System.Text.Json.Serialization;
using EyesOnItSDK.Data.Elements;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIProcessVideosInputs : EOIBaseInputs
    {
        [JsonPropertyName("input_video_path_list")]
        public string[] InputVideoFiles { get; set; }

        [JsonPropertyName("output_video_path")]
        public string OutputVideoFile { get; set; }

        [JsonPropertyName("alerting")]
        public EOIAlerting Alerting { get; set; }

        [JsonPropertyName("efficient_detection")]
        public EOIMotionDetection MotionDetection { get; set; }

        [JsonPropertyName("bounding_box")]
        public EOIBoundingBox BoundingBox{ get; set; }

        [JsonPropertyName("frame_rate")]
        public int? FrameRate { get; set; }

        [JsonPropertyName("object_size")]
        public int ObjectSize { get; set; }

        [JsonPropertyName("synchronous")]
        public bool Synchronous { get; set; }

        [JsonPropertyName("real_time")]
        public bool RealTime { get; set; }

        [JsonPropertyName("show_motion")]
        public bool ShowMotion { get; set; }

        [JsonPropertyName("show_bounding_box")]
        public bool ShowBoundingBox { get; set; }

        [JsonPropertyName("show_confidence_levels")]
        public bool ShowConfidenceLevels { get; set; }

        [JsonPropertyName("output_all_frames")]
        public bool OutputAllFrames { get; set; }

        public EOIProcessVideosInputs() : base()
        {
            this.InputVideoFiles = new string[] { };
            this.OutputVideoFile = null;
            this.Alerting = new EOIAlerting();
            this.MotionDetection = new EOIMotionDetection();
            this.BoundingBox= new EOIBoundingBox();
        }

        public EOIProcessVideosInputs(
            string[] videoFiles,
            string outputFile,
            EOIObjectDescription[] objectDescriptions, 
            EOIRegion[] regions,
            EOIAlerting alerting,
            EOIMotionDetection motionDetection,
            EOIBoundingBox boundingBox,
            string stream_url,
            string name,
            int frame_rate) : base(objectDescriptions, regions)
        {
            this.InputVideoFiles = videoFiles;
            this.OutputVideoFile= outputFile;
            this.Alerting = alerting;
            this.MotionDetection = motionDetection;
            this.BoundingBox = boundingBox;
            this.FrameRate = frame_rate;
        }
    }
}
