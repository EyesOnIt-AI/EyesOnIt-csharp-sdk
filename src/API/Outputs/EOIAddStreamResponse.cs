namespace EyesOnItSDK.Data.Outputs
{
    public class EOIAddStreamResponse: EOIBaseOutputs
    {
        internal EOIAddStreamResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
        }

        internal EOIAddStreamResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIAddStreamResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
