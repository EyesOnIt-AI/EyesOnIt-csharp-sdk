using EyesOnItSDK.API.Elements;
using System.Collections.Generic;
using System.Text.Json;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIHealthResponse : EOIBaseOutputs
    {
        public List<EOIGpu> Gpus { get; set; }
        public EOISystemHealth System { get; set; }

        internal EOIHealthResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                if (dataElement.TryGetProperty("gpus", out JsonElement gpusElement))
                {
                    Gpus = JsonSerializer.Deserialize<List<EOIGpu>>(gpusElement.GetRawText());
                }

                if (dataElement.TryGetProperty("system", out JsonElement systemElement))
                {
                    System = JsonSerializer.Deserialize<EOISystemHealth>(systemElement.GetRawText());
                }
            }
        }

        internal EOIHealthResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
