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
    }
}
