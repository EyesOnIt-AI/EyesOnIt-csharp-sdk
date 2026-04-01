using EyesOnItSDK.API.Elements;
using EyesOnItSDK.API.Inputs;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyesOnItSDK.API
{
    public class EOIAPIUtils
    {
        private static int MIN_REGION_WIDTH = 224;
        private static int MIN_REGION_HEIGHT = 224;

        private readonly EyesOnItAPI eoiAPI;

        public EOIAPIUtils(EyesOnItAPI eoiAPI)
        {
            this.eoiAPI = eoiAPI;
        }


        public static EOIStreamInfo GetInfoForStream(List<EOIStreamInfo> streamInfoList, string streamUrl)
        {
            EOIStreamInfo requestedStreamInfo = null;

            if (streamInfoList != null && streamInfoList.Count > 0)
            {
                requestedStreamInfo = streamInfoList.Where(si => si.StreamUrl == streamUrl).SingleOrDefault();
            }

            return requestedStreamInfo;
        }

        public static Size GetMinimumRegionSize()
        {
            return new Size(MIN_REGION_WIDTH, MIN_REGION_HEIGHT);
        }

        public static string GetJsonForAddStreamInputsList(List<EOIAddStreamInputs> addStreamInputsList)
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var json = JsonSerializer.Serialize<List<EOIAddStreamInputs>>(addStreamInputsList, options);

            return json;
        }

        public static List<EOIAddStreamInputs> GetAddStreamInputsListFromJson(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            List<EOIAddStreamInputs> addStreamInputsList = json == null ? null : JsonSerializer.Deserialize<List<EOIAddStreamInputs>>(json, options);

            return addStreamInputsList;
        }
    }
}
