using System.Text.Json;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIStopVideoResponse : EOIBaseOutputs
    {
        public string VideoId { get; set; }

        internal EOIStopVideoResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            ApplyData(eoiMessage.Data);
        }

        internal EOIStopVideoResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
            ApplyData(eoiResponse.Data);
        }

        internal EOIStopVideoResponse(bool success, string message = null) : base(success, message)
        {
        }

        private void ApplyData(object data)
        {
            if (!Success || !(data is JsonElement dataElement))
            {
                return;
            }

            if (dataElement.TryGetProperty("video_id", out JsonElement videoIdElement))
            {
                VideoId = videoIdElement.ValueKind == JsonValueKind.String
                    ? videoIdElement.GetString()
                    : videoIdElement.ValueKind == JsonValueKind.Null
                        ? null
                        : videoIdElement.GetRawText();
            }
        }
    }
}
