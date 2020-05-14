using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XServer
{
    class dummy
    {
        List<WaypointDesc> MatchResult_2_Waypoints(MatchResult matchResult)
        {
            var waypointDescs = new List<WaypointDesc>();
            foreach (var matchedLocation in matchResult.wrappedMatchedLocations)
            {
                var waypoint = new WaypointDesc()
                {
                    linkType = LinkType.NEXT_SEGMENT,
                    wrappedSegmentID = new UniqueGeoID[]
                    {
                        new UniqueGeoID()
                        {
                            iuCode = matchedLocation.wrappedPath.Last().countryCode,
                            tID = matchedLocation.wrappedPath.Last().tileId,
                            xOff  = matchedLocation.wrappedPath.Last().wrappedStartXYN[0],
                            yOff = matchedLocation.wrappedPath.Last().wrappedStartXYN[1],
                            n = matchedLocation.wrappedPath.Last().wrappedStartXYN[2],
                        },
                        new UniqueGeoID()
                        {
                            iuCode = matchedLocation.wrappedPath.Last().countryCode,
                            tID = matchedLocation.wrappedPath.Last().tileId,
                            xOff  = matchedLocation.wrappedPath.Last().wrappedEndXYN[0],
                            yOff = matchedLocation.wrappedPath.Last().wrappedEndXYN[1],
                            n = matchedLocation.wrappedPath.Last().wrappedEndXYN[2],
                        },
                    },
                };
                waypointDescs.Add(waypoint);
            }
            return waypointDescs;
        }
    }
}
