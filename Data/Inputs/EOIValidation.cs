using EyesOnItSDK.Data.Elements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EyesOnItSDK.Data.Inputs
{
    class EOIValidation
    {
        private static int MIN_STREAM_NAME_LENGTH = 3;
        private static int MIN_REGION_NAME_LENGTH = 3;
        private static int MIN_PHONE_NUMBER_LENGTH = 10;
        private static int MAX_PHONE_NUMBER_LENGTH = 20;
        private static int MIN_PROMPT_LENGTH = 1;
        private static int MIN_PROMPT_THRESHOLD = 1;
        private static int MAX_PROMPT_THRESHOLD = 99;
        private static int MIN_FRAME_RATE = 1;
        private static int MIN_LINE_VERTEX_COUNT = 2;
        private static string[] VALID_CLASS_NAMES = { "person", "vehicle", "bag", "animal", "face", "unknown" };
        private static string[] COUNT_CONDITION_TYPES = { "count_equals", "count_greater_than", "count_less_than" };
        private static string[] LINE_CROSS_CONDITION_TYPES = { "line_cross" };
        private static int MIN_LINE_NAME_LENGTH = 3;
        private static int MIN_OBJECT_SIZE = 100;
        private static float MIN_ALERT_SECONDS = 0.1F;
        private static float MIN_RESET_SECONDS = 0.1F;
        private static int MIN_CAMERA_UUID_LENGTH = 10;
        private static int MIN_MOTION_THRESHOLD = 10;
        private static int MIN_SEARCH_QUERY_LENGTH = 2;
        private static int MIN_FACEREC_GROUP_NAME_LENGTH = 2;
        private static int MIN_FACEREC_PERSON_NAME_LENGTH = 2;
        private static int MIN_FACEREC_GROUP_ID_LENGTH = 2;
        private static int MIN_FACEREC_PERSON_ID_LENGTH = 2;
        private static int MIN_FACEREC_GROUP_DESCRIPTION_LENGTH = 10;
        private static int MIN_FACEREC_FILE_PATH_LENGTH = 5;
        private static int MIN_FACEREC_IMAGE_BASE64_LENGTH = 20;


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
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (inputs == null)
            {
                response = new EOIResponse(false, "inputs = null. Search request must include inputs");
            }
            else
            {
                if (inputs.ClassName == null || !VALID_CLASS_NAMES.Contains(inputs.ClassName.Trim()))
                {
                    response = new EOIResponse(false, $"In search inputs, class name is not valid. Class name is {inputs.ClassName}. See documentation at https://developer.eyesonit.us/documentation for valid class names.");
                }

                if (response.Success)
                {
                    var trimmedSearch = inputs.ObjectDescription == null ? null : inputs.ObjectDescription.Trim();

                    if (trimmedSearch == null || trimmedSearch.Length < MIN_SEARCH_QUERY_LENGTH)
                    {
                        response = new EOIResponse(false, $"Search object description must be at {MIN_SEARCH_QUERY_LENGTH} characters. Object description is '{inputs.ObjectDescription}'");
                    }
                }
            }

            return response;
        }

        public static EOIResponse ValidateLiveSearchInputs(EOILiveSearchInputs inputs)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (inputs == null)
            {
                response = new EOIResponse(false, "inputs = null. Live search request must include inputs");
            }
            else
            {
                if (inputs.ClassName == null || !VALID_CLASS_NAMES.Contains(inputs.ClassName.Trim()))
                {
                    response = new EOIResponse(false, $"In search inputs, class name is not valid. Class name is {inputs.ClassName}. See documentation at https://developer.eyesonit.us/documentation for valid class names.");
                }

                if (response.Success)
                {
                    var trimmedSearch = inputs.ObjectDescription?.Trim();

                    if (trimmedSearch == null || trimmedSearch.Length < MIN_SEARCH_QUERY_LENGTH)
                    {
                        response = new EOIResponse(false, $"Search object description must be at {MIN_SEARCH_QUERY_LENGTH} characters. Object description is '{inputs.ObjectDescription}'");
                    }
                }

                if (response.Success)
                {
                    response = inputs.AlertThreshold == null || (inputs.AlertThreshold >= 0 && inputs.AlertThreshold < 100) ?
                        EOIResponse.DefaultSuccess() :
                        new EOIResponse(false, $"If specified, live search alert threshold must be between 0 and 99. Value is {inputs.AlertThreshold}.");
                }

                if (response.Success)
                {
                    response = inputs.DurationSeconds == null || inputs.DurationSeconds >= 0 ?
                        EOIResponse.DefaultSuccess() :
                        new EOIResponse(false, $"live search duration must be greater than 0. Value is {inputs.DurationSeconds}");
                }
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

        public static EOIResponse ValidateSimilaritySearchInputs(EOISimilaritySearchInputs inputs)
        {
            EOIResponse response = EOIResponse.DefaultSuccess();

            if (inputs == null)
            {
                response = new EOIResponse(false, "inputs = null. Similarity search request must include inputs");
            }
            else
            {
                if (inputs.ReferenceImageId == null || inputs.ReferenceImageId.Length < 18)
                {
                    response = new EOIResponse(false, $"In similarity search inputs, ReferenceImageId is not valid. ReferenceImageId is {inputs.ReferenceImageId}. Valid ReferenceImageId values will come from a previous search result and will be at least 18 characters.");
                }
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
                        if (detectionConfig.ClassName == null && detectionConfig.ObjectSize == null)
                        {
                            response = new EOIResponse(false, $"In detection configurations, either a class name or an object size must be specified");
                        }
                        else if (detectionConfig.ClassName != null && detectionConfig.ClassThreshold == null)
                        {
                            response = new EOIResponse(false, $"In detection configurations, if a class name is specified, a class threshold must also be specified.");
                        }
                        else if (detectionConfig.ClassName != null && !VALID_CLASS_NAMES.Contains(detectionConfig.ClassName.Trim()))
                        {
                            response = new EOIResponse(false, $"In detection configurations, class name is not valid. Class name is {detectionConfig.ClassName}. See documentation at https://developer.eyesonit.us/documentation for valid class names.");
                        }
                        else if (detectionConfig.ObjectSize != null && detectionConfig.ObjectSize < MIN_OBJECT_SIZE)
                        {
                            response = new EOIResponse(false, $"In detection configurations, the object size should be at least {MIN_OBJECT_SIZE}. ObjectSize = {detectionConfig.ObjectSize}");
                        }
                        else
                        {
                            response = ValidateObjectDescriptions(detectionConfig.ObjectDescriptions, validateForVideo);
                        }

                        if (response.Success && validateForVideo)
                        {
                            if (detectionConfig.AlertSeconds < MIN_ALERT_SECONDS)
                            {
                                Console.WriteLine($"AlertSeconds: {detectionConfig.AlertSeconds}, MIN_ALERT_SECONDS: {MIN_ALERT_SECONDS}");
                                response = new EOIResponse(false, $"In detection configurations, alert seconds must be at least {MIN_ALERT_SECONDS}. AlertSeconds = {detectionConfig.AlertSeconds}");
                            }
                            else if (detectionConfig.ResetSeconds < MIN_RESET_SECONDS)
                            {
                                response = new EOIResponse(false, $"In detection configurations, reset seconds must be at least {MIN_RESET_SECONDS}. ResetSeconds = {detectionConfig.ResetSeconds}");
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
                            if (objectDescription.Threshold < EOIValidation.MIN_PROMPT_THRESHOLD || objectDescription.Threshold > EOIValidation.MAX_PROMPT_THRESHOLD)
                            {
                                response = new EOIResponse(false, $"The object description alerting threshold must be between {EOIValidation.MIN_PROMPT_THRESHOLD} and {EOIValidation.MAX_PROMPT_THRESHOLD}. The value for object description '{objectDescription.Text}' is ${objectDescription.Threshold}");
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
                            if (objectDescription.Threshold < EOIValidation.MIN_PROMPT_THRESHOLD || objectDescription.Threshold > EOIValidation.MAX_PROMPT_THRESHOLD)
                            {
                                response = new EOIResponse(false, $"The object description alerting threshold must be between {EOIValidation.MIN_PROMPT_THRESHOLD} and {EOIValidation.MAX_PROMPT_THRESHOLD}. The value for object description '{objectDescription.Text}' is ${objectDescription.Threshold}");
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
                    response = new EOIResponse(false,$"The group ID {trimmedGroupId} must be at least {MIN_FACEREC_GROUP_ID_LENGTH} character(s)");
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
                if (genetecNotification.WebhookCameraUUID == null || genetecNotification.WebhookCameraUUID.Length < MIN_CAMERA_UUID_LENGTH)
                {
                    response = new EOIResponse(false, $"If genetec notification is included, the webhook camera uuid must be specified with a minimum length of {MIN_CAMERA_UUID_LENGTH} characters. WebhookCameraUUID = {genetecNotification.WebhookCameraUUID}");
                }
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
    }
}
