using System.Text.Json;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIGetVideoStatusResponse : EOIBaseOutputs
    {
        public JsonElement Video { get; set; }
        public bool HasVideo { get; set; }

        internal EOIGetVideoStatusResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            ApplyData(eoiMessage.Data);
        }

        internal EOIGetVideoStatusResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
            ApplyData(eoiResponse.Data);
        }

        internal EOIGetVideoStatusResponse(bool success, string message = null) : base(success, message)
        {
        }

        private void ApplyData(object data)
        {
            if (!Success || !(data is JsonElement dataElement))
            {
                return;
            }

            if (dataElement.TryGetProperty("video", out JsonElement videoElement))
            {
                Video = videoElement.Clone();
                HasVideo = true;
            }
        }
    }
}
