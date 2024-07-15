# EyesOnIt - Large Vision Model for Advanced Computer Vision

**EyesOnIt** brings the power of Large Vision Models to computer programmers without computer vision or data science skills. This SDK allows EyesOnIt to be easily integrated with other products or projects.

## User Guide

To get a free developer license and download EyesOnIt, refer to the EyesOnIt User Guide [here](https://www.eyesonit.us/userguide).

## Adding the SDK Package

To add the SDK package to your project:

```
dotnet add package eyesonit.csharp.sdk --version 2.2.4
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
    Regions = new EOIRegion[]                                               // specify the area in the frame to process
    {
        new EOIRegion(435, 388, 600, 224)
    },
    ObjectDescriptions = new EOIObjectDescription[]                         // provide the descriptions of objects to detect
    {
        new EOIObjectDescription("vehicle", false) { Threshold = 90 },
        new EOIObjectDescription("landscape", true)
    },
    ObjectSize = 250,                                                       // provide the estimated object size in pixels
    Alerting = new EOIAlerting()                                            // provide the alerting settings
    {
        AlertSecondsCount = 0.4,
        ResetSecondsCount = 2,
        PhoneNumber = "+11234567890",
        ImageNotification = true
    }
};

// Add the stream to EyesOnIt
EOIResponse response = await eyesOnItSDK.AddStream(addStreamInputs);        // add the stream and await the response

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

