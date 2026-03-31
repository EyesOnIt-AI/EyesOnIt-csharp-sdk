using System.Text.Json;

namespace EyesOnItSDK.Data.Outputs
{
    public class EOIRemoveFacerecGroupResponse : EOIBaseOutputs
    {
        public string GroupId { get; set; }
        public int RemovedMemberships { get; set; }

        internal EOIRemoveFacerecGroupResponse(EOIMessage eoiMessage) : base(eoiMessage)
        {
            if (Success)
            {
                JsonElement dataElement = (JsonElement)eoiMessage.Data;

                if (dataElement.TryGetProperty("group_id", out JsonElement groupIdElement))
                {
                    GroupId = JsonSerializer.Deserialize<string>(groupIdElement.GetRawText());
                }

                if (dataElement.TryGetProperty("removed_memberships", out JsonElement removedElement))
                {
                    RemovedMemberships = JsonSerializer.Deserialize<int>(removedElement.GetRawText());
                }
            }
        }

        internal EOIRemoveFacerecGroupResponse(EOIResponse eoiResponse) : base(eoiResponse)
        {
        }

        internal EOIRemoveFacerecGroupResponse(bool success, string message = null) : base(success, message)
        {
        }
    }
}
