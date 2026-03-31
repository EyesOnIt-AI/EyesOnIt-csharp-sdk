using EyesOnItSDK.Data.Elements;
using System.Text.Json;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOIGetStreamDetailsResponse : EOIBaseOutputs
    {
        public EOIStreamDetails Stream { get; set; }

        internal EOIGetStreamDetailsResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                // Check if it contains the "stream" key
                if (dataElement.TryGetProperty("stream", out JsonElement streamElement))
                {
                    // Deserialize the stream part
                    Stream = JsonSerializer.Deserialize<EOIStreamDetails>(streamElement.GetRawText());
                }
            }
        }

        internal EOIGetStreamDetailsResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIGetStreamDetailsResponse(bool success, string message = null) : base(success, message)
        {
        }

    }
}
