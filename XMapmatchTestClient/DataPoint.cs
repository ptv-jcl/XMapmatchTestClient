using System;
using System.ComponentModel;
using System.Linq;

using XServer;

namespace XMapmatchTestClient
{
    public class DataPoint : INotifyPropertyChanged
    {
        private TrackPosition trackPosition;

        public TrackPosition TrackPosition
        {
            get { return trackPosition; }
            private set
            {
                trackPosition = value;
                NotifyPropertyChanged("TrackPosition");
                NotifyPropertyChanged("Id");
                NotifyPropertyChanged("LatInput");
                NotifyPropertyChanged("LonInput");
                NotifyPropertyChanged("SpeedInMps");
                NotifyPropertyChanged("SpeedInKmph");
                NotifyPropertyChanged("Heading");
                NotifyPropertyChanged("Timestamp");
            }
        }

        public long Id { get { return trackPosition.id; } private set { } }
        public double LatInput { get { return trackPosition.lat; } private set { } }
        public double LonInput { get { return trackPosition.lon; } private set { } }
        public float SpeedInMps { get { return trackPosition.speedInMps; } private set { } }
        public float SpeedInKmph { get { return trackPosition.speedInMps * 3.6F; } private set { } }
        public float Heading { get { return trackPosition.heading; } private set { trackPosition.heading = value; } }
        public DateTime Timestamp { get { return trackPosition.timestamp; } private set { } }

        private MatchedLocation matchedLocation;

        public MatchedLocation MatchedLocation
        {
            get { return matchedLocation; }
            set
            {
                if (value != null && value.inputId == trackPosition.id)
                {
                    matchedLocation = value;
                }
                else matchedLocation = null;
                NotifyPropertyChanged("LatOutput");
                NotifyPropertyChanged("LonOutput");
                NotifyPropertyChanged("Segment");
                NotifyPropertyChanged("SpeedLimit");
                NotifyPropertyChanged("CountryCode");
                NotifyPropertyChanged("NetworkClass");
            }
        }

        public double LatOutput { get { return ((matchedLocation == null) ? 0 : matchedLocation.lat); } private set { } }
        public double LonOutput { get { return ((matchedLocation == null) ? 0 : matchedLocation.lon); } private set { } }
        public MatchedSegment Segment { get { return ((matchedLocation == null) ? null : matchedLocation.wrappedPath.Last()); } private set { } }
        public string VendorId { get { return ((matchedLocation == null) ? null : matchedLocation.wrappedPath.Last().vendorId); } private set { } }
        public int SpeedLimitForward { get { return ((matchedLocation == null) ? 0 : Segment.speedLimitForward); } private set { } }
        public int SpeedLimitBackward { get { return ((matchedLocation == null) ? 0 : Segment.speedLimitBackward); } private set { } }
        public int CountryCode { get { return ((matchedLocation == null) ? 0 : Segment.countryCode); } private set { } }
        public int NetworkClass { get { return ((matchedLocation == null) ? 0 : Segment.networkClass); } private set { } }

        public float LocalRating { get { return ((matchedLocation == null) ? 0 : matchedLocation.localRating); } private set { } }
        public float Probability { get { return ((matchedLocation == null) ? 0 : matchedLocation.probability); } private set { } }
        public float TransitionRating { get { return ((matchedLocation == null) ? 0 : matchedLocation.transitionRating); } private set { } }
        public float LinkingDistance { get { return ((matchedLocation == null) ? 0 : matchedLocation.linkingDistance); } private set { } }
        public float AngleDifference { get { return ((matchedLocation == null) ? 0 : matchedLocation.angleDifference); } private set { } }

        public double DrivenDistance { get { return ((matchedLocation == null) ? 0 : matchedLocation.drivenDistance); } private set { } }
        public double CoveredDistance { get { return ((matchedLocation == null) ? 0 : matchedLocation.coveredDistance); } private set { } }

        public bool Stable { get { return ((matchedLocation == null) ? false : matchedLocation.stable); } private set { } }
        public bool LocalMatching { get { return ((matchedLocation == null) ? false : matchedLocation.localMatching); } private set { } }

        public DataPoint(long id, double lat, double lon, float speed, float heading, DateTime timeStamp)
        {
            trackPosition = new TrackPosition()
            {
                id = id,
                heading = heading,
                lat = lat,
                latSpecified = true,
                lon = lon,
                lonSpecified = true,
                speedInMps = speed,
                timestamp = timeStamp,
                timestampSpecified = true,
            };
        }

        private ResultAddress resultAddress;

        public ResultAddress ResultAddress
        {
            get { return resultAddress; }
            set
            {
                resultAddress = value;
                NotifyPropertyChanged("ResultAddress");
                NotifyPropertyChanged("City");
                NotifyPropertyChanged("City2");
                NotifyPropertyChanged("PostCode");
                NotifyPropertyChanged("Street");
                NotifyPropertyChanged("SegmentId");
            }
        }

        public string City { get { return (resultAddress == null ? "" : resultAddress.city); } private set { } }
        public string City2 { get { return (resultAddress == null ? "" : resultAddress.city2); } private set { } }
        public string PostCode { get { return (resultAddress == null ? "" : resultAddress.postCode); } private set { } }
        public string Street { get { return (resultAddress == null ? "" : resultAddress.street); } private set { } }
        public string SegmentId { get { return (resultAddress == null ? "" : resultAddress.wrappedAdditionalFields[1].value); } private set { } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void SetHeading(float heading)
        {
            this.Heading = heading;
        }

        public bool HasEqualValuesAs(DataPoint dataPoint)
        {
            if (LonInput != dataPoint.LonInput) return false;
            if (LatInput != dataPoint.LatInput) return false;
            if (SpeedInMps != dataPoint.SpeedInMps) return false;
            if (Heading != dataPoint.Heading) return false;
            return true;
        }
    }
}