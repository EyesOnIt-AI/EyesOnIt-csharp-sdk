using EyesOnItSDK.API;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIUpdateConfigResponse : EOIBaseOutputs
    {
        internal EOIUpdateConfigResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
        }

        internal EOIUpdateConfigResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIUpdateConfigResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
