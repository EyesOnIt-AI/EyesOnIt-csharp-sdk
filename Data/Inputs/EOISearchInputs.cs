using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOISearchInputs {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("object_description")]
        public string ObjectDescription { get; set; }

        public EOISearchInputs(string className, string objectDescription)
        {
            ClassName = className;
            ObjectDescription = objectDescription;
        }
    }
}
