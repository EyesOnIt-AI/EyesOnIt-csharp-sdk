using EyesOnItSDK.Data.Elements;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOIPauseLiveSearchInputs {
        [JsonPropertyName("search_id")]
        public int SearchID { get; set; }

        public EOIPauseLiveSearchInputs(int searchId)
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

        public static EOIPauseLiveSearchInputs FromJson(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            EOIPauseLiveSearchInputs pauseLiveSearchInputs = jsonString == null ? null : JsonSerializer.Deserialize<EOIPauseLiveSearchInputs>(jsonString, options);

            return pauseLiveSearchInputs;
        }
    }
}
