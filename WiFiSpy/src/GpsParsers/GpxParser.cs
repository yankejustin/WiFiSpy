using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WiFiSpy.src
{
    public class GpxParser
    {

        //I only used 1 csv file sample from a app, let's hope they're all using the same template
        public static GpsLocation[] GetLocations(string FilePath)
        {

            XmlDocument xmlReader = new XmlDocument();
            xmlReader.Load(FilePath);

            XmlElement GpxHeader = xmlReader["gpx"];

            if (GpxHeader == null)
                return new GpsLocation[0]; //no header

            XmlElement TrkHeader = GpxHeader["trk"];

            if (TrkHeader == null)
                return new GpsLocation[0]; //no header

            XmlElement trksegHeader = TrkHeader["trkseg"];

            if (trksegHeader == null)
                return new GpsLocation[0]; //no header

            XmlNodeList Locations = trksegHeader.GetElementsByTagName("trkpt");
            List<GpsLocation> Gpslocations = new List<GpsLocation>();

            foreach (XmlNode node in Locations)
            {
                double Lat = 0;
                double Lon = 0;

                int year = 0;
                int month = 0;
                int day = 0;
                int hour = 0;
                int minute = 0;
                int second = 0;

                if (node["time"] != null && node.Attributes["lat"] != null && node.Attributes["lon"] != null &&
                    double.TryParse(node.Attributes["lat"].Value, out Lat) && double.TryParse(node.Attributes["lon"].Value, out Lon))
                {
                    string TimeStr = node["time"].InnerText;
                    if (!TimeStr.Contains("T") || !TimeStr.Contains("Z"))
                        continue;

                    string[] DateTimeStrSplit = TimeStr.Replace("Z", "").Split('T');
                    string[] DateSplit = DateTimeStrSplit[0].Split('-');
                    string[] TimeSplit = DateTimeStrSplit[1].Split(':');

                    if (DateSplit.Length != 3 || TimeSplit.Length != 3)
                        continue;

                    if (!int.TryParse(DateSplit[0], out year) || !int.TryParse(DateSplit[1], out month) || !int.TryParse(DateSplit[2], out day))
                        continue;

                    if (!int.TryParse(TimeSplit[0], out hour) || !int.TryParse(TimeSplit[1], out minute) || !int.TryParse(TimeSplit[2], out second))
                        continue;

                    DateTime time = new DateTime(year, month, day, hour, minute, second);
                    Gpslocations.Add(new GpsLocation(Utils.GetRealArrivalTime(time), Lon, Lat));
                }
            }

            return Gpslocations.ToArray();
        }
    }
}