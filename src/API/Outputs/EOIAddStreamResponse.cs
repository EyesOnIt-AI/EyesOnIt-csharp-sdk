using System.Text.Json;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIAddStreamResponse: EOIBaseOutputs
    {
        public string StreamId { get; set; }

        internal EOIAddStreamResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            ApplyData(eoiMessage.Data);
        }

        internal EOIAddStreamResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
            ApplyData(eoiResponse.Data);
        }

        internal EOIAddStreamResponse(bool success, string message = null) : base(success, message)
        {
        }

        private void ApplyData(object data)
        {
            if (!Success || !(data is JsonElement dataElement))
            {
                return;
            }

            if (dataElement.TryGetProperty("stream_id", out JsonElement streamIdElement))
            {
                StreamId = JsonSerializer.Deserialize<string>(streamIdElement.GetRawText());
            }
        }
    }
}
