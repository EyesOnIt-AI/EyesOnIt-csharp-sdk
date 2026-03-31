namespace EyesOnItSDK.API.Outputs
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
