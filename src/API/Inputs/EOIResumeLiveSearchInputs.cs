using System;

namespace EyesOnItSDK.API.Inputs
{
    [Obsolete("Use EOIUpdateLiveSearchInputs.")]
    public class EOIResumeLiveSearchInputs : EOIUpdateLiveSearchInputs
    {
        public EOIResumeLiveSearchInputs(int searchId) : base(searchId)
        {
        }
    }
}
