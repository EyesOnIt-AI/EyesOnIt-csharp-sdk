using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API.Inputs
{
    public class EOIUpdateLiveSearchInputs
    {
        [JsonPropertyName("search_id")]
        public int SearchId { get; set; }

        public EOIUpdateLiveSearchInputs(int searchId)
        {
            SearchId = searchId;
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

        public static EOIUpdateLiveSearchInputs FromJson(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            EOIUpdateLiveSearchInputs updateLiveSearchInputs = jsonString == null ? null : JsonSerializer.Deserialize<EOIUpdateLiveSearchInputs>(jsonString, options);

            return updateLiveSearchInputs;
        }
    }
}
