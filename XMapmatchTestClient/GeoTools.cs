using System;
using System.Windows;

namespace Geotools
{
    public enum CoordinateFormat
    {
        /// <summary>
        /// Lat/Lon EPGS:4326
        /// </summary>
        Wgs84,

        /// <summary>
        /// PTV Spherical Mercator EPSG:505456
        /// </summary>
        Ptv_Mercator,

        /// <summary>
        /// PTV Geodecimal (WGS * 100000)
        /// </summary>
        Ptv_Geodecimal,

        /// <summary>
        /// PTV Smart Units
        /// </summary>
        Ptv_SmartUnits,

        /// <summary>
        /// Spherical Web Mercator Projection
        /// (aka Google Mercator) EPSG:900913
        /// </summary>
        Web_Mercator,
    }

    public static class AirLineDistanceCalculator
    {
        // Calcluate airline distance based on mercator distance.
        // This approximation formula is sufficiently accurate for
        // our needs for distances of up to 600 km and
        // 80° latitude (the error is never more than 5% even for
        // extreme values).
        public static double CalculateUsingWGS84(Point start, Point end)
        {
            var startMerc = GeoTransform.Trans(CoordinateFormat.Wgs84, CoordinateFormat.Ptv_Mercator, start);
            var endMerc = GeoTransform.Trans(CoordinateFormat.Wgs84, CoordinateFormat.Ptv_Mercator, end);

            double dist = Math.Sqrt((startMerc.X - endMerc.X) * (startMerc.X - endMerc.X) + (startMerc.Y - endMerc.Y) * (startMerc.Y - endMerc.Y));
            return dist * Math.Cos(start.Y * Math.PI / 180.0);
        }

        //public static void Main()
        //{
        //    double lat1 = 49.0;
        //    double lon1 = 8.0;

        //    double lat2 = 50.0;
        //    double lon2 = 9.0;

        //    // convert to PTV Mercator
        //    double x1, y1, x2, y2;
        //    LatLonToSphereMercator(lat1, lon1, out x1, out y1);
        //    LatLonToSphereMercator(lat2, lon2, out x2, out y2);

        //    // mercator distance
        //    double mercDist = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

        //    // real distance
        //    double realDist = mercDist * Math.Cos(lat1 * Math.PI / 180.0);

        //    Console.WriteLine("Distance Between {0}/{1} and {2}/{3}", lat1, lon1, lat2, lon2);
        //    Console.WriteLine("Mercator: {0}", mercDist);
        //    Console.WriteLine("Real: {0}", realDist);
        //}
    }

    public static class GeoTransform
    {
        public static Point Trans(CoordinateFormat inFormat, CoordinateFormat outFormat, Point point)
        {
            // int == out -> return
            if (inFormat == outFormat)
                return point;

            // direct transformations
            if (inFormat == CoordinateFormat.Ptv_Geodecimal && outFormat == CoordinateFormat.Wgs84)
                return Geodecimal_2_WGS84(point);

            if (inFormat == CoordinateFormat.Wgs84 && outFormat == CoordinateFormat.Ptv_Geodecimal)
                return WGS84_2_Geodecimal(point);

            if (inFormat == CoordinateFormat.Ptv_Mercator && outFormat == CoordinateFormat.Ptv_SmartUnits)
                return Mercator_2_SmartUnits(point);

            if (inFormat == CoordinateFormat.Ptv_SmartUnits && outFormat == CoordinateFormat.Ptv_Mercator)
                return SmartUnits_2_Mercator(point);

            if (inFormat == CoordinateFormat.Ptv_Mercator && outFormat == CoordinateFormat.Wgs84)
                return SphereMercator_2_Wgs(point, Ptv_Radius);

            if (inFormat == CoordinateFormat.Wgs84 && outFormat == CoordinateFormat.Ptv_Mercator)
                return Wgs_2_SphereMercator(point, Ptv_Radius);

            if (inFormat == CoordinateFormat.Web_Mercator && outFormat == CoordinateFormat.Wgs84)
                return SphereMercator_2_Wgs(point, Google_Radius);

            if (inFormat == CoordinateFormat.Wgs84 && outFormat == CoordinateFormat.Web_Mercator)
                return Wgs_2_SphereMercator(point, Google_Radius);

            if (inFormat == CoordinateFormat.Ptv_Mercator && outFormat == CoordinateFormat.Web_Mercator)
                return Ptv_2_Google(point);

            if (inFormat == CoordinateFormat.Web_Mercator && outFormat == CoordinateFormat.Ptv_Mercator)
                return Google_2_Ptv(point);

            // transitive transformations
            if (inFormat == CoordinateFormat.Ptv_SmartUnits)
                return Trans(CoordinateFormat.Ptv_Mercator, outFormat, SmartUnits_2_Mercator(point));

            if (outFormat == CoordinateFormat.Ptv_SmartUnits)
                return Mercator_2_SmartUnits(Trans(inFormat, CoordinateFormat.Ptv_Mercator, point));

            if (inFormat == CoordinateFormat.Ptv_Geodecimal)
                return Trans(CoordinateFormat.Wgs84, outFormat, Geodecimal_2_WGS84(point));

            if (outFormat == CoordinateFormat.Ptv_Geodecimal)
                return WGS84_2_Geodecimal(Trans(inFormat, CoordinateFormat.Wgs84, point));

            // this should not happen
            throw new NotImplementedException(string.Format("transformation not implemented for {0} to {1}",
              inFormat.ToString(), outFormat.ToString()));
        }

        #region geographic formats

        public static Point Geodecimal_2_WGS84(Point point)
        {
            return new Point(point.X / 100000.0, point.Y / 100000.0);
        }

        public static Point WGS84_2_Geodecimal(Point point)
        {
            return new Point(point.X * 100000.0, point.Y * 100000.0);
        }

        #endregion geographic formats

        #region smart units

        private const double SMARTFACTOR = 4.809543;
        private const int SMART_OFFSET = 20015087;

        public static Point SmartUnits_2_Mercator(Point point)
        {
            return new Point(
              (point.X * SMARTFACTOR) - SMART_OFFSET,
              (point.Y * SMARTFACTOR) - SMART_OFFSET);
        }

        public static Point Mercator_2_SmartUnits(Point point)
        {
            return new Point(
              (point.X + SMART_OFFSET) / SMARTFACTOR,
              (point.Y + SMART_OFFSET) / SMARTFACTOR);
        }

        #endregion smart units

        #region Spherical Mercator

        private const double Ptv_Radius = 6371000.0;
        private const double Google_Radius = 6378137.0;

        public static Point Ptv_2_Google(Point point)
        {
            return new Point(point.X / Ptv_Radius * Google_Radius, point.Y / Ptv_Radius * Google_Radius);
        }

        public static Point Google_2_Ptv(Point point)
        {
            return new Point(point.X / Google_Radius * Ptv_Radius, point.Y / Google_Radius * Ptv_Radius);
        }

        public static Point Wgs_2_SphereMercator(Point point, double earthRadius)
        {
            double x = earthRadius * point.X * Math.PI / 180.0;
            double y = earthRadius * Math.Log(Math.Tan(Math.PI / 4.0 + point.Y * Math.PI / 360.0));

            return new Point(x, y);
        }

        public static Point SphereMercator_2_Wgs(Point point, double earthRadius)
        {
            double x = (180.0 / Math.PI) * (point.X / earthRadius);
            double y = (360 / Math.PI) * (Math.Atan(Math.Exp(point.Y / earthRadius)) - (Math.PI / 4));

            return new Point(x, y);
        }

        #endregion Spherical Mercator
    }
}