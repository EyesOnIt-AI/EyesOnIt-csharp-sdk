using EyesOnItSDK;
using EyesOnItSDK.Data.Elements;
using EyesOnItSDK.Data.Inputs;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace EyesOnItSDK.Data.Inputs
{
    class EOIValidation
    {
        private static int MIN_STREAM_NAME_LENGTH = 3;
        private static int MIN_REGION_NAME_LENGTH = 3;
        private static int MIN_PHONE_NUMBER_LENGTH = 10;
        private static int MAX_PHONE_NUMBER_LENGTH = 20;
        private static int MIN_PROMPT_LENGTH = 1;
        private static int MIN_CONFIDENCE_THRESHOLD = 1;
        private static int MAX_CONFIDENCE_THRESHOLD = 99;
        private static int MIN_FRAME_RATE = 1;
        private static int MIN_LINE_VERTEX_COUNT = 2;
        private static string[] VALID_CLASS_NAMES = { "person", "vehicle", "bag", "animal", "unknown" };
        private static string[] VALID_DETECTION_TYPE_NAMES = { "class_name", "natural_language", "face_recognition", "similarity" };
        private static string[] VALID_FACE_REC_MATCH_TYPE_NAMES = { "person", "group", "all_faces" };
        private static string[] COUNT_CONDITION_TYPES = { "count_equals", "count_greater_than", "count_less_than" };
        private static string[] LINE_CROSS_CONDITION_TYPES = { "line_cross" };
        private static int MIN_LINE_NAME_LENGTH = 3;
        private static int MIN_OBJECT_SIZE = 100;
        private static float MIN_ALERT_SECONDS = 0.1F;
        private static float MIN_RESET_SECONDS = 0.1F;
        private static int MIN_CAMERA_UUID_LENGTH = 10;
        private static int MIN_MOTION_THRESHOLD = 10;
        private static int MIN_SEARCH_QUERY_LENGTH = 2;
        private static int MIN_SEED_ID_LENGTH = 10;
        private static int MIN_IMAGE_LENGTH = 100;
        private static int MIN_FACEREC_GROUP_NAME_LENGTH = 2;
        private static int MIN_FACEREC_PERSON_NAME_LENGTH = 2;
        private static int MIN_FACEREC_GROUP_ID_LENGTH = 2;
        private static int MIN_FACEREC_PERSON_ID_LENGTH = 2;
        private static int MIN_FACEREC_GROUP_DESCRIPTION_LENGTH = 10;
        private static int MIN_FACEREC_FILE_PATH_LENGTH = 5;
        private static int MIN_FACEREC_IMAGE_BASE64_LENGTH = 20;
        private static string MIN_SEARCH_DATE_ISO = "2020-01-01T00:00:00Z";
        private static DateTime MIN_SEARCH_DATE = DateTime.Parse(MIN_SEARCH_DATE_ISO);


        public static EOIResponse ValidateProcessImageInputs(EOIProcessImageInputs inputs)
        {
            EOIResponse response = ValidateBaseInputs(inputs, null, false, false);

            if (response.Success && (inputs.Base64Image == null || inputs.Base64Image.Length == 0))
            {
                response = new EOIResponse(false, $"Base64Image must not be null or empty");
            }

            return response;
        }

        public static EOIResponse ValidateAddStreamInputs(EOIAddStreamInputs inputs)
        {
            EOIResponse response;

            if (inputs == null)
            {
                response = new EOIResponse(false, "inputs = null. Add stream request must include inputs");
            }
            else
            {
                response = ValidateLines(inputs.Lines);

                if (response.Success)
                {
                    response = ValidateSearchIndexing(inputs);
                }

                bool validSearchIndexInputs = response.Success && inputs.IndexForSearch;

                if (response.Success)
                {
                    response = ValidateBaseInputs(inputs, inputs.Lines, true, validSearchIndexInputs);
                }

                if (response.Success)
                {
                    response = ValidateStreamUrl(inputs.StreamUrl);
                }

                if (response.Success)
                {
                    string name = inputs.Name == null ? "" : inputs.Name.Trim();

                    if (name.Length < MIN_STREAM_NAME_LENGTH)
                    {
                        response = new EOIResponse(false, $"Stream name must be at least {MIN_STREAM_NAME_LENGTH} characters long. Stream name is {inputs.Name}");
                    }
                }

                if (response.Success)
                {
                    response = ValidateFrameRate(inputs.FrameRate);
                }

                if (response.Success)
                {
                    response = ValidateNotification(inputs.Notification);
                }
            }

            return response;
        }

        public static EOIResponse ValidateProcessVideosInputs(EOIProcessVideosInputs inputs)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (inputs == null)
            {
                response = new EOIResponse(false, "inputs = null. Process video request must include inputs");
            }
            else
            {
                if (response.Success)
                {
                    response = ValidateSearchIndexing(inputs);
                }

                bool validSearchIndexInputs = response.Success && inputs.IndexForSearch;

                response = ValidateBaseInputs(inputs, inputs.Lines, true, validSearchIndexInputs);

                if (response.Success)
                {
                    response = ValidateInputVideoFiles(inputs.InputVideoFiles);
                }

                // Video output file is optional for now
                //if (response.Success)
                //{
                //    response = ValidateOutputVideoFile(inputs.OutputVideoFile);
                //}

                if (response.Success && inputs.StartSeconds != null && inputs.StartSeconds < 0)
                {
                    response = new EOIResponse(false, $"If specified, StartSeconds must be at least 0. StartSeconds = {inputs.StartSeconds}");
                }

                if (response.Success && inputs.EndSeconds != null && inputs.EndSeconds < 1)
                {
                    response = new EOIResponse(false, $"If specified, EndSeconds must be at least 1. EndSeconds = {inputs.EndSeconds}");
                }

                if (response.Success && inputs.StartSeconds != null && inputs.EndSeconds != null && inputs.EndSeconds <= inputs.StartSeconds)
                {
                    response = new EOIResponse(false, $"If specified, EndSeconds must be greater than StartSeconds. StartSeconds = {inputs.StartSeconds}. EndSeconds = {inputs.EndSeconds}");
                }

                if (response.Success)
                {
                    response = ValidateFrameRate(inputs.FrameRate);
                }
            }

            return response;
        }
        public static EOIResponse ValidateSearchInputs(EOISearchInputs inputs)
        {
            EOIResponse response = inputs == null ?
                new EOIResponse(false, "Search request must include inputs")
                : EOIResponse.DefaultSuccess();

            if (response.Success)
            {
                string trimmedObjectDescription = inputs.ObjectDescription?.Trim();
                bool objDescValid = trimmedObjectDescription != null && trimmedObjectDescription.Length >= EOIValidation.MIN_SEARCH_QUERY_LENGTH;
                bool faceRecognitionValid = ValidateFaceRecognitionConfig(inputs.FaceMatchType, inputs.FacePersonId, inputs.FaceGroupId).Success;
                bool similarityValid = ValidateSimilarityConfig(inputs.Similarity).Success;

                if (!objDescValid && !faceRecognitionValid && !similarityValid)
                {
                    response = new EOIResponse(false, "Search must include a valid object description, face recognition configuration, or similarity configuration");
                }
            }

            return response;
        }

        public static EOIResponse ValidateArchiveSearchInputs(EOIArchiveSearchInputs inputs)
        {
            EOIResponse response = ValidateSearchInputs(inputs);

            if (response.Success)
            {
                response = ValidateSearchDateRange(inputs.StartDateTime, inputs.EndDateTime);
            }

            return response;
        }

        public static EOIResponse ValidateLiveSearchInputs(EOILiveSearchInputs inputs)
        {
            EOIResponse response = ValidateSearchInputs(inputs);

            if (response.Success)
            {
                if (inputs.SearchType == null || !EOIValidation.VALID_DETECTION_TYPE_NAMES.Contains(inputs.SearchType.Trim()))
                {
                    response = new EOIResponse(false, $"In search configuration, search type is not valid. Search type is {inputs.SearchType}");
                }
                else if (inputs.SearchType.Trim() == "natural_lanuage")
                {
                    if (inputs.ClassName != null && !EOIValidation.VALID_CLASS_NAMES.Contains(inputs.ClassName.Trim()))
                    {
                        response = new EOIResponse(false, $"In search configurations, class name is not valid. Class name is {inputs.ClassName}");
                    }
                    else
                    {
                        string trimmedText = inputs.ObjectDescription?.Trim();

                        if (trimmedText == null || trimmedText.Length < EOIValidation.MIN_SEARCH_QUERY_LENGTH)
                        {
                            response = new EOIResponse(false, $"Live search text must be at least {EOIValidation.MIN_SEARCH_QUERY_LENGTH} character(s). Search text = {trimmedText}");
                        }
                    }
                }
                else if (inputs.SearchType.Trim() == "face_recognition")
                {
                    response = ValidateFaceRecognitionConfig(inputs.FaceMatchType, inputs.FacePersonId, inputs.FaceGroupId);
                }
                else if (inputs.SearchType.Trim() == "similarity")
                {
                    response = ValidateSimilarityConfig(inputs.Similarity);
                }
            }

            if (response.Success)
            {
                response = inputs.AlertThreshold > 0 && inputs.AlertThreshold < 100
                    ? EOIResponse.DefaultSuccess()
                    : new EOIResponse(false, $"Live search threshold must be greater than 0 and less than 100. Value is {inputs.AlertThreshold}");
            }

            if (response.Success)
            {
                response = ValidateNotification(inputs.Notification);
            }

            return response;
        }

        public static EOIResponse ValidatePauseLiveSearchInputs(EOIPauseLiveSearchInputs inputs)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (inputs == null)
            {
                response = new EOIResponse(false, "inputs = null. Pause live search request must include inputs");
            }
            else
            {
                response = inputs.SearchID == -1 || inputs.SearchID > 0 ?
                    EOIResponse.DefaultSuccess() :
                    new EOIResponse(false, $"live search ID must be greater than 0. Value is {inputs.SearchID}");
            }

            return response;
        }

        public static EOIResponse ValidateResumeLiveSearchInputs(EOIResumeLiveSearchInputs inputs)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (inputs == null)
            {
                response = new EOIResponse(false, "inputs = null. Resume live search request must include inputs");
            }
            else
            {
                response = inputs.SearchID == -1 || inputs.SearchID > 0 ?
                    EOIResponse.DefaultSuccess() :
                    new EOIResponse(false, $"live search ID must be greater than 0. Value is {inputs.SearchID}");
            }

            return response;
        }

        public static EOIResponse ValidateCancelLiveSearchInputs(EOICancelLiveSearchInputs inputs)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (inputs == null)
            {
                response = new EOIResponse(false, "inputs = null. Cancel live search request must include inputs");
            }
            else
            {
                response = inputs.SearchID == -1 || inputs.SearchID > 0 ?
                    EOIResponse.DefaultSuccess() :
                    new EOIResponse(false, $"live search ID must be greater than 0. Value is {inputs.SearchID}");
            }

            return response;
        }

        private static EOIResponse ValidateBaseInputs(
            EOIBaseInputs inputs,
            EOILine[] lines,
            bool validateForVideo,
            bool validSearchIndexInputs)
        {
            EOIResponse response;

            if (inputs == null)
            {
                response = new EOIResponse(false, "inputs = null. Request must include inputs");
            }
            else
            {
                response = ValidateRegions(inputs.Regions, lines, validateForVideo, validSearchIndexInputs);
            }

            return response;
        }

        public static EOIResponse ValidateRegions(
            EOIRegion[] regions,
            EOILine[] lines,
            bool validateForVideo,
            bool validSearchIndexInputs)
        {
            EOIResponse response = regions == null || regions.Length == 0 ?
                new EOIResponse(false, "request must include one or more region configurations")
                : EOIResponse.DefaultSuccess();

            if (response.Success && regions != null)
            {
                foreach (var region in regions)
                {
                    if (response.Success)
                    {
                        string name = region.Name == null ? "" : region.Name.Trim();

                        if (name.Length < MIN_REGION_NAME_LENGTH)
                        {
                            response = new EOIResponse(false, $"Region name must be at least {MIN_REGION_NAME_LENGTH} characters long. Region name is {region.Name}");
                        }
                    }

                    if (response.Success)
                    {
                        response = ValidatePolygon(region.Polygon);
                    }

                    if (response.Success && validateForVideo)
                    {
                        response = ValidateMotionDetection(region.MotionDetection);
                    }

                    if (response.Success)
                    {
                        response = ValidateDetectionConfigs(region.DetectionConfigs, lines, validateForVideo, validSearchIndexInputs);
                    }

                }
            }

            return response;
        }

        public static EOIResponse ValidatePolygon(EOIVertex[] polygon)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (polygon == null || polygon.Length < 3)
            {
                response = new EOIResponse(false, "Polygon must contain at least 3 vertices");
            }
            else
            {
                response = ValidateVertexArray(polygon);
            }

            return response;
        }

        private static EOIResponse ValidateVertexArray(EOIVertex[] vertices)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            foreach (var vertex in vertices)
            {
                if (response.Success)
                {
                    if (vertex.X < 0 || vertex.Y < 0)
                    {
                        response = new EOIResponse(false, $"The vertex x and y values cannot be negative");
                        break;
                    }
                }
            }

            return response;
        }

        public static EOIResponse ValidateMotionDetection(EOIMotionDetection motionDetection)
        {
            EOIResponse response = motionDetection == null ?
                new EOIResponse(false, "request must include motion_detection configuration")
                : EOIResponse.DefaultSuccess();

            if (response.Success && motionDetection != null)
            {
                if (motionDetection.Enabled)
                {
                    if (motionDetection.DetectionThreshold < MIN_MOTION_THRESHOLD)
                    {
                        response = new EOIResponse(false, $"motion detection threshold should be at least {MIN_MOTION_THRESHOLD}. DetectionThreshold = {motionDetection.DetectionThreshold}");
                    }
                    else if (motionDetection.RegularCheckFrameInterval < 1)
                    {
                        response = new EOIResponse(false, $"motion detection regular check interval should be at least 1. RegularCheckFrameInterval = {motionDetection.RegularCheckFrameInterval}");
                    }
                }
            }

            return response;
        }

        public static EOIResponse ValidateDetectionConfigs(
            EOIDetectionConfig[] detectionConfigs, 
            EOILine[] lines, 
            bool validateForVideo, 
            bool validSearchIndexInputs)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (detectionConfigs == null || detectionConfigs.Length == 0)
            {
                if (!validSearchIndexInputs)
                {
                    response = new EOIResponse(false, $"A region must have a detection configuration if the stream is not indexed for search");
                }
                else
                {
                    // this case is valid - valid search index inputs but no detection config. No more validation needed.
                }
            }
            else
            {
                foreach (var detectionConfig in detectionConfigs)
                {
                    if (response.Success)
                    {
                        if (detectionConfig.ClassName != null && detectionConfig.ClassThreshold == null)
                        {
                            response = new EOIResponse(false, "In detection configurations, if a class name is specified, a class threshold must also be specified.");
                        }
                        else if (detectionConfig.ObjectSize != null && detectionConfig.ObjectSize < EOIValidation.MIN_OBJECT_SIZE)
                        {
                            response = new EOIResponse(false, $"In detection configurations, the object size should be at least {EOIValidation.MIN_OBJECT_SIZE}. object_size = {detectionConfig.ObjectSize}");
                        }

                        if (detectionConfig.ClassName != null && !EOIValidation.VALID_CLASS_NAMES.Contains(detectionConfig.ClassName.Trim()))
                        {
                            response = new EOIResponse(false, $"In detection configurations, class name is not valid. Class name is {detectionConfig.ClassName}");
                        }
                        else
                        {
                            response = ValidateObjectDescriptions(detectionConfig.ObjectDescriptions, validateForVideo);
                        }

                        if (response.Success && detectionConfig.FaceRecognition != null)
                        {
                            response = ValidateFaceRecognitionConfig(detectionConfig.FaceRecognition.MatchType, detectionConfig.FaceRecognition.Person, detectionConfig.FaceRecognition.Group);
                        }

                        if (response.Success && detectionConfig.Similarity != null)
                        {
                            response = ValidateSimilarityConfig(detectionConfig.Similarity);
                        }

                        if (response.Success && validateForVideo)
                        {
                            if (detectionConfig.AlertSeconds < EOIValidation.MIN_ALERT_SECONDS)
                            {
                                response = new EOIResponse(false, $"In detection configurations, alert seconds must be at least {EOIValidation.MIN_ALERT_SECONDS}. alert_seconds = {detectionConfig.AlertSeconds}");
                            }
                            else if (detectionConfig.ResetSeconds < EOIValidation.MIN_RESET_SECONDS)
                            {
                                response = new EOIResponse(false, $"In detection configurations, reset seconds must be at least {EOIValidation.MIN_RESET_SECONDS}. reset_seconds = {detectionConfig.ResetSeconds}");
                            }
                            else
                            {
                                if (response.Success)
                                {
                                    response = ValidateConditions(detectionConfig.DetectionConditions, lines);
                                }
                            }
                        }
                    }
                }
            }

            return response;
        }

        public static EOIResponse ValidateObjectDescriptions(EOIObjectDescription[] objectDescriptions, bool validateForVideo)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            // validate each object description - minimum length, no duplicates, thresholds
            if (response.Success && objectDescriptions != null && objectDescriptions.Length > 0)
            {
                var textSet = new HashSet<string>();

                foreach (var objectDescription in objectDescriptions)
                {
                    if (response.Success)
                    {
                        var trimmedText = objectDescription.Text?.Trim();

                        if (trimmedText == null || trimmedText.Length < EOIValidation.MIN_PROMPT_LENGTH)
                        {
                            response = new EOIResponse(false, $"Object description text must be at least {EOIValidation.MIN_PROMPT_LENGTH} character(s). Object description text = {trimmedText}");
                        }
                        else
                        {
                            if (textSet.Contains(trimmedText))
                            {
                                response = new EOIResponse(false, $"duplicate object description found: {trimmedText}");
                            }
                            else
                            {
                                textSet.Add(trimmedText);
                            }
                        }

                        if (response.Success && objectDescription.Alert == true && validateForVideo)
                        {
                            if (objectDescription.Threshold < EOIValidation.MIN_CONFIDENCE_THRESHOLD || objectDescription.Threshold > EOIValidation.MAX_CONFIDENCE_THRESHOLD)
                            {
                                response = new EOIResponse(false, $"The object description alerting threshold must be between {EOIValidation.MIN_CONFIDENCE_THRESHOLD} and {EOIValidation.MAX_CONFIDENCE_THRESHOLD}. The value for object description '{objectDescription.Text}' is ${objectDescription.Threshold}");
                            }
                        }
                    }
                }

            }

            return response;
        }

        public static EOIResponse ValidateSearchIndexing(EOIBaseVideoInputs inputs)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (inputs.IndexForSearch)
            {
                if (inputs.SearchIndexTypes == null || inputs.SearchIndexTypes.Length == 0)
                {
                    response = new EOIResponse(false, $"SearchIndexTypes must be specified when IndexForSearch = true. SearchIndexTypes = {inputs.SearchIndexTypes}");
                }
                else
                {
                    foreach (var searchIndexType in inputs.SearchIndexTypes)
                    {
                        if (!VALID_CLASS_NAMES.Contains(searchIndexType.Trim()))
                        {
                            response = new EOIResponse(false, $"SearchIndexTypes contains invalid class {searchIndexType}. See documentation at https://developer.eyesonit.us/documentation for valid class names.");
                        }
                    }
                }
            }

            return response;
        }

        public static EOIResponse ValidateLines(EOILine[] lines)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (lines != null && lines.Length > 0)
            {
                var lineNameTextSet = new HashSet<string>();

                foreach (var line in lines)
                {
                    if (response.Success)
                    {
                        var trimmedText = line.Name?.Trim().ToLower();

                        if (trimmedText == null || trimmedText.Length < MIN_LINE_NAME_LENGTH)
                        {
                            response = new EOIResponse(false, $"Line names must be at least {MIN_LINE_NAME_LENGTH} characters. The line name {trimmedText} is not valid.");
                        }
                        else if (lineNameTextSet.Contains(trimmedText))
                        {
                            response = new EOIResponse(false, $"Duplicate line name found: {trimmedText}");
                        }
                        else
                        {
                            lineNameTextSet.Add(trimmedText);
                        }

                        if (response.Success)
                        {
                            if (line.Vertices == null || line.Vertices.Length < MIN_LINE_VERTEX_COUNT)
                            {
                                response = new EOIResponse(false, $"Each line must have at least {MIN_LINE_VERTEX_COUNT} vertices. The line with name {line.Name} has {line.Vertices.Length} vertices.");
                            }

                            if (response.Success)
                            {
                                response = ValidateVertexArray(line.Vertices);
                            }
                        }
                    }
                }
            }

            return response;
        }

        public static EOIResponse ValidateConditions(EOIDetectionCondition[] detectionConditions, EOILine[] lines)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (detectionConditions != null && detectionConditions.Length > 0)
            {
                var countConditionTextSet = new HashSet<string>();

                foreach (var detectionCondition in detectionConditions)
                {
                    if (response.Success)
                    {
                        var trimmedText = detectionCondition.Type?.Trim().ToLower();

                        if (COUNT_CONDITION_TYPES.Contains(trimmedText))
                        {
                            if (countConditionTextSet.Contains(trimmedText))
                            {
                                response = new EOIResponse(false, $"Duplicate detection condition found: {trimmedText}");
                            }
                            else
                            {
                                countConditionTextSet.Add(trimmedText);
                            }

                            if (response.Success)
                            {
                                if (detectionCondition.Count < 0)
                                {
                                    response = new EOIResponse(false, $"The detection condition count must be at least 0");
                                }
                            }
                        }
                        else if (LINE_CROSS_CONDITION_TYPES.Contains(trimmedText))
                        {
                            var lineNameSet = new HashSet<string>();

                            if (lines != null)
                            {
                                foreach (var line in lines)
                                {
                                    lineNameSet.Add(line.Name);
                                }
                            }

                            if (detectionCondition.LineName == null || !lineNameSet.Contains(detectionCondition.LineName))
                            {
                                response = new EOIResponse(false, $"The line_name for line_cross conditions must mach a line name defined in the lines array. The line name {detectionCondition.LineName} does not match any line names.");
                            }
                        }
                    }
                }
            }

            return response;
        }

        /*
        public static EOIResponse ValidateRegions(EOIRegion[] regions, bool validateThresholds)
        {
            EOIResponse response = regions == null || regions.Length == 0 ?
                new EOIResponse(false, $"request must include an array of regions. regions = {regions}")
                : EOIResponse.DefaultSuccess();

            // validate each object description - minimum length, no duplicates, thresholds
            if (response.Success && objectDescriptions != null)
            {
                var textSet = new HashSet<string>();

                foreach (var objectDescription in objectDescriptions)
                {
                    if (response.Success)
                    {
                        var trimmedText = objectDescription.Text == null ? null : objectDescription.Text.Trim();

                        if (trimmedText == null || trimmedText.Length < EOIValidation.MIN_PROMPT_LENGTH)
                        {
                            response = new EOIResponse(false, $"Object description text must be at least {EOIValidation.MIN_PROMPT_LENGTH} character(s). Object description text = {trimmedText}");
                        }
                        else
                        {
                            if (textSet.Contains(trimmedText))
                            {
                                response = new EOIResponse(false, $"duplicate object description found: {trimmedText}");
                            }
                            else
                            {
                                textSet.Add(trimmedText);
                            }
                        }

                        if (validateThresholds)
                        {
                            if (objectDescription.Threshold < EOIValidation.MIN_CONFIDENCE_THRESHOLD || objectDescription.Threshold > EOIValidation.MAX_CONFIDENCE_THRESHOLD)
                            {
                                response = new EOIResponse(false, $"The object description alerting threshold must be between {EOIValidation.MIN_CONFIDENCE_THRESHOLD} and {EOIValidation.MAX_CONFIDENCE_THRESHOLD}. The value for object description '{objectDescription.Text}' is ${objectDescription.Threshold}");
                            }
                        }
                    }
                }

            }

            return response;
        }
        */

        public static EOIResponse ValidateStreamUrl(string streamUrl)
        {
            var trimmedUrl = streamUrl == null ? null : streamUrl.Trim();

            return trimmedUrl != null && trimmedUrl.Length > 0 ?
                EOIResponse.DefaultSuccess()
                : new EOIResponse(false, "The stream url must be a valid RTSP URL");
        }

        public static EOIResponse ValidateFrameRate(int? frameRate)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (frameRate == null)
            {
                response = new EOIResponse(false, $"The frame rate must be provided");
            }
            else if (frameRate < MIN_FRAME_RATE)
            {
                response = new EOIResponse(false, $"The frame rate must be at least {MIN_FRAME_RATE}. FrameRate = {frameRate}");
            }

            return response;
        }

        public static EOIResponse ValidateInputVideoFiles(string[] inputVideoFiles)
        {
            EOIResponse response = inputVideoFiles == null || inputVideoFiles.Length == 0 ?
                new EOIResponse(false, "request must include at least one input video file")
                : EOIResponse.DefaultSuccess();

            return response;
        }

        public static EOIResponse ValidateOutputVideoFile(string outputVideoFiles)
        {
            EOIResponse response = outputVideoFiles == null ?
                new EOIResponse(false, "request must include an output video file")
                : EOIResponse.DefaultSuccess();

            return response;
        }

        public static EOIResponse ValidateNotification(EOINotification notification)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (notification != null)
            {
                response = EOIValidation.ValidatePhoneNumber(notification.PhoneNumber);

                if (response.Success)
                {
                    response = EOIValidation.ValidateGenetecNotification(notification.GenetecNotification);
                }

                if (response.Success)
                {
                    response = EOIValidation.ValidateRESTUrl(notification.RESTUrl);
                }
            }

            return response;

        }

        public static EOIResponse ValidateFacerecGroupNameSearch(string search)
        {
            var trimmedSearch = search == null ? null : search.Trim();

            return trimmedSearch != null && trimmedSearch.Length > 0 ?
                EOIResponse.DefaultSuccess()
                : new EOIResponse(false, $"The group name search string {trimmedSearch} must be at least 1 character");
        }

        public static EOIResponse ValidateFacerecPeopleNameSearch(string search)
        {
            var trimmedSearch = search == null ? null : search.Trim();

            return trimmedSearch != null && trimmedSearch.Length > 0 ?
                EOIResponse.DefaultSuccess()
                : new EOIResponse(false, $"The person name search string {trimmedSearch} must be at least 1 character");
        }

        public static EOIResponse ValidateRemoveFacerecGroupInputs(string groupId)
        {
            var trimmedId = groupId == null ? null : groupId.Trim();

            return trimmedId != null && trimmedId.Length >= MIN_FACEREC_GROUP_ID_LENGTH ?
                EOIResponse.DefaultSuccess()
                : new EOIResponse(false, $"The group ID {trimmedId} must be at least {MIN_FACEREC_GROUP_ID_LENGTH} character(s)");
        }

        public static EOIResponse ValidateNewFacerecGroup(EOIAddFacerecGroupInputs inputs)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (response.Success)
            {
                var trimmedGroupId = inputs.GroupId?.Trim();
                if (trimmedGroupId == null || trimmedGroupId.Length < MIN_FACEREC_GROUP_ID_LENGTH)
                {
                    response = new EOIResponse(false, $"The group ID {trimmedGroupId} must be at least {MIN_FACEREC_GROUP_ID_LENGTH} character(s)");
                }
            }

            if (response.Success)
            {
                var trimmedGroupName = inputs.GroupName?.Trim();
                if (trimmedGroupName == null || trimmedGroupName.Length < MIN_FACEREC_GROUP_NAME_LENGTH)
                {
                    response = new EOIResponse(false, $"The group name {trimmedGroupName} must be at least {MIN_FACEREC_GROUP_NAME_LENGTH} character(s)");
                }
            }

            if (response.Success)
            {
                var trimmedGroupDescription = inputs.GroupDescription?.Trim();
                if (trimmedGroupDescription == null || trimmedGroupDescription.Length < MIN_FACEREC_GROUP_DESCRIPTION_LENGTH)
                {
                    response = new EOIResponse(false, $"The group description {trimmedGroupDescription} must be at least {MIN_FACEREC_GROUP_DESCRIPTION_LENGTH} character(s)");
                }
            }

            return response;
        }

        public static EOIResponse ValidateNewFacerecPerson(EOIAddFacerecPersonInputs inputs)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (response.Success)
            {
                var trimmedPersonId = inputs.PersonId?.Trim();
                if (trimmedPersonId == null || trimmedPersonId.Length < MIN_FACEREC_PERSON_ID_LENGTH)
                {
                    response = new EOIResponse(false, $"The person ID {trimmedPersonId} must be at least {MIN_FACEREC_PERSON_ID_LENGTH} character(s)");
                }
            }

            if (response.Success)
            {
                var trimmedPersonName = inputs.PersonDisplayName?.Trim();
                if (trimmedPersonName == null || trimmedPersonName.Length < MIN_FACEREC_PERSON_NAME_LENGTH)
                {
                    response = new EOIResponse(false, $"The person name {trimmedPersonName} must be at least {MIN_FACEREC_PERSON_NAME_LENGTH} character(s)");
                }
            }

            if (response.Success)
            {
                if (inputs.PersonGroups != null && inputs.PersonGroups.Length > 0)
                {
                    foreach (var personGroup in inputs.PersonGroups)
                    {
                        if (response.Success)
                        {
                            var trimmedGroupId = personGroup.Trim();

                            if (trimmedGroupId == null || trimmedGroupId.Length < MIN_FACEREC_GROUP_ID_LENGTH)
                            {
                                response = new EOIResponse(false, $"The group ID {trimmedGroupId} must be at least {MIN_FACEREC_GROUP_ID_LENGTH} character(s)");
                            }
                        }
                    }
                }
            }

            if (response.Success)
            {
                var imageCount = 0;

                if (inputs.PersonImages != null && inputs.PersonImages.Count() > 0)
                {
                    foreach (var personImage in inputs.PersonImages)
                    {
                        if (response.Success)
                        {
                            if (personImage.Image == null || personImage.Image.Trim().Length < MIN_FACEREC_IMAGE_BASE64_LENGTH)
                            {
                                response = new EOIResponse(false, "Please provide a valid base64 image string");
                            }
                            else if (personImage.FilePath == null || personImage.FilePath.Trim().Length < MIN_FACEREC_FILE_PATH_LENGTH)
                            {
                                response = new EOIResponse(false, "Please provide a valid file path");
                            }
                            else
                            {
                                imageCount++;
                            }
                        }
                    }
                }

                if (imageCount == 0)
                {
                    response = new EOIResponse(false, "Please provide at least one image as base64 or as a file path");
                }
            }

            return response;
        }
        public static EOIResponse ValidateAddFacerecPeople(EOIAddFacerecPeopleInputs inputs)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (response.Success)
            {
                var trimmedFilePath = inputs.FilePath?.Trim();
                if (trimmedFilePath == null || trimmedFilePath.Length < MIN_FACEREC_FILE_PATH_LENGTH)
                {
                    response = new EOIResponse(false, "Please provide a valid file path");
                }
            }

            return response;
        }

        public static EOIResponse ValidateRemoveFacerecPersonInputs(string personId)
        {
            var trimmedId = personId == null ? null : personId.Trim();

            return trimmedId != null && trimmedId.Length >= MIN_FACEREC_PERSON_ID_LENGTH ?
                EOIResponse.DefaultSuccess()
                : new EOIResponse(false, $"The person ID {trimmedId} must be at least {MIN_FACEREC_PERSON_ID_LENGTH} character(s)");
        }

        public static EOIResponse ValidateFacerecPersonDetailsInputs(string personId)
        {
            var trimmedId = personId == null ? null : personId.Trim();

            return trimmedId != null && trimmedId.Length >= MIN_FACEREC_PERSON_ID_LENGTH ?
                EOIResponse.DefaultSuccess()
                : new EOIResponse(false, $"The person ID {trimmedId} must be at least {MIN_FACEREC_PERSON_ID_LENGTH} character(s)");
        }

        private static EOIResponse ValidatePhoneNumber(string phoneNumber)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            var trimmedPhoneNumber = phoneNumber?.Trim();

            if (trimmedPhoneNumber != null)
            {
                if (trimmedPhoneNumber.Length < MIN_PHONE_NUMBER_LENGTH)
                {
                    response = new EOIResponse(false, $"If specified, phone number must be at least {MIN_PHONE_NUMBER_LENGTH} characters. Phone number is {phoneNumber}");
                }
                else if (trimmedPhoneNumber.Length > MAX_PHONE_NUMBER_LENGTH)
                {
                    response = new EOIResponse(false, $"If specified, phone number maximum length is {EOIValidation.MAX_PHONE_NUMBER_LENGTH}.");
                }
                else if (!trimmedPhoneNumber.StartsWith("+"))
                {
                    response = new EOIResponse(false, "The phone number must start with a country code like + 1");
                }
            }

            return response;
        }

        private static EOIResponse ValidateGenetecNotification(EOIGenetecNotification genetecNotification)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (genetecNotification != null)
            {
                // genetecNotification.WebhookEventId is optional now
                //if (genetecNotification.WebhookEventId == null)
                //{
                //    response = new EOIResponse(false, $"If genetec notification is included, webhook event ID must be specified. WebhookEventId is null.");
                //}
                //else 
                //if (genetecNotification.WebhookCameraUUID == null || genetecNotification.WebhookCameraUUID.Length < MIN_CAMERA_UUID_LENGTH)
                //{
                //    response = new EOIResponse(false, $"If genetec notification is included, the webhook camera uuid must be specified with a minimum length of {MIN_CAMERA_UUID_LENGTH} characters. WebhookCameraUUID = {genetecNotification.WebhookCameraUUID}");
                //}
            }

            return response;
        }

        private static EOIResponse ValidateRESTUrl(string restUrl)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            var trimmedRestUrl = restUrl?.Trim();

            if (trimmedRestUrl != null)
            {
                bool success = false;

                if (Uri.TryCreate(restUrl, UriKind.Absolute, out Uri uriResult))
                {
                    success = (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                }

                if (!success)
                {
                    response = new EOIResponse(false, "The provided REST URL is not a valid URL");
                }
            }

            return response;
        }


        private static EOIResponse ValidateSearchDateRange(string startDateTime, string endDateTime)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            string startDateText = startDateTime == null ? null : startDateTime.Trim();
            string endDateText = endDateTime == null ? null : endDateTime.Trim();

            bool startProvided = startDateText != null && startDateText.Length > 0;
            bool endProvided = endDateText != null && endDateText.Length > 0;

            DateTime? startDate = null;
            DateTime? endDate = null;

            if (startProvided)
            {
                if (!DateTime.TryParse(startDateText, out DateTime parsedDate))
                {
                    startDate = parsedDate;
                    response = new EOIResponse(false, $"Start datetime must be a valid datetime. Start = {startDateText}");
                }

                if (response.Success && (startDate < MIN_SEARCH_DATE))
                {
                    response = new EOIResponse(false, $"Start datetime must be on or after {MIN_SEARCH_DATE_ISO}. Start = {startDateText}");
                }
            }

            if (response.Success && endProvided)
            {
                if (!DateTime.TryParse(endDateText, out DateTime parsedDate))
                {
                    endDate = parsedDate;
                    response = new EOIResponse(false, $"End datetime must be a valid datetime. End = {endDateText}");
                }

                if (response.Success && (endDate < MIN_SEARCH_DATE))
                {
                    response = new EOIResponse(false, $"End datetime must be on or after {MIN_SEARCH_DATE_ISO}. End = {endDateText}");
                }
            }

            if (response.Success && startDate != null && endDate != null && startDate >= endDate)
            {
                response = new EOIResponse(false, $"Start datetime must be before end datetime. Start = {startDateText}; End = {endDateText}");
            }

            return response;
        }
        public static EOIResponse ValidateFaceRecognitionConfig(string matchType, string personId, string groupId = null)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            // validate match type
            if (response.Success)
            {
                if (matchType != null)
                {
                    matchType = matchType.Trim().ToLower();
                }

                if (matchType == null || !EOIValidation.VALID_FACE_REC_MATCH_TYPE_NAMES.Contains(matchType))
                {
                    response = new EOIResponse(false, $"Invalid face recognition match type. Value is {matchType}");
                }

                // validate person / group match params
                if (response.Success)
                {
                    if (matchType == "person")
                    {
                        if (string.IsNullOrEmpty(personId) || personId.Length < EOIValidation.MIN_FACEREC_PERSON_ID_LENGTH)
                        {
                            response = new EOIResponse(false, $"Invalid face recognition person id. Value is {personId}");
                        }
                    }
                    else if (matchType == "group")
                    {
                        if (string.IsNullOrEmpty(groupId) || groupId.Length < EOIValidation.MIN_FACEREC_GROUP_ID_LENGTH)
                        {
                            response = new EOIResponse(false, $"Invalid face recognition group id. Value is {groupId}");
                        }
                    }
                }
            }

            return response;
        }

        public static EOIResponse ValidateSimilarityConfig(string image, double? matchThreshold)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            // validate match type
            if (response.Success)
            {
                if (string.IsNullOrEmpty(image) || image.Length < EOIValidation.MIN_IMAGE_LENGTH)
                {
                    response = new EOIResponse(false, "Invalid similarity image. Please provide a valid base64 image string");
                }
                else if (matchThreshold == null || matchThreshold < EOIValidation.MIN_CONFIDENCE_THRESHOLD || matchThreshold > EOIValidation.MAX_CONFIDENCE_THRESHOLD)
                {
                    response = new EOIResponse(false, $"Invalid similarity match threshold. Value must be between {EOIValidation.MIN_CONFIDENCE_THRESHOLD} and {EOIValidation.MAX_CONFIDENCE_THRESHOLD}. Value is {matchThreshold}");
                }
            }

            return response;
        }

        public static EOIResponse ValidateSimilarityImageConfig(EOISimilarityImageConfig imageConfig)
        {
            EOIResponse response = imageConfig == null ?
                new EOIResponse(false, "Invalid similarity image. Please provide a valid similarity image configuration")
                : EOIResponse.DefaultSuccess();

            if (response.Success)
            {
                string seedId = imageConfig.SeedId?.Trim();
                string image = imageConfig.Image?.Trim();
                bool seedIdValid = !string.IsNullOrEmpty(seedId) && seedId.Length >= EOIValidation.MIN_SEED_ID_LENGTH;
                bool imageValid = !string.IsNullOrEmpty(image) && image.Length >= EOIValidation.MIN_IMAGE_LENGTH;

                if (!seedIdValid && !imageValid)
                {
                    response = new EOIResponse(false, "Invalid similarity image. Please provide a valid seed ID or base64 image string");
                }
                else if (imageConfig.Alert == true && imageConfig.Threshold == null)
                {
                    response = new EOIResponse(false, "Invalid similarity image threshold. Please provide a threshold when alert is true");
                }
                else if (imageConfig.Threshold != null &&
                    (imageConfig.Threshold < EOIValidation.MIN_CONFIDENCE_THRESHOLD || imageConfig.Threshold > EOIValidation.MAX_CONFIDENCE_THRESHOLD))
                {
                    response = new EOIResponse(false, $"Invalid similarity match threshold. Value must be between {EOIValidation.MIN_CONFIDENCE_THRESHOLD} and {EOIValidation.MAX_CONFIDENCE_THRESHOLD}. Value is {imageConfig.Threshold}");
                }
            }

            return response;
        }

        public static EOIResponse ValidateSimilarityConfig(EOISimilarityConfig similarityConfig)
        {
            EOIResponse response = similarityConfig == null ?
                new EOIResponse(false, "Invalid similarity configuration. Please provide at least one similarity image")
                : EOIResponse.DefaultSuccess();

            var validImageCount = 0;

            if (response.Success)
            {
                if (similarityConfig.Images != null)
                {
                    foreach (var imageConfig in similarityConfig.Images)
                    {
                        response = ValidateSimilarityImageConfig(imageConfig);

                        if (!response.Success)
                        {
                            break;
                        }

                        validImageCount++;
                    }
                }
            }

            if (response.Success && validImageCount == 0)
            {
                response = new EOIResponse(false, "Invalid similarity configuration. Please provide at least one valid similarity image");
            }

            return response;
        }
    }

}
