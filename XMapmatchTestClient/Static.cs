using System;

namespace XMapmatchTestClient
{
    public static class Static
    {
        public static double AirLineDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // convert to PTV Mercator
            double x1, y1, x2, y2;
            LatLonToSphereMercator(lat1, lon1, out x1, out y1);
            LatLonToSphereMercator(lat2, lon2, out x2, out y2);

            // mercator distance
            double mercDist = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

            // real distance
            return mercDist * Math.Cos(lat1 * Math.PI / 180.0);
        }

        // Project a geographic coordinate on the (spherical) Mercator map.
        // Using the mean between major an minor axis here ("PTV standard").
        // You could also use the major axis 6378137 ("Google standard").
        public static void LatLonToSphereMercator(double latitude, double longitude, out double x, out double y)
        {
            x = 6371000.0 * longitude * Math.PI / 180;
            y = 6371000.0 * Math.Log(Math.Tan(Math.PI / 4 + latitude * Math.PI / 360));
        }
    }
}