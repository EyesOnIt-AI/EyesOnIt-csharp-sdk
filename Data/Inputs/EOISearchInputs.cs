using System;
using System.Security.Policy;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOISearchInputs {
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }

        [JsonPropertyName("object_description")]
        public string ObjectDescription { get; set; }

        [JsonPropertyName("start_date_time")]
        public DateTime StartDateTime { get; set; }

        [JsonPropertyName("end_date_time")]
        public DateTime EndDateTime { get; set; }

        [JsonPropertyName("stream_list")]
        public string[] StreamList { get; set; }

        public EOISearchInputs(string className, string objectDescription)
        {
            ClassName = className;
            ObjectDescription = objectDescription;
        }
    }
}
