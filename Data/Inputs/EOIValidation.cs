using EyesOnItSDK.Data.Elements;
using System.Collections.Generic;

namespace EyesOnItSDK.Data.Inputs
{
    class EOIValidation
    {
        private static int MAX_PHONE_NUMBER_LENGTH = 20;
        private static int MIN_PROMPT_LENGTH = 1;
        private static int MIN_PROMPT_THRESHOLD = 1;
        private static int MAX_PROMPT_THRESHOLD = 99;
        private static int MIN_FRAME_RATE = 1;

        private static EOIResponse ValidateBaseInputs(EOIBaseInputs inputs, bool validateThresholds)
        {
            EOIResponse response;

            if (inputs == null)
            {
                response = new EOIResponse(false, "inputs = null. Request must include inputs");
            }
            else
            {
                response = ValidatePrompts(inputs.ObjectDescriptions, validateThresholds);
            }

            return response;
        }

        public static EOIResponse ValidateProcessImageInputs(EOIBaseInputs inputs)
        {
            return ValidateBaseInputs(inputs, false);
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
                response = ValidateBaseInputs(inputs, true);

                if (response.Success)
                {
                    response = ValidateAlerting(inputs.Alerting);
                }

                if (response.Success)
                {
                    response = ValidateMotionDetection(inputs.MotionDetection);
                }

                if (response.Success)
                {
                    response = ValidateBoundingBox(inputs.BoundingBox);
                }

                if (response.Success)
                {
                    response = ValidateStreamUrl(inputs.StreamUrl);
                }

                if (response.Success)
                {
                    var nameTrimmed = inputs.Name == null ? null : inputs.Name.Trim();

                    if (nameTrimmed == null || nameTrimmed.Length == 0)
                    {
                        response = new EOIResponse(false, $"the stream name must be specified. stream name = {nameTrimmed}");
                    }
                }

                if (response.Success && inputs.FrameRate < EOIValidation.MIN_FRAME_RATE)
                {
                    response = new EOIResponse(false, $"the minimum frame rate is {EOIValidation.MIN_FRAME_RATE}. frame rate = {inputs.FrameRate}");
                }
            }

            return response;
        }

        public static EOIResponse ValidateProcessVideosInputs(EOIProcessVideosInputs inputs)
        {
            EOIResponse response;

            if (inputs == null)
            {
                response = new EOIResponse(false, "inputs = null. Process video request must include inputs");
            }
            else
            {
                response = ValidateBaseInputs(inputs, true);

                if (response.Success)
                {
                    response = ValidateInputVideoFiles(inputs.InputVideoFiles);
                }

                // Video output file is optional for now
                //if (response.Success)
                //{
                //    response = ValidateOutputVideoFile(inputs.OutputVideoFile);
                //}

                if (response.Success)
                {
                    response = ValidateAlerting(inputs.Alerting);
                }

                if (response.Success)
                {
                    response = ValidateMotionDetection(inputs.MotionDetection);
                }

                if (response.Success)
                {
                    response = ValidateBoundingBox(inputs.BoundingBox);
                }

                if (response.Success && inputs.FrameRate < EOIValidation.MIN_FRAME_RATE)
                {
                    response = new EOIResponse(false, $"the minimum frame rate is {EOIValidation.MIN_FRAME_RATE}. frame rate = {inputs.FrameRate}");
                }
            }

            return response;
        }

        public static EOIResponse ValidateStreamUrl(string streamUrl)
        {
            var trimmedUrl = streamUrl == null ? null : streamUrl.Trim();

            return trimmedUrl != null && trimmedUrl.Length > 0 ?
                EOIResponse.DefaultSuccess()
                : new EOIResponse(false, "The stream url must be a valid RTSP URL");
        }

        public static EOIResponse ValidateMotionDetection(EOIMotionDetection motionDetection)
        {
            EOIResponse response = motionDetection == null ?
                new EOIResponse(false, "request must include motion_detection configurtion")
                : EOIResponse.DefaultSuccess();

            if (response.Success && motionDetection != null)
            {
                if (!motionDetection.PeriodicCheckEnabled)
                {
                    if (motionDetection.MotionDetectionEnabled)
                    {
                        response = new EOIResponse(false, "if MotionDetectionEnabled is true then PeriodicCheckEnabled must also be true");
                    }
                }
                else
                {
                    if (motionDetection.MotionDetectionThreshold < 1)
                    {
                        response = new EOIResponse(false, $"motion detection threshold should be at least 1. MotionDetectionThreshold = {motionDetection.MotionDetectionThreshold}");
                    }
                    else if (motionDetection.MotionDetectionSeconds < 0)
                    {
                        response = new EOIResponse(false, $"motion detection seconds should be at least 0. MotionDetectionSeconds = {motionDetection.MotionDetectionSeconds}");
                    }
                    else if (motionDetection.PeriodicCheckSeconds < 0)
                    {
                        response = new EOIResponse(false, $"periodic check seconds should be at least 0. PeriodicCheckSeconds = {motionDetection.PeriodicCheckSeconds}");
                    }
                }
            }

            return response;
        }

