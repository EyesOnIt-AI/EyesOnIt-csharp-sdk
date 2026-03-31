namespace EyesOnItSDK.Data.Outputs
{
    public class EOIResumeLiveSearchResponse: EOIBaseOutputs
    {
        internal EOIResumeLiveSearchResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
        }

        internal EOIResumeLiveSearchResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIResumeLiveSearchResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
