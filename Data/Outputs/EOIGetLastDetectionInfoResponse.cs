using EyesOnItSDK.Data.Elements;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOIGetLastDetectionInfoResponse: EOIBaseOutputs
    {
        public string Image{ get; set; }

        public List<EOIDetection> Detections { get; set; }

        internal EOIGetLastDetectionInfoResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                // Check if it contains the "detections" key
                if (dataElement.TryGetProperty("detections", out JsonElement detectionElement))
                {
                    // Deserialize the detection part
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

        internal EOIGetLastDetectionInfoResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIGetLastDetectionInfoResponse(bool success, string message = null) : base(success, message)
        {
        }

        public EOIDetection GetMostRecentDetection()
        {
            DateTime mostRecentAlertTime = DateTime.MinValue;
            EOIDetection mostRecentDetection = null;

            foreach (var detection in Detections)
            {
                if (detection.Time > mostRecentAlertTime)
                {
                    mostRecentAlertTime = detection.Time;
                    mostRecentDetection = detection;
                }
            }

            return mostRecentDetection;
        }
    }
}
