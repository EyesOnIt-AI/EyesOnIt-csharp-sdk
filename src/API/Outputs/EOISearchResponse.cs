using EyesOnItSDK.Data.Elements;
using System.Collections.Generic;
using System.Text.Json;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOISearchResponse : EOIBaseOutputs
    {
        public List<EOISearchResult> Results { get; set; }

        internal EOISearchResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                // Check if it contains the "stream" key
                if (dataElement.TryGetProperty("results", out JsonElement resultsElement))
                {
                    // Deserialize the streams part
                    Results = JsonSerializer.Deserialize<List<EOISearchResult>>(resultsElement.GetRawText());
                }
            }
        }

        internal EOISearchResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOISearchResponse(bool success, string message = null) : base(success, message)
        {
        }

    }
}
