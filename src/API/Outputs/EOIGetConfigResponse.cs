using System.Text.Json;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIGetConfigResponse : EOIBaseOutputs
    {
        public JsonElement Config { get; set; }
        public bool HasConfig { get; set; }

        internal EOIGetConfigResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            ApplyData(eoiMessage.Data);
        }

        internal EOIGetConfigResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
            ApplyData(eoiResponse.Data);
        }

        internal EOIGetConfigResponse(bool success, string message = null) : base(success, message)
        {
        }

        private void ApplyData(object data)
        {
            if (!Success || !(data is JsonElement dataElement))
            {
                return;
            }

            Config = dataElement.Clone();
            HasConfig = true;
        }
    }
}
