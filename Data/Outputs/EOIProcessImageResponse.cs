using EyesOnItSDK.Data.Elements;
using System.Collections.Generic;
using System.Text.Json;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOIProcessImageResponse : EOIBaseOutputs
    {
        public List<EOIDetection> Detections { get; set; }

        public string Image { get; set; }

        internal EOIProcessImageResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                // Check if it contains the "detections" key
                if (dataElement.TryGetProperty("detections", out JsonElement detectionElement))
                {
                    // Deserialize the detections part
                    Detections = JsonSerializer.Deserialize<List<EOIDetection>>(detectionElement.GetRawText());
                }

                // Check if it contains the "image" key
                if (dataElement.TryGetProperty("image", out JsonElement imageElement))
                {
                    // Deserialize the image part
                    Image = JsonSerializer.Deserialize<string>(imageElement.GetRawText());
                }
            }
        }

        internal EOIProcessImageResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIProcessImageResponse(bool success, string message = null) : base(success, message)
        {
        }

    }
}
