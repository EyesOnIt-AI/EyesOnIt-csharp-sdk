using System.Collections.Generic;
using System.Text.Json;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIGetSupportedClassesResponse : EOIBaseOutputs
    {
        public List<string> Classes { get; set; }

        internal EOIGetSupportedClassesResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                // Check if it contains the "supported_classes" key
                if (dataElement.TryGetProperty("supported_classes", out JsonElement classesElement))
                {
                    // Deserialize the supported classes part
                    Classes = JsonSerializer.Deserialize<List<string>>(classesElement.GetRawText());
                }
            }
        }

        internal EOIGetSupportedClassesResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIGetSupportedClassesResponse(bool success, string message = null) : base(success, message)
        {
        }

    }
}
