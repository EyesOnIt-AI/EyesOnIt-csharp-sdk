using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIStopVideoInputs
    {
        [JsonPropertyName("video_id")]
        public string VideoId { get; set; }

        public EOIStopVideoInputs(string videoId = null)
        {
            VideoId = videoId;
        }
    }
}
