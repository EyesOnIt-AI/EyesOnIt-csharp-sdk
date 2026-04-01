using EyesOnItSDK.API.Elements;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIArchiveSearchInputs : EOISearchInputs
    {
        [JsonPropertyName("start_date_time")]
        public string StartDateTime { get; set; }

        [JsonPropertyName("end_date_time")]
        public string EndDateTime { get; set; }


        public EOIArchiveSearchInputs() : base()
        {
        }

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var jsonData = JsonSerializer.Serialize(this, options);

            return jsonData;
        }

        public static EOIArchiveSearchInputs FromJson(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            EOIArchiveSearchInputs archiveSearchInputs = jsonString == null ? null : JsonSerializer.Deserialize<EOIArchiveSearchInputs>(jsonString, options);

            return archiveSearchInputs;
        }
    }
}