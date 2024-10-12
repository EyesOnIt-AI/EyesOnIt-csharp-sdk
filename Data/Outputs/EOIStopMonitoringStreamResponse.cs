using EyesOnItSDK.Data.Inputs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOIStopMonitoringStreamResponse: EOIBaseOutputs
    {
        internal EOIStopMonitoringStreamResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
        }

        internal EOIStopMonitoringStreamResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIStopMonitoringStreamResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
