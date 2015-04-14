using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiFiSpy.src
{
    public class GpsLocation
    {
        public DateTime Time { get; private set; }
        public double Longitude { get; private set; }
        public double Latitude { get; private set; }

        public GpsLocation(DateTime Time, double Longitude, double Latitude)
        {
            this.Time = Time;
            this.Longitude = Longitude;
            this.Latitude = Latitude;
        }

        public override string ToString()
        {
            return "Date:" + Time.ToString("dd-MM-yyyy HH:mm:ss") + ", long:" + Longitude + ", lat:" + Latitude;
        }

        public bool IsNearby(DateTime TargetTime)
        {
            if (Time == null)
                return false;

            return Time.AddSeconds(-5) > TargetTime && TargetTime < Time.AddSeconds(5);
        }

        public static string ToKML(GpsLocation[] Locations)
        {
            if (Locations == null || Locations.Length == 0)
                return "";

            StringBuilder SB = new StringBuilder();

            string TagName = Locations[0].Time.Year + "-" + Locations[0].Time.Month.ToString("D2") + "-" + Locations[0].Time.Day.ToString("D2") + "T" + Locations[0].Time.Hour.ToString("D2") + ":" + Locations[0].Time.Minute.ToString("D2") + ":" + Locations[0].Time.Second.ToString("D2") + "Z";

            SB.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            SB.AppendLine("<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\"><Document><name>" + TagName + "</name>");

            SB.AppendLine("<Placemark>");
            SB.AppendLine("<gx:Track>");

            foreach (GpsLocation loc in Locations)
            {
                SB.AppendLine("");

                SB.AppendLine("<when>" + loc.Time.Year + "-" + loc.Time.Month.ToString("D2") + "-" + loc.Time.Day.ToString("D2") + "T" + loc.Time.Hour.ToString("D2") + ":" + loc.Time.Minute.ToString("D2") + ":" + loc.Time.Second.ToString("D2") + "</when>");
                SB.AppendLine("<gx:coord>" + loc.Longitude + " " + loc.Latitude + " 00.0</gx:coord>");

            }

            SB.AppendLine("</gx:Track>");
            SB.AppendLine("</Placemark></Document></kml>");

            return SB.ToString();
        }
    }
}