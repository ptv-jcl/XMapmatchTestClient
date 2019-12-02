namespace XServer
{
    public partial class CountryInfo
    {
        public override string ToString()
        {
            string toString = this.countryCode;
            if (this.tollTotals != null)
            {
                toString += " - " + this.tollTotals.cost + " - " + this.tollTotals.distance;
            }
            return toString;
        }
    }

    public partial class TollStationDescription
    {
        public override string ToString()
        {
            return this.tollStationName;
        }
    }

    public partial class ResultAddress
    {
        public override string ToString()
        {
            string singleLine = country ?? "";
            if ((postCode ?? "") != "") singleLine += ", " + postCode;
            if ((city ?? "") != "") singleLine += ", " + city;
            if ((city2 ?? "") != "") singleLine += "/ " + city2;
            if ((street ?? "") != "") singleLine += ", " + street;
            if ((houseNumber ?? "") != "") singleLine += " " + houseNumber;
            return singleLine;
        }
    }
}