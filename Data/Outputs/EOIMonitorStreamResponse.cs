using EyesOnItSDK.Data.Inputs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOIMonitorStreamResponse: EOIBaseOutputs
    {
        internal EOIMonitorStreamResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
        }

        internal EOIMonitorStreamResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIMonitorStreamResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
