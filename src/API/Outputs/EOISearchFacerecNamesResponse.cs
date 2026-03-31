using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOISearchFacerecNamesResult
    {
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
    }

    public class EOISearchFacerecNamesResponse : EOIBaseOutputs
    {
        public List<EOISearchFacerecNamesResult> Matches { get; set; }

        internal EOISearchFacerecNamesResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                if (dataElement.TryGetProperty("matches", out JsonElement matchesElement))
                {
                    Matches = JsonSerializer.Deserialize<List<EOISearchFacerecNamesResult>>(matchesElement.GetRawText());
                }
            }
        }

        internal EOISearchFacerecNamesResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOISearchFacerecNamesResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
