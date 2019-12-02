using System;
using System.Linq;
using XServer;

namespace XMapmatchTestClient
{
    public static class Extensions
    {
        public static int GetSpeedlimit(this MatchedLocation matchedLocation)
        {
            return Math.Max(matchedLocation.wrappedPath.Last().speedLimitBackward, matchedLocation.wrappedPath.Last().speedLimitForward);
        }
    }
}