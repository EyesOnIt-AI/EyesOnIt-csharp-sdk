using System;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOISimilaritySearchInputs {
        [JsonPropertyName("seed_id")]
        public string ReferenceImageId { get; set; }

        [JsonPropertyName("start_date_time")]
        public DateTime StartDateTime { get; set; }

        [JsonPropertyName("end_date_time")]
        public DateTime EndDateTime { get; set; }

        [JsonPropertyName("stream_list")]
        public string[] StreamList { get; set; }

        public EOISimilaritySearchInputs(string resultId)
        {
            ReferenceImageId = resultId;
        }
    }
}
