using Serilog;
using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EyesOnItSDK.Data.Elements;
using EyesOnItSDK.Data.Inputs;
using System.Net;
using EyesOnItSDK.Data.Outputs;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;

namespace EyesOnItSDK
{
    public class EyesOnIt
    {
        private readonly HttpClient httpClient;
        private readonly string addStreamPath = "/add_stream";
        private readonly string baseUrl;
        private readonly string getAllStreamsInfoPath = "/get_all_streams_info";
        private readonly string getLastDetectionInfoPath = "/get_last_detection_info";
        private readonly string getStreamDetailsPath = "/get_stream_details";
        private readonly string getSupportedClassesPath = "/get_supported_classes";
        private readonly string getVideoFramePath = "/get_video_frame";
        private readonly string monitorStreamPath = "/monitor_stream";
        private readonly string processImagePath = "/process_image";
        private readonly string processVideosPath = "/process_videos";
        private readonly string removeStreamPath = "/remove_stream";
        private readonly string stopMonitorStreamPath = "/stop_monitoring";
        private readonly string searchLivePath = "/live_search";
        private readonly string searchArchivePath = "/archive_search";
        private readonly string pauseLiveSearchPath = "/pause_live_search";
        private readonly string resumeLiveSearchPath = "/resume_live_search";
        private readonly string cancelLiveSearchPath = "/cancel_live_search";
        private readonly string facerecGroupsPath = "/facerec_groups";
        private readonly string facerecSearchGroupNamesPath = "/facerec_search_group_names";
        private readonly string facerecSearchPeopleNamesPath = "/facerec_search_people_names";
        private readonly string facerecAddGroupPath = "/facerec_add_group";
        private readonly string facerecRemoveGroupPath = "/facerec_remove_group";
        private readonly string facerecAddPersonPath = "/facerec_add_person";
        private readonly string facerecAddPeoplePath = "/facerec_add_people";
        private readonly string facerecRemovePersonPath = "/facerec_remove_person";
        private readonly string facerecPersonDetailsPath = "/facerec_person_details";

        public EyesOnIt(string baseUrl)
        {
            httpClient = new HttpClient();
            this.baseUrl = baseUrl;
        }

        public string GetBaseUrl()
        {
            return this.baseUrl;
        }

        public async Task<EOIProcessImageResponse> ProcessImageFromFile(string filePath, EOIRegion[] regions)
        {
            return await this.ProcessImageFromFile(
                new EOIProcessImageInputs(null, regions), 
                filePath);
        }

        public async Task<EOIProcessImageResponse> ProcessImageFromFile(EOIProcessImageInputs inputs, string filePath)
        {
            EOIProcessImageResponse eoiProcessImageResponse = new EOIProcessImageResponse(EOIValidation.ValidateProcessImageInputs(inputs));

            if (eoiProcessImageResponse.Success)
            {
                if (filePath == null || filePath.Length == 0)
                {
                    eoiProcessImageResponse = new EOIProcessImageResponse(false, $"filePath must not be null or empty.filePath = {filePath}");
                }
            }

            if (eoiProcessImageResponse.Success && filePath != null)
            {
                // Read the image file as a byte array
                byte[] imageBytes = File.ReadAllBytes(filePath);

                // Convert the byte array to a Base64 encoded string
                string base64String = Convert.ToBase64String(imageBytes);

                EOIProcessImageInputs inputsWithImage = new EOIProcessImageInputs(base64String, inputs.Regions);

                eoiProcessImageResponse = await this.ProcessImage(inputsWithImage);
            }

            return eoiProcessImageResponse;
        }

        public async Task<EOIProcessImageResponse> ProcessImage(string base64Image, EOIRegion[] regions)
        {
            EOIProcessImageInputs inputs = new EOIProcessImageInputs(base64Image, regions);

            return await this.ProcessImage(inputs);
        }

        public async Task<EOIProcessImageResponse> ProcessImage(EOIProcessImageInputs inputs)
        {
            EOIProcessImageResponse eoiProcessImageResponse = new EOIProcessImageResponse(EOIValidation.ValidateProcessImageInputs(inputs));

            if (eoiProcessImageResponse.Success)
            {
                // set up request endpoint and body
                string endPoint = $"{baseUrl}{processImagePath}";

                Log.Debug($"Calling {endPoint}");

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize(inputs, options);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    eoiProcessImageResponse = new EOIProcessImageResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"InferFromImage: Exception: {exc.Message}");
                    eoiProcessImageResponse = new EOIProcessImageResponse(false, exc.Message);
                }
            }

