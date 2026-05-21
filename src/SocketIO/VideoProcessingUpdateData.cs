using System.Text.Json.Serialization;

namespace EyesOnItSDK.SocketIO
{
    public class VideoProcessingUpdateData
    {
        [JsonPropertyName("video_id")]
        public string VideoId { get; set; }

        [JsonPropertyName("input_video_path")]
        public string InputVideoPath { get; set; }

        [JsonPropertyName("input_video_name")]
        public string InputVideoName { get; set; }

        [JsonPropertyName("length")]
        public double Length { get; set; }

        [JsonPropertyName("current_processing_time")]
        public double CurrentProcessingTime { get; set; }

        [JsonPropertyName("percent_complete")]
        public double PercentComplete { get; set; }
    }
}
