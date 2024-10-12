using EyesOnItSDK.Data.Inputs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOIProcessVideosResponse: EOIBaseOutputs
    {
        internal EOIProcessVideosResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
        }

        internal EOIProcessVideosResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIProcessVideosResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
