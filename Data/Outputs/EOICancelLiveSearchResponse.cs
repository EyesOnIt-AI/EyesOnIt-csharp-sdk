namespace EyesOnItSDK.Data.Outputs
{
    public class EOICancelLiveSearchResponse: EOIBaseOutputs
    {
        internal EOICancelLiveSearchResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
        }

        internal EOICancelLiveSearchResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOICancelLiveSearchResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
