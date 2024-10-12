using EyesOnItSDK.Data.Inputs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using EyesOnItSDK.Data.Elements;
using System.Runtime.Remoting.Messaging;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOIGetSupportedClassesResponse : EOIBaseOutputs
    {
        public List<string> Classes { get; set; }

        internal EOIGetSupportedClassesResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                // Check if it contains the "detection" key
                if (dataElement.TryGetProperty("supported_classes", out JsonElement classesElement))
                {
                    // Deserialize the detection part
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
