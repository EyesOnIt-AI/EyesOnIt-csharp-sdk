# EyesOnIt - Large Vision Model for Advanced Computer Vision

**EyesOnIt** brings the power of Large Vision Models to computer programmers without computer vision or data science skills. This SDK allows EyesOnIt to be easily integrated with other products or projects.

## User Guide

To get a free developer license and download EyesOnIt, refer to the EyesOnIt User Guide [here](https://developer.eyesonit.us/documentation).

## Adding the SDK Package

To add the SDK package to your project:

```
dotnet add package eyesonit.csharp.sdk --version 3.0.3
```

More details about the the SDK package are [here](https://www.nuget.org/packages/eyesonit.csharp.sdk).

## Sample Code

```csharp
// initialize the EyesOnIt SDK
EyesOnIt eyesOnItSDK = new EyesOnIt("http://192.168.1.11:8000");

// Define the video stream to process
EOIAddStreamInputs addStreamInputs = new EOIAddStreamInputs()
{
    StreamUrl = "rtsp://192.168.1.54/live0",                                // specify the stream RTSP URL
    Name = "Street Camera",                                                 // provide a friendly name for the stream
    FrameRate = 5,                                                          // specify the number of frames per second to read
    Regions = new EOIRegion[]                                               // detection parameters are defined for one or more regions
    {
        new EOIRegion()
        {
            Name = "Doorway",                                               // provide a friendly name for the region
            Polygon = new EOIVertex[]                                       // define the polygon for the region - at least 3 vertices
            {
                new EOIVertex(800, 0),
                new EOIVertex(1600, 0),
                new EOIVertex(1600, 1100),
                new EOIVertex(800, 1100)
            },
            MotionDetection = new EOIMotionDetection()                      // specify motion detection parameters for the region
            {
                Enabled = true,
                DetectionThreshold = 300,
                RegularCheckFrameInterval = 1
            },
            DetectionConfigs = new EOIDetectionConfig[]                     // each region can have one of more detection configurations
            {
                new EOIDetectionConfig()
                {
                    ClassName = "person",                                   // this is optional. Tell EyesOnIt to look for people, vehicles or other common objects
                    ClassThreshold = 30,
                    ObjectDescriptions = new EOIObjectDescription[]         // use object descriptions to validate and refine the class detection 
                    {
                        new EOIObjectDescription()
                        {
                            Text = "person with blue shirt",                // use English words to describe what you want to detect
                            Threshold = 90                                  // provide the alerting confidence threshold
                        },
                        new EOIObjectDescription()                          // provide other descriptions where you don't want alerts
                        {
                            Text = "person"                                 // in this case, EyesOnIt will alert for people with blue shirts, but not other people
                        },
                        new EOIObjectDescription()
                        {
                            Text = "wall door",                             // optionally describe the background to help EyesOnIt produce more accurate results
                            BackgroundDescription = true
                        }
                    },
                    DetectionConditions = new EOIDetectionCondition[]       // define the alert conditions
                    {
                        new EOIDetectionCondition()
                        {
                            Type = "count_greater_than",                    // general alert conditions - alert if a person with a blue shirt is present
                            Count = 0
                        }
                    },
                    AlertSeconds = 2F,                                      // use this parameter to confirm that the object is present for more 2 seconds
                    ResetSeconds = 10                                       // reset the alert state when the object has not been present for 10 seconds
                }
            }
        }
    },
    Notification = new EOINotification()                                    // provide the notification settings
    {
        PhoneNumber = "+11234567890",                                       // send alert SMS notifications to this phone number 
        ImageNotification = true,                                           // include the frame where the alert occurred
        RESTUrl = "https://192.168.1.50/my_service/handle_alert"            // also send a POST to a REST URL
    }
};

// Add the stream to EyesOnIt
EOIAddStreamResponse response = await eyesOnItSDK.AddStream(addStreamInputs);  // add the stream and await the response

if (response.Success)
{
    // Monitor the stream
    await eyesOnItSDK.MonitorStream(addStreamInputs.StreamUrl, null);       // AddStream succeeded. Monitor the stream.
}
```

## Demo

The free interactive online demo is available [here](https://www.eyesonit.us/free-demo-sign-up).

## Questions

Please email us at support@eyesonit.us if you have questions

