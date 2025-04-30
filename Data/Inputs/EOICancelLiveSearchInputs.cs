using EyesOnItSDK.Data.Elements;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.Data.Inputs
{
    public class EOICancelLiveSearchInputs {
        [JsonPropertyName("search_id")]
        public int SearchID { get; set; }

        public EOICancelLiveSearchInputs(int searchId)
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

        public static EOICancelLiveSearchInputs FromJson(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            EOICancelLiveSearchInputs cancelLiveSearchInputs = jsonString == null ? null : JsonSerializer.Deserialize<EOICancelLiveSearchInputs>(jsonString, options);

            return cancelLiveSearchInputs;
        }
    }
}
