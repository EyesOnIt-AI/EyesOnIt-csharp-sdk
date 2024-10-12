using System.Text.Json;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOIGetVideoFrameResponse: EOIBaseOutputs
    {
        public string Image { get; set; }

        internal EOIGetVideoFrameResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                // Check if it contains the "detection" key
                if (dataElement.TryGetProperty("image", out JsonElement imageElement))
                {
                    // Deserialize the detection part
                    Image = JsonSerializer.Deserialize<string>(imageElement.GetRawText());
                }
            }
        }

        internal EOIGetVideoFrameResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIGetVideoFrameResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
