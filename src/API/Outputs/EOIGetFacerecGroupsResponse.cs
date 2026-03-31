using System.Collections.Generic;
using System.Text.Json;

namespace EyesOnItSDK.API.Outputs
{
    public class EOIGetFacerecGroupsResponse : EOIBaseOutputs
    {
        public List<EOIFacerecGroup> Groups { get; set; }

        internal EOIGetFacerecGroupsResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                if (dataElement.TryGetProperty("groups", out JsonElement groupsElement))
                {
                    Groups = JsonSerializer.Deserialize<List<EOIFacerecGroup>>(groupsElement.GetRawText());
                }
            }
        }

        internal EOIGetFacerecGroupsResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIGetFacerecGroupsResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
