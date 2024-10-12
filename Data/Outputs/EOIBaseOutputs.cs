using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EyesOnItSDK.Data.Elements;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOIBaseOutputs
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        internal EOIBaseOutputs(EOIMessage eoiMessage)
        {
            Success = eoiMessage.Success;
            Message = eoiMessage.Message;
        }

        internal EOIBaseOutputs(EOIResponse eoiResponse)
        {
            Success = eoiResponse.Success;
            Message = eoiResponse.Message;
        }

        internal EOIBaseOutputs(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }

    }
}
