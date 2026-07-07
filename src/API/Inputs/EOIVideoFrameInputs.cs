using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIVideoFrameInputs
    {
        [JsonPropertyName("stream_id")]
        public string StreamId { get; set; }

        public EOIVideoFrameInputs(string streamId)
        {
            StreamId = streamId;
        }
    }
}
