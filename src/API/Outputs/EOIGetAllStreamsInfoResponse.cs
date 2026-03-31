using EyesOnItSDK.API.Elements;
using System.Collections.Generic;
using System.Text.Json;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIGetAllStreamsInfoResponse : EOIBaseOutputs
    {
        public List<EOIStreamInfo> Streams { get; set; }

        internal EOIGetAllStreamsInfoResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                // Check if it contains the "stream" key
                if (dataElement.TryGetProperty("streams", out JsonElement streamsElement))
                {
                    // Deserialize the streams part
                    Streams = JsonSerializer.Deserialize<List<EOIStreamInfo>>(streamsElement.GetRawText());
                }
            }
        }

        internal EOIGetAllStreamsInfoResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIGetAllStreamsInfoResponse(bool success, string message = null) : base(success, message)
        {
        }

    }
}
