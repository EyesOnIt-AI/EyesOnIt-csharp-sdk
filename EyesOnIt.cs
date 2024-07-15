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

namespace EyesOnItSDK
{
    public class EyesOnIt
    {
        private readonly HttpClient httpClient;
        private readonly string baseUrl;
        private readonly string processImagePath = "/process_image";
        private readonly string addStreamPath = "/add_stream";
        private readonly string processVideosPath = "/process_videos";
        private readonly string removeStreamPath = "/remove_stream";
        private readonly string monitorStreamPath = "/monitor_stream";
        private readonly string stopMonitorStreamPath = "/stop_monitoring";
        private readonly string getStreamsInfoPath = "/get_streams_info";
        private readonly string getBoundingBoxObjectsPath = "/get_bounding_box_objects";
        private readonly string getLastDetectionInfoPath = "/get_last_detection_info";
        private readonly string getPreviewVideoFramePath = "/get_preview_video_frame";
        private readonly string getVideoFramePath = "/get_video_frame";

        public EyesOnIt(string baseUrl)
        {
            httpClient = new HttpClient();
            this.baseUrl = baseUrl;
        }

        public string GetBaseUrl()
        {
            return this.baseUrl;
        }

        public async Task<EOIResponse> ProcessImageFromFile(string filePath, EOIRegion[] regions, int objectSize, EOIObjectDescription[] objectDescriptions)
        {
            return await this.ProcessImageFromFile(
                new EOIProcessImageInputs(null, regions, objectSize, objectDescriptions), 
                filePath);
        }

        public async Task<EOIResponse> ProcessImageFromFile(EOIProcessImageInputs inputs, string filePath)
        {
            EOIResponse eoiResponse = null;

            eoiResponse = EOIValidation.ValidateProcessImageInputs(inputs);

            if (eoiResponse.Success)
            {
                if (filePath == null || filePath.Length == 0)
                {
                    eoiResponse = new EOIResponse(false, $"filePath must not be null or empty.filePath = {filePath}");
                }
            }

            if (eoiResponse.Success && filePath != null)
            {
                // Read the image file as a byte array
                byte[] imageBytes = File.ReadAllBytes(filePath);

                // Convert the byte array to a Base64 encoded string
                string base64String = Convert.ToBase64String(imageBytes);

                EOIProcessImageInputs inputsWithImage = new EOIProcessImageInputs(base64String, inputs.Regions, inputs.ObjectSize, inputs.ObjectDescriptions);

                eoiResponse = await this.ProcessImage(inputsWithImage);
            }

            return eoiResponse;
        }

        public async Task<EOIResponse> ProcessImage(
                string base64Image,
                EOIRegion[] regions,
                int objectSize,
                EOIObjectDescription[] objectDescriptions
            )
        {
            EOIProcessImageInputs inputs = new EOIProcessImageInputs(base64Image, regions, objectSize, objectDescriptions);

            return await this.ProcessImage(inputs);
        }

