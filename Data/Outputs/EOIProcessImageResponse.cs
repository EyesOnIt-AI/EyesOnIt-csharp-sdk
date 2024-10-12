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
    public class EOIProcessImageResponse : EOIBaseOutputs
    {
        public List<EOIDetection> Detections { get; set; }

        internal EOIProcessImageResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                // Check if it contains the "detection" key
                if (dataElement.TryGetProperty("detections", out JsonElement detectionElement))
                {
                    // Deserialize the detection part
                    Detections = JsonSerializer.Deserialize<List<EOIDetection>>(detectionElement.GetRawText());
                }
            }
        }

        internal EOIProcessImageResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIProcessImageResponse(bool success, string message = null) : base(success, message)
        {
        }

    }
}
