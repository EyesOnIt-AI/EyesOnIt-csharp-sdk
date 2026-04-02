using System;
using System.Collections.Generic;

namespace EyesOnItSDK.SocketIO
{
    public static class EOISocketRooms
    {
        public const string AllStreamUpdates = "all_stream_updates";
        public const string AllStreamDetections = "all_stream_detections";
        public const string AllPerformanceUpdates = "all_performance_updates";
        public const string AllLiveSearchUpdates = "all_live_search_updates";
        // This is the room-name prefix; use LiveSearchDetectionsForSearch(searchId) to join a specific room.
        public const string LiveSearchDetections = "live_search_detections";
        public static IReadOnlyCollection<string> StaticRoomNames { get; } = Array.AsReadOnly(new[]
        {
            AllStreamUpdates,
            AllStreamDetections,
            AllPerformanceUpdates,
            AllLiveSearchUpdates
        });

        public static string LiveSearchDetectionsForSearch(int searchId)
        {
            if (searchId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(searchId), searchId, "Search ID must be greater than zero.");
            }

            return $"{LiveSearchDetections}_{searchId}";
        }
    }
}
