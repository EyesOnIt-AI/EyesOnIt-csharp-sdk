namespace EyesOnItSDK.API.Outputs
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
