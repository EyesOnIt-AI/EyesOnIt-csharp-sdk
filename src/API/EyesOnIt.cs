using System;

namespace EyesOnItSDK.API
{
    [Obsolete("Use EyesOnItAPI.")]
    public class EyesOnIt : EyesOnItAPI
    {
        public EyesOnIt(string baseUrl) : base(baseUrl)
        {
        }
    }
}
