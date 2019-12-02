using System;
using System.ComponentModel;
using System.IO;

namespace XMapmatchTestClient
{
    public static class NMEAParsers
    {
        public static BindingList<DataPoint> ParseFile(string fileName, int interval = 10)
        {
            if (!File.Exists(fileName)) throw new ArgumentException("The file " + fileName + " does not exists.");
            StreamReader reader;
            try
            {
                reader = new StreamReader(fileName);
            }
            catch (Exception exception)
            {
                throw new ArgumentException("Exception while opening " + fileName + " .", exception);
            }

            return ParseFile(reader, interval);
        }

        public static BindingList<DataPoint> ParseFile(StreamReader reader, int interval = 10)
        {
            BindingList<DataPoint> dataPointList = new BindingList<DataPoint>();
            string line;
            double lat, lon;
            float speed, heading;
            DateTime timeStamp = new DateTime();
            DateTime nextPollMoment = DateTime.FromFileTimeUtc(0);
            int id = 0;
            while (true)
            {
                line = reader.ReadLine();
                if (line == null) break;
                string[] split = line.Split(',');
                if (split[0] != "$GPRMC") continue;

                if (!double.TryParse(split[3], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), out lat)) continue;
                lat = GeoMinDec2GeoDec(lat);
                if (split[4] == "S") lat = -lat;
                if (!double.TryParse(split[5], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), out lon)) continue;
                lon = GeoMinDec2GeoDec(lon);
                if (split[6] == "W") lon = -lon;
                if (!float.TryParse(split[7], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), out speed)) continue;
                speed *= 0.51444f;
                if (!float.TryParse(split[8], System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CreateSpecificCulture("en-US"), out heading)) continue;

                try
                {
                    string[] DateTimeFormats = { "ddMMyyHHmmss", "ddMMyy", "ddMMyyHHmmss.FFFFFF" };
                    if (split[9].Length >= 6)
                    { //Require at least the date to be present
                        string time = split[9] + split[1]; // +" 0";
                        timeStamp = DateTime.ParseExact(time, DateTimeFormats, new System.Globalization.CultureInfo("en-US", false).NumberFormat, System.Globalization.DateTimeStyles.AssumeUniversal);
                    }
                    else
                        timeStamp = new DateTime();
                }
                catch { timeStamp = new DateTime(); }
                if (nextPollMoment == DateTime.FromFileTimeUtc(0)) nextPollMoment = timeStamp;

                if (timeStamp > nextPollMoment)
                {
                    DataPoint dataPoint = new DataPoint(id++, lat, lon, speed, heading, timeStamp);
                    dataPointList.Add(dataPoint);
                    while (nextPollMoment <= timeStamp)
                        nextPollMoment = nextPollMoment.AddSeconds(interval);
                }
            }
            return dataPointList;
        }

        public static double GeoMinDec2GeoDec(double geoMinDec)
        {
            double degree = (double)(int)(geoMinDec / 100) * 100;
            double minute = (geoMinDec - degree);
            double _temp = (degree + (minute / 0.60)) / 100;
            return _temp;
        }
    }
}