        public async Task<EOIResponse> ProcessImage(EOIProcessImageInputs inputs)
        {
            EOIResponse eoiResponse = EOIValidation.ValidateProcessImageInputs(inputs);

            if (eoiResponse.Success)
            {
                if (inputs.Base64Image == null || inputs.Base64Image.Length == 0)
                {
                    eoiResponse = new EOIResponse(false, $"Base64Image must not be null or empty.filePath = {inputs.Base64Image}");
                }
            }

            if (eoiResponse.Success && inputs.Base64Image != null)
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

                    var jsonData = JsonSerializer.Serialize<EOIProcessImageInputs>(inputs, options);

                    eoiResponse = await PostAsync(endPoint, jsonData);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"InferFromImage: Exception: {exc.Message}");
                    eoiResponse = new EOIResponse(false, exc.Message);
                }
            }

            return eoiResponse;
        }

        public async Task<EOIResponse> GetStreamsInfo()
        {
            EOIResponse eoiResponse;

            string endPoint = $"{baseUrl}{getStreamsInfoPath}";

            Log.Debug($"Calling {endPoint}");

            try
            {
                eoiResponse = await GetAsync(endPoint);

                if (eoiResponse.Success && eoiResponse.Data != null)
                {
                    Log.Debug($"GetStreamsInfo response: {eoiResponse.Data.ToString()}");

                    eoiResponse.Data = EOIStreamInfo.FromJson(eoiResponse.Data.ToString());
                }
            }
            catch (HttpRequestException exc)
            {
                Log.Error($"GetStreamsInfo: Exception: {exc.Message}");
                eoiResponse = new EOIResponse(false, exc.Message);
            }

            return eoiResponse;
        }

        public async Task<EOIResponse> GetBoundingBoxObjects()
        {
            EOIResponse eoiResponse;

            string endPoint = $"{baseUrl}{getBoundingBoxObjectsPath}";

            Log.Debug($"Calling {endPoint}");

            try
            {
                eoiResponse = await GetAsync(endPoint);

                if (eoiResponse.Success && eoiResponse.Data != null)
                {
                    Log.Debug($"GetBoundingBoxObjects response: {eoiResponse.Data}");

                    EOIMessage eoiMessage = null;
                    eoiMessage = EOIMessage.FromJson(eoiResponse.Data.ToString());

                    if (eoiMessage != null)
                    {
                        eoiResponse.BoundingBoxObjects = eoiMessage.BoundingBoxObjects;
                    }

                    eoiResponse.Data = eoiResponse.Data.ToString();


                }
            }
            catch (HttpRequestException exc)
            {
                Log.Error($"GetStreamsInfo: Exception: {exc.Message}");
                eoiResponse = new EOIResponse(false, exc.Message);
            }

            return eoiResponse;
        }

        public async Task<EOIResponse> AddStream(
            string streamUrl,
            string name,
            EOIRegion[] regions,
            int objectSize,
            EOIObjectDescription[] objectDescriptions,
            EOIAlerting alerting,
            EOIMotionDetection motionDetection = null,
            EOIBoundingBox boundingBox = null,
            int? frameRate = 5)
        {
            EOIAddStreamInputs inputs = new EOIAddStreamInputs(streamUrl, name, regions, objectSize, objectDescriptions, alerting, motionDetection, boundingBox, frameRate);

            return await this.AddStream(inputs);
        }

        public async Task<EOIResponse> AddStream(EOIAddStreamInputs inputs)
        {
            EOIResponse eoiResponse = EOIValidation.ValidateAddStreamInputs(inputs);

            if (eoiResponse.Success)
            {
                string endPoint = $"{baseUrl}{addStreamPath}";

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize<EOIAddStreamInputs>(inputs, options);

                    eoiResponse = await PostAsync(endPoint, jsonData);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"AddStream: Exception: {exc.Message}");
                    eoiResponse = new EOIResponse(false, exc.Message);
                }
            }

            return eoiResponse;
        }

        public async Task<EOIResponse> ProcessVideos(EOIProcessVideosInputs inputs)
        {
            EOIResponse eoiResponse = EOIValidation.ValidateProcessVideosInputs(inputs);

            if (eoiResponse.Success)
            {
                string endPoint = $"{baseUrl}{processVideosPath}";

                try
                {
                    var options = new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };

                    var jsonData = JsonSerializer.Serialize<EOIProcessVideosInputs>(inputs, options);

                    Log.Debug($"Calling {endPoint} with this JSON: {jsonData}");

                    eoiResponse = await PostAsync(endPoint, jsonData);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"ProcessVideo: Exception: {exc.Message}");
                    eoiResponse = new EOIResponse(false, exc.Message);
                }
            }
            else
            {
                Log.Warning($"Validation of inputs failed. Message = {eoiResponse.Message}");
            }

            return eoiResponse;
        }

        public async Task<EOIResponse> MonitorStream(string streamUrl, int? durationSeconds)
        {
            return await this.MonitorStream(new EOIMonitorStreamInputs(streamUrl, durationSeconds));
        }

        public async Task<EOIResponse> MonitorStream(EOIMonitorStreamInputs inputs)
        {
            EOIResponse eoiResponse;

            string endPoint = $"{baseUrl}{monitorStreamPath}";

            Log.Debug($"Calling {endPoint}");

            try
            {
                var options = new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var jsonData = JsonSerializer.Serialize<EOIMonitorStreamInputs>(inputs, options);

                eoiResponse = await PostAsync(endPoint, jsonData);
            }
            catch (HttpRequestException exc)
            {
                Log.Error($"MonitorStream: Exception: {exc.Message}");
                eoiResponse = new EOIResponse(false, exc.Message);
            }

            return eoiResponse;
        }

        public async Task<EOIResponse> StopMonitoringStream(string streamUrl)
        {
            return await this.StopMonitoringStream(new EOIStopMonitoringStreamInputs(streamUrl));
        }

        public async Task<EOIResponse> StopMonitoringStream(EOIStopMonitoringStreamInputs inputs)
        {
            EOIResponse eoiResponse;

            string endPoint = $"{baseUrl}{stopMonitorStreamPath}";

            Log.Debug($"Calling {endPoint}");

            try
            {
                var jsonData = JsonSerializer.Serialize<EOIStopMonitoringStreamInputs>(inputs);

                eoiResponse = await PostAsync(endPoint, jsonData);
            }
            catch (HttpRequestException exc)
            {
                Log.Error($"StopMonitoringStream: Exception: {exc.Message}");
                eoiResponse = new EOIResponse(false, exc.Message);
            }

            return eoiResponse;
        }

        public async Task<EOIResponse> GetPreviewVideoFrame(string streamUrl)
        {
            return await this.GetPreviewVideoFrame(new EOIGetPreviewFrameInputs(streamUrl));
        }

        public async Task<EOIResponse> GetPreviewVideoFrame(EOIGetPreviewFrameInputs inputs)
        {
            EOIResponse getPreviewFrameResponse = EOIValidation.ValidateStreamUrl(inputs.StreamUrl);

            if (getPreviewFrameResponse.Success)
            {
                string endPoint = $"{baseUrl}{getPreviewVideoFramePath}";
                Log.Debug($"Calling {endPoint}");

                try
                {
                    var jsonData = JsonSerializer.Serialize<EOIGetPreviewFrameInputs>(inputs);

                    getPreviewFrameResponse = await PostAsync(endPoint, jsonData);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"GetPreviewFrame: Exception: {exc.Message}");
                    getPreviewFrameResponse = new EOIResponse(false, exc.Message);
                }
            }

            return getPreviewFrameResponse;
        }

        public async Task<EOIResponse> GetVideoFrame(string streamUrl)
        {
            return await this.GetVideoFrame(new EOIGetVideoFrameInputs(streamUrl));
        }

        public async Task<EOIResponse> GetVideoFrame(EOIGetVideoFrameInputs inputs)
        {
            EOIResponse getVideoFrameResponse = EOIValidation.ValidateStreamUrl(inputs.StreamUrl);

            if (getVideoFrameResponse.Success)
            {
                string endPoint = $"{baseUrl}{getVideoFramePath}";
                Log.Debug($"Calling {endPoint}");

                try
                {
                    var jsonData = JsonSerializer.Serialize<EOIGetVideoFrameInputs>(inputs);

                    getVideoFrameResponse = await PostAsync(endPoint, jsonData);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"GetVideoFrame: Exception: {exc.Message}");
                    getVideoFrameResponse = new EOIResponse(false, exc.Message);
                }
            }

            return getVideoFrameResponse;
        }

        public async Task<EOIResponse> GetLastDetectionInfo(string streamUrl)
        {
            return await this.GetLastDetectionInfo(new EOIGetLastDetectionInfoInputs(streamUrl));
        }

        public async Task<EOIResponse> GetLastDetectionInfo(EOIGetLastDetectionInfoInputs inputs)
        {
            EOIResponse getLastDetectionInfoResponse = EOIValidation.ValidateStreamUrl(inputs.StreamUrl);

            if (getLastDetectionInfoResponse.Success)
            {
                string endPoint = $"{baseUrl}{getLastDetectionInfoPath}";
                Log.Debug($"Calling {endPoint}");

                try
                {
                    var jsonData = JsonSerializer.Serialize<EOIGetLastDetectionInfoInputs>(inputs);

                    getLastDetectionInfoResponse = await PostAsync(endPoint, jsonData);

                    if (getLastDetectionInfoResponse.Success && getLastDetectionInfoResponse.Data != null)
                    {
                        getLastDetectionInfoResponse.Data = EOILastDetection.FromJson(getLastDetectionInfoResponse.Data.ToString());
                    }
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"GetLastDetectionInfo: Exception: {exc.Message}");
                    getLastDetectionInfoResponse = new EOIResponse(false, exc.Message);
                }
            }

            return getLastDetectionInfoResponse;
        }

        public async Task<EOIResponse> RemoveStream(string streamUrl)
        {
            return await this.RemoveStream(new EOIRemoveStreamInputs(streamUrl));
        }

        public async Task<EOIResponse> RemoveStream(EOIRemoveStreamInputs inputs)
        {
            EOIResponse removeStreamResponse = EOIValidation.ValidateStreamUrl(inputs.StreamUrl);

            if (removeStreamResponse.Success)
            {
                string endPoint = $"{baseUrl}{removeStreamPath}";
                Log.Debug($"Calling {endPoint}");

                try
                {
                    var jsonData = JsonSerializer.Serialize<EOIRemoveStreamInputs>(inputs);

                    removeStreamResponse = await PostAsync(endPoint, jsonData);
                }
                catch (HttpRequestException exc)
                {
                    Log.Error($"RemoveStream: Exception: {exc.Message}");
                    removeStreamResponse = new EOIResponse(false, exc.Message);
                }

            }

            return removeStreamResponse;
        }

        private async Task<EOIResponse> GetAsync(string endPoint)
        {
            EOIResponse eoiResponse;
            string responseContent = null;

            try
            {
                HttpResponseMessage httpResponse = await httpClient.GetAsync(endPoint);
                responseContent = await httpResponse.Content.ReadAsStringAsync();

                httpResponse.EnsureSuccessStatusCode();

                Log.Debug($"{endPoint} response: {responseContent}");

                eoiResponse = EOIResponse.DefaultSuccess();
                eoiResponse.Data = responseContent;
            }
            catch (HttpRequestException exc)
            {
                string innerExcMsg = exc.InnerException == null ? "" : exc.InnerException.Message;
                Log.Error($"GetAsync: HttpRequestException: {exc.Message} {innerExcMsg}");
                eoiResponse = new EOIResponse(false, responseContent ?? $"{exc.Message} {innerExcMsg}");
            }
            catch (Exception exc)
            {
                string innerExcMsg = exc.InnerException == null ? "" : exc.InnerException.Message;
                Log.Error($"GetAsync: Generic Exception: {exc.Message} {innerExcMsg}");
                eoiResponse = new EOIResponse(false, responseContent ?? $"{exc.Message} {innerExcMsg}");
            }

            return eoiResponse;
        }

        private async Task<EOIResponse> PostAsync(string endpoint, string jsonString)
        {
            EOIResponse eoiResponse;
            EOIMessage eoiMessage = null;
            string responseContent = null;

            try
            {
                Log.Debug($"PostAsync: posting to {endpoint}. JSON = {jsonString}");

                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponse = await httpClient.PostAsync(endpoint, content);
                responseContent = await httpResponse.Content.ReadAsStringAsync();

                Log.Debug($"PostAsync: post to {endpoint}: response JSON = {responseContent}");

                eoiMessage = EOIMessage.FromJson(responseContent);

                httpResponse.EnsureSuccessStatusCode();

                eoiResponse = EOIResponse.DefaultSuccess();
            }
            catch (HttpRequestException exc)
            {
                string innerExcMsg = exc.InnerException == null ? "" : exc.InnerException.Message;
                Log.Error($"PostAsync: HttpRequestException: {exc.Message} {innerExcMsg}");
                eoiResponse = EOIResponse.DefaultFailure();

                if (eoiMessage == null)
                {
                    eoiMessage = new EOIMessage();
                }

                eoiMessage.Message = $"{exc.Message} {innerExcMsg}";
            }
            catch (Exception exc)
            {
                string innerExcMsg = exc.InnerException == null ? "" : exc.InnerException.Message;
                Log.Error($"PostAsync: Generic Exception: {exc.Message} {innerExcMsg}");
                eoiResponse = EOIResponse.DefaultFailure();

                if (eoiMessage == null)
                {
                    eoiMessage = new EOIMessage();
                }

                eoiMessage.Message = $"{exc.Message} {innerExcMsg}";
            }

            if (eoiMessage != null)
            {
                eoiResponse.Message = eoiMessage.Message;
                eoiResponse.Detail = eoiMessage.Detail;
                eoiResponse.ConfidenceLevels = eoiMessage.JSON;
                eoiResponse.Image = eoiMessage.Image;
                eoiResponse.Data = eoiMessage.RawResponse;
            }

            if (eoiResponse != null && !eoiResponse.Success)
            {
                Log.Warning($"Failure calling EyesOnIt server: Message: {eoiResponse.Message}. Data: {eoiResponse.Data}");
            }

            return eoiResponse;
        }
    }
}


/*
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class ImageUploader
{
    private readonly HttpClient _httpClient;

    public ImageUploader()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> UploadImageAsync(string imagePath, string endpointUrl)
    {
        // Read the image file as a byte array
        byte[] imageBytes = File.ReadAllBytes(imagePath);

        // Convert the byte array to a Base64 encoded string
        string base64String = Convert.ToBase64String(imageBytes);

        // Create a JSON object with the Base64 string
        var requestData = new
        {
            image_base64 = base64String
        };

        // Serialize the JSON object
        var jsonContent = System.Text.Json.JsonSerializer.Serialize(requestData);

        // Create StringContent with the serialized JSON data
        var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

        // Send POST request with the Base64 encoded image data
        HttpResponseMessage response = await _httpClient.PostAsync(endpointUrl, content);
        response.EnsureSuccessStatusCode();

        // Read and return the response content
        string responseContent = await response.Content.ReadAsStringAsync();
        return responseContent;
    }
}

var uploader = new ImageUploader();
string imagePath = "path_to_your_image.jpg";
string endpointUrl = "your_rest_endpoint_url";
string response = await uploader.UploadImageAsync(imagePath, endpointUrl);

*/