            return eoiProcessImageResponse;
        }

        public async Task<EOIGetAllStreamsInfoResponse> GetAllStreamsInfo()
        {
            EOIGetAllStreamsInfoResponse eoiGetAllStreamsInfoResponse;

            string endPoint = $"{baseUrl}{getAllStreamsInfoPath}";

            Log.Debug($"Calling {endPoint}");

            try
            {
                EOIMessage eoiMessage = await GetAsync(endPoint);
                eoiGetAllStreamsInfoResponse = new EOIGetAllStreamsInfoResponse(eoiMessage);
            }
            catch (HttpRequestException exc)
            {
                Log.Error($"GetAllStreamsInfo: Exception: {exc.Message}");
                eoiGetAllStreamsInfoResponse = new EOIGetAllStreamsInfoResponse(false, exc.Message);
            }

            return eoiGetAllStreamsInfoResponse;
        }

        public async Task<EOIGetStreamDetailsResponse> GetStreamDetails(EOIGetStreamDetailsInputs inputs)
        {
            EOIGetStreamDetailsResponse eoiGetStreamDetailsResponse;

            string endPoint = $"{baseUrl}{getStreamDetailsPath}";

            Log.Debug($"Calling {endPoint}");

            try
            {
                var options = new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var jsonData = JsonSerializer.Serialize(inputs, options);

                EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                eoiGetStreamDetailsResponse = new EOIGetStreamDetailsResponse(eoiMessage);
            }
            catch (HttpRequestException exc)
            {
                Log.Error($"GetStreamDetails: Exception: {exc.Message}");
                eoiGetStreamDetailsResponse = new EOIGetStreamDetailsResponse(false, exc.Message);
            }

            return eoiGetStreamDetailsResponse;
        }

        public async Task<EOIGetSupportedClassesResponse> GetSupportedClasses()
        {
            EOIGetSupportedClassesResponse getSupportedClassesResponse;

            string endPoint = $"{baseUrl}{getSupportedClassesPath}";

            Log.Debug($"Calling {endPoint}");

            try
            {
                EOIMessage eoiMessage = await GetAsync(endPoint);
                getSupportedClassesResponse = new EOIGetSupportedClassesResponse(eoiMessage);
            }
            catch (HttpRequestException exc)
            {
                Log.Error($"GetSupportedClasses: Exception: {exc.Message}");
                getSupportedClassesResponse = new EOIGetSupportedClassesResponse(false, exc.Message);
            }

            return getSupportedClassesResponse;
        }

        public async Task<EOIAddStreamResponse> AddStream(
            string streamUrl,
            string name,
            EOIRegion[] regions,
            EOILine[] lines,
            int? frameRate = 5,
            EOINotification notification = null)
        {
            return await AddStream(streamUrl, name, regions, lines, frameRate, notification, null, null);
        }

        public async Task<EOIAddStreamResponse> AddStream(
            string streamUrl,
            string name,
            EOIRegion[] regions,
            EOILine[] lines,
            int? frameRate = 5,
            EOINotification notification = null,
            EOIRecording recording = null,
            EOIEffects effects = null,
            bool indexForSearch = false,
            string[] searchIndexTypes = null)
        {
            searchIndexTypes = searchIndexTypes ?? new string[0];

            EOIAddStreamInputs inputs = new EOIAddStreamInputs() 
            {   
                Name = name,
                StreamUrl = streamUrl,
                FrameRate = frameRate,
                Regions = regions,
                Lines = lines,
                Notification = notification,
                Recording = recording,
                Effects = effects,
                IndexForSearch = indexForSearch,
                SearchIndexTypes = searchIndexTypes
            };

            return await AddStream(inputs);
        }

        public async Task<EOIAddStreamResponse> AddStream(EOIAddStreamInputs inputs)
        {
            EOIAddStreamResponse addStreamResponse = new EOIAddStreamResponse(EOIValidation.ValidateAddStreamInputs(inputs));

            if (addStreamResponse.Success)
            {
                string endPoint = $"{baseUrl}{addStreamPath}";

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize(inputs, options);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    addStreamResponse = new EOIAddStreamResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"AddStream: Exception: {exc.Message}");
                    addStreamResponse = new EOIAddStreamResponse(false, exc.Message);
                }
            }

            return addStreamResponse;
        }

        public async Task<EOIProcessVideosResponse> ProcessVideos(EOIProcessVideosInputs inputs)
        {
            EOIProcessVideosResponse processVideosResponse = new EOIProcessVideosResponse(EOIValidation.ValidateProcessVideosInputs(inputs));

            if (processVideosResponse.Success)
            {
                string endPoint = $"{baseUrl}{processVideosPath}";

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize(inputs, options);

                    Log.Debug($"Calling {endPoint} with this JSON: {jsonData}");

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    processVideosResponse = new EOIProcessVideosResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"ProcessVideo: Exception: {exc.Message}");
                    processVideosResponse = new EOIProcessVideosResponse(false, exc.Message);
                }
            }

            return processVideosResponse;
        }

        public async Task<EOIMonitorStreamResponse> MonitorStream(string streamUrl, int? durationSeconds)
        {
            return await this.MonitorStream(new EOIMonitorStreamInputs(streamUrl, durationSeconds));
        }

        public async Task<EOIMonitorStreamResponse> MonitorStream(EOIMonitorStreamInputs inputs)
        {
            EOIMonitorStreamResponse eoiMonitorStreamResponse;

            string endPoint = $"{baseUrl}{monitorStreamPath}";

            Log.Debug($"Calling {endPoint}");

            try
            {
                var options = new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var jsonData = JsonSerializer.Serialize(inputs, options);

                EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                eoiMonitorStreamResponse = new EOIMonitorStreamResponse(eoiMessage);
            }
            catch (HttpRequestException exc)
            {
                Log.Error($"MonitorStream: Exception: {exc.Message}");
                eoiMonitorStreamResponse = new EOIMonitorStreamResponse(false, exc.Message);
            }

            return eoiMonitorStreamResponse;
        }

        public async Task<EOIStopMonitoringStreamResponse> StopMonitoringStream(string streamUrl)
        {
            return await this.StopMonitoringStream(new EOIStopMonitoringStreamInputs(streamUrl));
        }

        public async Task<EOIStopMonitoringStreamResponse> StopMonitoringStream(EOIStopMonitoringStreamInputs inputs)
        {
            EOIStopMonitoringStreamResponse eoiStopMonitoringResponse;

            string endPoint = $"{baseUrl}{stopMonitorStreamPath}";

            Log.Debug($"Calling {endPoint}");

            try
            {
                var jsonData = JsonSerializer.Serialize(inputs);

                EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                eoiStopMonitoringResponse = new EOIStopMonitoringStreamResponse(eoiMessage);
            }
            catch (HttpRequestException exc)
            {
                Log.Error($"StopMonitoringStream: Exception: {exc.Message}");
                eoiStopMonitoringResponse = new EOIStopMonitoringStreamResponse(false, exc.Message);
            }

            return eoiStopMonitoringResponse;
        }

        public async Task<EOIGetVideoFrameResponse> GetVideoFrame(string streamUrl)
        {
            return await this.GetVideoFrame(new EOIGetVideoFrameInputs(streamUrl));
        }

        public async Task<EOIGetVideoFrameResponse> GetVideoFrame(EOIGetVideoFrameInputs inputs)
        {
            EOIGetVideoFrameResponse getVideoFrameResponse = new EOIGetVideoFrameResponse(EOIValidation.ValidateStreamUrl(inputs.StreamUrl));

            if (getVideoFrameResponse.Success)
            {
                string endPoint = $"{baseUrl}{getVideoFramePath}";
                Log.Debug($"Calling {endPoint}");

                try
                {
                    var jsonData = JsonSerializer.Serialize(inputs);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    getVideoFrameResponse = new EOIGetVideoFrameResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"GetVideoFrame: Exception: {exc.Message}");
                    getVideoFrameResponse = new EOIGetVideoFrameResponse(false, exc.Message);
                }
            }

            return getVideoFrameResponse;
        }

        public async Task<EOIGetLastDetectionInfoResponse> GetLastDetectionInfo(string streamUrl)
        {
            return await this.GetLastDetectionInfo(new EOIGetLastDetectionInfoInputs(streamUrl));
        }

        public async Task<EOIGetLastDetectionInfoResponse> GetLastDetectionInfo(EOIGetLastDetectionInfoInputs inputs)
        {
            EOIGetLastDetectionInfoResponse getLastDetectionInfoResponse = new EOIGetLastDetectionInfoResponse(EOIValidation.ValidateStreamUrl(inputs.StreamUrl));

            if (getLastDetectionInfoResponse.Success)
            {
                string endPoint = $"{baseUrl}{getLastDetectionInfoPath}";
                Log.Debug($"Calling {endPoint}");

                try
                {
                    var jsonData = JsonSerializer.Serialize(inputs);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    getLastDetectionInfoResponse = new EOIGetLastDetectionInfoResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"GetLastDetectionInfo: Exception: {exc.Message}");
                    getLastDetectionInfoResponse = new EOIGetLastDetectionInfoResponse(false, exc.Message);
                }
            }

            return getLastDetectionInfoResponse;
        }

        public async Task<EOIRemoveStreamResponse> RemoveStream(string streamUrl)
        {
            return await this.RemoveStream(new EOIRemoveStreamInputs(streamUrl));
        }

        public async Task<EOIRemoveStreamResponse> RemoveStream(EOIRemoveStreamInputs inputs)
        {
            EOIRemoveStreamResponse removeStreamResponse = new EOIRemoveStreamResponse(EOIValidation.ValidateStreamUrl(inputs.StreamUrl));

            if (removeStreamResponse.Success)
            {
                string endPoint = $"{baseUrl}{removeStreamPath}";
                Log.Debug($"Calling {endPoint}");

                try
                {
                    var jsonData = JsonSerializer.Serialize(inputs);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    removeStreamResponse = new EOIRemoveStreamResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"RemoveStream: Exception: {exc.Message}");
                    removeStreamResponse = new EOIRemoveStreamResponse(false, exc.Message);
                }
            }

            return removeStreamResponse;
        }

        public async Task<EOISearchResponse> SearchArchive(EOIArchiveSearchInputs inputs)
        {
            EOISearchResponse searchResponse = new EOISearchResponse(EOIValidation.ValidateArchiveSearchInputs(inputs));

            if (searchResponse.Success)
            {
                string endPoint = $"{baseUrl}{searchArchivePath}";

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize(inputs, options);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    searchResponse = new EOISearchResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"Search: Exception: {exc.Message}");
                    searchResponse = new EOISearchResponse(false, exc.Message);
                }
            }

            return searchResponse;
        }

        public async Task<EOILiveSearchResponse> SearchLive(EOILiveSearchInputs inputs)
        {
            EOILiveSearchResponse liveSearchResponse = new EOILiveSearchResponse(EOIValidation.ValidateLiveSearchInputs(inputs));

            if (liveSearchResponse.Success)
            {
                string endPoint = $"{baseUrl}{searchLivePath}";

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize(inputs, options);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    liveSearchResponse = new EOILiveSearchResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"LiveSearch: Exception: {exc.Message}");
                    liveSearchResponse = new EOILiveSearchResponse(false, exc.Message);
                }
            }

            return liveSearchResponse;
        }

        public async Task<EOIPauseLiveSearchResponse> PauseLiveSearch(EOIPauseLiveSearchInputs inputs)
        {
            EOIPauseLiveSearchResponse pauseLiveSearchResponse = new EOIPauseLiveSearchResponse(EOIValidation.ValidatePauseLiveSearchInputs(inputs));

            if (pauseLiveSearchResponse.Success)
            {
                string endPoint = $"{baseUrl}{pauseLiveSearchPath}";

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize(inputs, options);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    pauseLiveSearchResponse = new EOIPauseLiveSearchResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"PauseLiveSearch: Exception: {exc.Message}");
                    pauseLiveSearchResponse = new EOIPauseLiveSearchResponse(false, exc.Message);
                }
            }

            return pauseLiveSearchResponse;
        }

        public async Task<EOIResumeLiveSearchResponse> ResumeLiveSearch(EOIResumeLiveSearchInputs inputs)
        {
            EOIResumeLiveSearchResponse resumeLiveSearchResponse = new EOIResumeLiveSearchResponse(EOIValidation.ValidateResumeLiveSearchInputs(inputs));

            if (resumeLiveSearchResponse.Success)
            {
                string endPoint = $"{baseUrl}{resumeLiveSearchPath}";

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize(inputs, options);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    resumeLiveSearchResponse = new EOIResumeLiveSearchResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"ResumeLiveSearch: Exception: {exc.Message}");
                    resumeLiveSearchResponse = new EOIResumeLiveSearchResponse(false, exc.Message);
                }
            }

            return resumeLiveSearchResponse;
        }

        public async Task<EOICancelLiveSearchResponse> CancelLiveSearch(EOICancelLiveSearchInputs inputs)
        {
            EOICancelLiveSearchResponse cancelLiveSearchResponse = new EOICancelLiveSearchResponse(EOIValidation.ValidateCancelLiveSearchInputs(inputs));

            if (cancelLiveSearchResponse.Success)
            {
                string endPoint = $"{baseUrl}{cancelLiveSearchPath}";

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize(inputs, options);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    cancelLiveSearchResponse = new EOICancelLiveSearchResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"CancelLiveSearch: Exception: {exc.Message}");
                    cancelLiveSearchResponse = new EOICancelLiveSearchResponse(false, exc.Message);
                }
            }

            return cancelLiveSearchResponse;
        }

        public async Task<EOIGetFacerecGroupsResponse> GetFacerecGroups()
        {
            EOIGetFacerecGroupsResponse eoiGetFacerecGroupsResponse;

            string endPoint = $"{baseUrl}{facerecGroupsPath}";

            Log.Debug($"Calling {endPoint}");

            try
            {
                EOIMessage eoiMessage = await GetAsync(endPoint);
                eoiGetFacerecGroupsResponse = new EOIGetFacerecGroupsResponse(eoiMessage);
            }
            catch (HttpRequestException exc)
            {
                Log.Error($"GetFacerecGroups: Exception: {exc.Message}");
                eoiGetFacerecGroupsResponse = new EOIGetFacerecGroupsResponse(false, exc.Message);
            }

            return eoiGetFacerecGroupsResponse;
        }
        public async Task<EOIBaseOutputs> AddFacerecGroup(EOIAddFacerecGroupInputs inputs)
        {
            EOIBaseOutputs addFacerecGroupResponse = new EOIBaseOutputs(EOIValidation.ValidateNewFacerecGroup(inputs));

            if (addFacerecGroupResponse.Success)
            {
                string endPoint = $"{baseUrl}{facerecAddGroupPath}";

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize(inputs, options);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    addFacerecGroupResponse = new EOIBaseOutputs(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"AddFacerecGroup: Exception: {exc.Message}");
                    addFacerecGroupResponse = new EOIBaseOutputs(false, exc.Message);
                }
            }

            return addFacerecGroupResponse;
        }

        public async Task<EOIRemoveFacerecGroupResponse> RemoveFacerecGroup(string groupId)
        {
            EOIRemoveFacerecGroupResponse removeFacerecGroupResponse = new EOIRemoveFacerecGroupResponse(EOIValidation.ValidateRemoveFacerecGroupInputs(groupId));

            if (removeFacerecGroupResponse.Success)
            {
                string endPoint = $"{baseUrl}{facerecRemoveGroupPath}";
                Log.Debug($"Calling {endPoint}");

                try
                {
                    var jsonData = JsonSerializer.Serialize(new { group_id = groupId });
                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    removeFacerecGroupResponse = new EOIRemoveFacerecGroupResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"RemoveFacerecGroup: Exception: {exc.Message}");
                    removeFacerecGroupResponse = new EOIRemoveFacerecGroupResponse(false, exc.Message);
                }
            }

            return removeFacerecGroupResponse;
        }

        public async Task<EOIBaseOutputs> AddFacerecPerson(EOIAddFacerecPersonInputs inputs)
        {
            EOIBaseOutputs addFacerecPersonResponse = new EOIBaseOutputs(EOIValidation.ValidateNewFacerecPerson(inputs));

            if (addFacerecPersonResponse.Success)
            {
                string endPoint = $"{baseUrl}{facerecAddPersonPath}";

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize(inputs, options);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    addFacerecPersonResponse = new EOIBaseOutputs(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"AddFacerecPerson: Exception: {exc.Message}");
                    addFacerecPersonResponse = new EOIBaseOutputs(false, exc.Message);
                }
            }

            return addFacerecPersonResponse;
        }

        public async Task<EOIBaseOutputs> AddFacerecPeople(EOIAddFacerecPeopleInputs inputs)
        {
            EOIBaseOutputs addFacerecPeopleResponse = new EOIBaseOutputs(EOIValidation.ValidateAddFacerecPeople(inputs));

            if (addFacerecPeopleResponse.Success)
            {
                string endPoint = $"{baseUrl}{facerecAddPeoplePath}";

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize(inputs, options);

                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    addFacerecPeopleResponse = new EOIBaseOutputs(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"AddFacerecPerson: Exception: {exc.Message}");
                    addFacerecPeopleResponse = new EOIBaseOutputs(false, exc.Message);
                }
            }

            return addFacerecPeopleResponse;
        }

        public async Task<EOIBaseOutputs> RemoveFacerecPerson(string personId)
        {
            EOIBaseOutputs removeFacerecPersonResponse = new EOIBaseOutputs(EOIValidation.ValidateRemoveFacerecPersonInputs(personId));

            if (removeFacerecPersonResponse.Success)
            {
                string endPoint = $"{baseUrl}{facerecRemovePersonPath}";
                Log.Debug($"Calling {endPoint}");

                try
                {
                    var jsonData = JsonSerializer.Serialize(new { person_id = personId });
                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    removeFacerecPersonResponse = new EOIBaseOutputs(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"RemoveFacerecPerson: Exception: {exc.Message}");
                    removeFacerecPersonResponse = new EOIBaseOutputs(false, exc.Message);
                }
            }

            return removeFacerecPersonResponse;
        }

        public async Task<EOISearchFacerecNamesResponse> SearchFacerecGroupNames(String search)
        {
            EOISearchFacerecNamesResponse searchResponse = new EOISearchFacerecNamesResponse(EOIValidation.ValidateFacerecGroupNameSearch(search));

            if (searchResponse.Success)
            {
                string endPoint = $"{baseUrl}{facerecSearchGroupNamesPath}";

                try
                {
                    var jsonData = JsonSerializer.Serialize(new { search_text = search });
                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    searchResponse = new EOISearchFacerecNamesResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"Search: Exception: {exc.Message}");
                    searchResponse = new EOISearchFacerecNamesResponse(false, exc.Message);
                }
            }

            return searchResponse;
        }

        public async Task<EOISearchFacerecNamesResponse> SearchFacerecPeopleNames(String search)
        {
            EOISearchFacerecNamesResponse searchResponse = new EOISearchFacerecNamesResponse(EOIValidation.ValidateFacerecPeopleNameSearch(search));

            if (searchResponse.Success)
            {
                string endPoint = $"{baseUrl}{facerecSearchPeopleNamesPath}";

                try
                {
                    var jsonData = JsonSerializer.Serialize(new { search_text = search });
                    EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                    searchResponse = new EOISearchFacerecNamesResponse(eoiMessage);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"Search: Exception: {exc.Message}");
                    searchResponse = new EOISearchFacerecNamesResponse(false, exc.Message);
                }
            }

            return searchResponse;
        }

        public async Task<EOIFacerecPersonDetailsResponse> GetFacerecPersonDetails(String personId)
        {
            EOIFacerecPersonDetailsResponse eoiFacerecPersonDetailsResponse = new EOIFacerecPersonDetailsResponse(EOIValidation.ValidateFacerecPersonDetailsInputs(personId));

            string endPoint = $"{baseUrl}{facerecPersonDetailsPath}";

            Log.Debug($"Calling {endPoint}");

            try
            {
                var jsonData = JsonSerializer.Serialize(new { person_id = personId });
                EOIMessage eoiMessage = await PostAsync(endPoint, jsonData);
                eoiFacerecPersonDetailsResponse = new EOIFacerecPersonDetailsResponse(eoiMessage);
            }
            catch (HttpRequestException exc)
            {
                Log.Error($"GetFacerecGroups: Exception: {exc.Message}");
                eoiFacerecPersonDetailsResponse = new EOIFacerecPersonDetailsResponse(false, exc.Message);
            }

            return eoiFacerecPersonDetailsResponse;
        }

        private async Task<EOIMessage> GetAsync(string endPoint)
        {
            EOIMessage eoiMessage;
            string responseContent = null;

            try
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(endPoint);
                responseContent = await httpResponse.Content.ReadAsStringAsync();

                httpResponse.EnsureSuccessStatusCode();

                Log.Debug($"{endPoint} response: {responseContent}");

                eoiMessage = JsonSerializer.Deserialize<EOIMessage>(responseContent);
            }
            catch (HttpRequestException exc)
            {
                string innerExcMsg = exc.InnerException == null ? "" : exc.InnerException.Message;
                Log.Error($"GetAsync: HttpRequestException: {exc.Message} {innerExcMsg}");
                eoiMessage = new EOIMessage(false, responseContent ?? $"{exc.Message} {innerExcMsg}");
            }
            catch (Exception exc)
            {
                string innerExcMsg = exc.InnerException == null ? "" : exc.InnerException.Message;
                Log.Error($"GetAsync: Generic Exception: {exc.Message} {innerExcMsg}");
                eoiMessage = new EOIMessage(false, responseContent ?? $"{exc.Message} {innerExcMsg}");
            }

            return eoiMessage;
        }

        private async Task<EOIMessage> PostAsync(string endpoint, string jsonString)
        {
            EOIMessage eoiMessage = null;

            try
            {
                Log.Debug($"PostAsync: posting to {endpoint}. JSON = {jsonString}");

                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await httpClient.PostAsync(endpoint, content);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                string responseNoImage = this.RemoveImageProperties(responseContent);
                Log.Debug($"PostAsync: post to {endpoint}: response JSON = {responseNoImage}");

                eoiMessage = JsonSerializer.Deserialize<EOIMessage>(responseContent);

                httpResponse.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException exc)
            {
                string innerExcMsg = exc.InnerException == null ? "" : exc.InnerException.Message;
                Log.Error($"PostAsync: HttpRequestException: {exc.Message} {innerExcMsg}");

                eoiMessage = new EOIMessage(false, $"{exc.Message} {innerExcMsg}");
            }
            catch (Exception exc)
            {
                string innerExcMsg = exc.InnerException == null ? "" : exc.InnerException.Message;
                Log.Error($"PostAsync: Generic Exception: {exc.Message} {innerExcMsg}");

                eoiMessage = new EOIMessage(false, $"{exc.Message} {innerExcMsg}");
            }

            return eoiMessage;
        }

        private string RemoveImageProperties(string json)
        {
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                JsonElement root = doc.RootElement;
                JsonElement cleanedRoot = RemoveImageRecursive(root);

                return JsonSerializer.Serialize(cleanedRoot, new JsonSerializerOptions { WriteIndented = true });
            }
        }

        private JsonElement RemoveImageRecursive(JsonElement element)
        {
            using (var stream = new MemoryStream())
            using (var writer = new Utf8JsonWriter(stream))
            {
                if (element.ValueKind == JsonValueKind.Object)
                {
                    writer.WriteStartObject();
                    foreach (JsonProperty property in element.EnumerateObject())
                    {
                        if (property.Name != "image") // Skip "image" key
                        {
                            writer.WritePropertyName(property.Name);
                            RemoveImageRecursive(property.Value).WriteTo(writer);
                        }
                    }
                    writer.WriteEndObject();
                }
                else if (element.ValueKind == JsonValueKind.Array)
                {
                    writer.WriteStartArray();
                    foreach (JsonElement arrayItem in element.EnumerateArray())
                    {
                        RemoveImageRecursive(arrayItem).WriteTo(writer);
                    }
                    writer.WriteEndArray();
                }
                else
                {
                    element.WriteTo(writer);
                }

                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                using (JsonDocument newDoc = JsonDocument.Parse(stream))
                {
                    return newDoc.RootElement.Clone();
                }
            }
        }
    }
}
