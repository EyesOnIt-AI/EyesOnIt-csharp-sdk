using EyesOnItSDK;
using EyesOnItSDK.Data.Elements;
using EyesOnItSDK.Data.Inputs;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace EyesOnItSDK
{
    public class EOIAPIUtils
    {
        private static int MIN_REGION_WIDTH = 224;
        private static int MIN_REGION_HEIGHT = 224;

        private EyesOnItSDK.EyesOnIt eoiAPI;

        public EOIAPIUtils(EyesOnIt eoiAPI)
        {
            this.eoiAPI = eoiAPI;
        }

        public async Task<EOIResponse> GetFrameFromStream(string streamUrl)
        {
            EOIResponse apiResponse = new EOIResponse(false, "unknown error");

            apiResponse = await this.eoiAPI.GetStreamsInfo();

            if (apiResponse.Success && apiResponse.Data != null)
            {
                List<EOIStreamInfo> streamsDataObj = (List<EOIStreamInfo>)apiResponse.Data;
                EOIStreamInfo streamInfo = EOIAPIUtils.GetInfoForStream(streamsDataObj, streamUrl);

                if (streamInfo != null)
                {
                    if (!streamInfo.IsMonitoring)
                    {
                        // need to monitor stream before we can get a frame
                        await this.eoiAPI.MonitorStream(new EOIMonitorStreamInputs(streamUrl, null));
                    }

                    // stream has been added and is being monitored. Get a frame.
                    apiResponse = await this.eoiAPI.GetVideoFrame(new EOIGetVideoFrameInputs(streamUrl));

                    if (!streamInfo.IsMonitoring)
                    {
                        await this.eoiAPI.StopMonitoringStream(new EOIStopMonitoringStreamInputs(streamUrl));
                    }
                }
                else
                {
                    // stream not added. Get preview frame.
                    apiResponse = await this.eoiAPI.GetPreviewVideoFrame(new EOIGetPreviewFrameInputs(streamUrl));
                }
            }

            return apiResponse;
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
