using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIGetVideoStatusInputs
    {
        [JsonPropertyName("video_id")]
        public string VideoId { get; set; }

        public EOIGetVideoStatusInputs(string videoId)
        {
            VideoId = videoId;
        }
    }
}
