using EyesOnItSDK.Data.Elements;
using System.Collections.Generic;
using System.Text.Json;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOILiveSearchResponse : EOIBaseOutputs
    {
        public int SearchId { get; set; }

        internal EOILiveSearchResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                // Check if it contains the "search_id" key
                if (dataElement.TryGetProperty("search_id", out JsonElement resultsElement))
                {
                    // Deserialize the search ID
                    SearchId = JsonSerializer.Deserialize<int>(resultsElement.GetRawText());
                }
            }
        }

        internal EOILiveSearchResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOILiveSearchResponse(bool success, string message = null) : base(success, message)
        {
        }

    }
}
