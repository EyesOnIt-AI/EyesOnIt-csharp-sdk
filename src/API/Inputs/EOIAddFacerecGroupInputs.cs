using EyesOnItSDK.Data.Elements;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIAddFacerecGroupInputs : EOIBaseInputs
    {
        [JsonPropertyName("group_id")]
        public string GroupId { get; set; }

        [JsonPropertyName("group_name")]
        public string GroupName { get; set; }

        [JsonPropertyName("group_description")]
        public string GroupDescription { get; set; }

        public EOIAddFacerecGroupInputs(string groupId, string groupName, string groupDescription)
        {
            this.GroupId = groupId;
            this.GroupName = groupName;
            this.GroupDescription = groupDescription;
        }
    }
}
