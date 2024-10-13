namespace EyesOnItSDK.Data.Outputs
{
    public class EOIProcessVideosResponse: EOIBaseOutputs
    {
        internal EOIProcessVideosResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
        }

        internal EOIProcessVideosResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIProcessVideosResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
