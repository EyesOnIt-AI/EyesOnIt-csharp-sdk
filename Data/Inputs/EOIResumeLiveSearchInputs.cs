using EyesOnItSDK.Data.Elements;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIResumeLiveSearchInputs {
        [JsonPropertyName("search_id")]
        public int SearchID { get; set; }

        public EOIResumeLiveSearchInputs(int searchId)
        {
            SearchID = searchId;
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

        public static EOIResumeLiveSearchInputs FromJson(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            EOIResumeLiveSearchInputs resumeLiveSearchInputs = jsonString == null ? null : JsonSerializer.Deserialize<EOIResumeLiveSearchInputs>(jsonString, options);

            return resumeLiveSearchInputs;
        }
    }
}
