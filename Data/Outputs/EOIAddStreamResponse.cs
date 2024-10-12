using EyesOnItSDK.Data.Inputs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOIAddStreamResponse: EOIBaseOutputs
    {
        internal EOIAddStreamResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
        }

        internal EOIAddStreamResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIAddStreamResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
