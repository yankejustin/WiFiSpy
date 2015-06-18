using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiFiSpy.src.Packets;

namespace WiFiSpy.src
{
    public class AccessPoint : IEqualityComparer<AccessPoint>
    {
        public BeaconFrame BeaconFrame { get; private set; }
        private List<BeaconFrame> BeaconFrames { get; set; }

        public string SSID
        {
            get
            {
                return BeaconFrame.SSID;
            }
        }

        public string MacAddress
        {
            get
            {
                return BeaconFrame.MacAddressStr;
            }
        }

        public long MacAddressLong
        {
            get
            {
                return CapFile.MacToLong(BeaconFrame.MacAddress);
            }
        }

        public string Manufacturer
        {
            get
            {
                return BeaconFrame.Manufacturer;
            }
        }

        public DateTime TimeStamp
        {
            get
            {
                return BeaconFrame.TimeStamp;
            }
        }

        public bool WPS_Enabled
        {
            get
            {
                return BeaconFrame.WPS_Enabled;
            }
        }

        public AccessPoint()
        {

        }

        public AccessPoint(BeaconFrame beaconFrame)
        {
            this.BeaconFrame = beaconFrame;
            this.BeaconFrames = new List<BeaconFrame>();
        }

        internal void AddBeaconFrame(BeaconFrame beaconFrame)
        {
            this.BeaconFrames.Add(beaconFrame);
        }

        public override string ToString()
        {
            return "[" + MacAddress + "] " + SSID;
        }

        public bool Equals(AccessPoint x, AccessPoint y)
        {
            return x.BeaconFrame.MacAddressStr == y.BeaconFrame.MacAddressStr;
        }

        public int GetHashCode(AccessPoint obj)
        {
            return (int)CapFile.MacToLong(obj.BeaconFrame.MacAddress);
        }
    }
}