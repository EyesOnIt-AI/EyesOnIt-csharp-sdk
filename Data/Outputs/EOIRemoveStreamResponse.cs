using EyesOnItSDK.Data.Inputs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOIRemoveStreamResponse: EOIBaseOutputs
    {
        internal EOIRemoveStreamResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
        }

        internal EOIRemoveStreamResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIRemoveStreamResponse(bool success, string message = null) : base(success, message)
        {
        }

        //public string ToJson()
        //{
        //    var options = new JsonSerializerOptions
        //    {
        //        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        //    };

        //    var jsonData = JsonSerializer.Serialize(this, options);

        //    return jsonData;
        //}

        //internal static EOIProcessImageOutputs FromJSON(string strResponse)
        //{
        //    return new EOIProcessImageOutputs(true, null);
        //}
    }
}
