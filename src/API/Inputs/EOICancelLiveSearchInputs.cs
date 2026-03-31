using System;

namespace EyesOnItSDK.API.Inputs
{
    [Obsolete("Use EOIUpdateLiveSearchInputs.")]
    public class EOICancelLiveSearchInputs : EOIUpdateLiveSearchInputs
    {
        public EOICancelLiveSearchInputs(int searchId) : base(searchId)
        {
        }
    }
}
