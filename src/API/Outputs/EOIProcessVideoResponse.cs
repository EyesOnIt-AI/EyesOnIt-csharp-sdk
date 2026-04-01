using EyesOnItSDK.API;
using System.Text.Json;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIProcessVideoResponse: EOIBaseOutputs
    {
        public int VideoId { get; set; }

        internal EOIProcessVideoResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success && eoiMessage.Data is JsonElement dataElement && dataElement.TryGetProperty("video_id", out JsonElement videoIdElement))
            {
                VideoId = JsonSerializer.Deserialize<int>(videoIdElement.GetRawText());
            }
        }

        internal EOIProcessVideoResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
            if (Success && eoiResponse.Data is JsonElement dataElement && dataElement.TryGetProperty("video_id", out JsonElement videoIdElement))
            {
                VideoId = JsonSerializer.Deserialize<int>(videoIdElement.GetRawText());
            }
        }

        internal EOIProcessVideoResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
