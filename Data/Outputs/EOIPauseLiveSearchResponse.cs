namespace EyesOnItSDK.Data.Outputs
{
    public class EOIPauseLiveSearchResponse: EOIBaseOutputs
    {
        internal EOIPauseLiveSearchResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
        }

        internal EOIPauseLiveSearchResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIPauseLiveSearchResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
