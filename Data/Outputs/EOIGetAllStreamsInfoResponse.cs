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
    public class EOIGetAllStreamsInfoResponse : EOIBaseOutputs
    {
        public List<EOIStreamInfo> Streams { get; set; }

        internal EOIGetAllStreamsInfoResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                // Check if it contains the "detection" key
                if (dataElement.TryGetProperty("streams", out JsonElement streamsElement))
                {
                    // Deserialize the detection part
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