        public static EOIResponse ValidateBoundingBox(EOIBoundingBox boundingBox)
        {
            EOIResponse response = boundingBox == null ?
                new EOIResponse(false, "request must include BoundingBox configurtion")
                : EOIResponse.DefaultSuccess();

            if (response.Success && boundingBox != null)
            {
                if (boundingBox.BoundingBoxEnabled)
                {
                    if (!boundingBox.DetectPeople && !boundingBox.DetectVehicles && !boundingBox.DetectBags)
                    {
                        response = new EOIResponse(false, "at least one object type must be specified for bounding box detection");
                    }
                    else if (boundingBox.PeopleConfidenceThreshold < 10 ||
                        boundingBox.VehiclesConfidenceThreshold < 10 ||
                        boundingBox.BagsConfidenceThreshold < 10)
                    {
                        response = new EOIResponse(false, "bounding box detection threshold must be at least 10");
                    }
                    else if (boundingBox.PeopleConfidenceThreshold > 100 ||
                        boundingBox.VehiclesConfidenceThreshold > 100 ||
                        boundingBox.BagsConfidenceThreshold > 100)
                    {
                        response = new EOIResponse(false, "bounding box detection threshold must not be greater than 100");
                    }
                }
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

        public static EOIResponse ValidateAlerting(EOIAlerting alerting)
        {
            EOIResponse response = alerting == null ?
                new EOIResponse(false, "request must include alerting configurtion")
                : EOIResponse.DefaultSuccess();

            if (response.Success && alerting != null)
            {
                if (alerting.AlertSecondsCount < 0.1 || alerting.AlertSecondsCount > 3600)
                {
                    response = new EOIResponse(false, $"alert_seconds_count must be between 0.1 and 3600.alert_seconds_count = ${alerting.AlertSecondsCount}");
                }
                else if (alerting.ResetSecondsCount < 0.1 || alerting.ResetSecondsCount > 3600)
                {
                    response = new EOIResponse(false, $"reset_seconds_count must be between 0.1 and 3600.reset_seconds_count = ${alerting.ResetSecondsCount}");
                }
                else if (alerting.PhoneNumber != null)
                {
                    response = EOIValidation.ValidatePhoneNumber(alerting.PhoneNumber);
                }
            }

            return response;

        }

        private static EOIResponse ValidatePhoneNumber(string phoneNumber)
        {
            var trimmedPhoneNumber = phoneNumber == null ? null : phoneNumber.Trim();

            EOIResponse response = trimmedPhoneNumber == null || trimmedPhoneNumber.Length == 0 ?
                new EOIResponse(false, "The phone number cannot be null or empty")
                : EOIResponse.DefaultSuccess();

            if (response.Success && trimmedPhoneNumber != null)
            {
                if (trimmedPhoneNumber.Length > EOIValidation.MAX_PHONE_NUMBER_LENGTH)
                {
                    response = new EOIResponse(false, $"The phone number maximum length is {EOIValidation.MAX_PHONE_NUMBER_LENGTH}");
                }
                else if (!trimmedPhoneNumber.StartsWith("+"))
                {
                    response = new EOIResponse(false, "The phone number must start with a country code like + 1");
                }
                else
                {
                    response = EOIResponse.DefaultSuccess();
                }
            }

            return response;

        }

        public static EOIResponse ValidatePrompts(EOIObjectDescription[] objectDescriptions, bool validateThresholds)
        {
            EOIResponse response = objectDescriptions == null || objectDescriptions.Length == 0 ?
                new EOIResponse(false, $"request must include an array of object descriptions. objectDescriptions = {objectDescriptions}")
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

        public static EOIResponse ValidateRegions(EOIRegion[] regions)
        {
            EOIResponse response = regions == null ?
                new EOIResponse(false, "request must include regions configurtion")
                : EOIResponse.DefaultSuccess();

            if (response.Success && regions != null)
            {
                foreach (var region in regions)
                {
                    if (response.Success)
                    {
                        if (region.X < 0 || region.Y < 0)
                        {
                            response = new EOIResponse(false, $"The region x and y values cannot be negative");
                        }
                        else if (region.Width < 1 || region.Height < 1)
                        {
                            response = new EOIResponse(false, $"The region width and height values cannot be negative");
                        }
                    }
                }
            }

            return response;
        }


    }

}
