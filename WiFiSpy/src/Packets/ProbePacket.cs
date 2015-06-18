using PacketDotNet.Ieee80211;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiFiSpy.src.Packets
{
    public class ProbePacket : IEqualityComparer<ProbePacket>
    {
        public DateTime TimeStamp { get; private set; }

        public byte[] SourceMacAddress
        {
            get;
            private set;
        }

        public string SourceMacAddressStr
        {
            get
            {
                return BitConverter.ToString(SourceMacAddress);
            }
        }

        public bool IsBroadCastProbe
        {
            get
            {
                for (int i = 0; i < SourceMacAddress.Length; i++)
                {
                    if (SourceMacAddress[i] != 0xFF)
                        return false;
                }
                return true;
            }
        }

        public string VendorSpecificManufacturer { get; private set; }

        public string SSID { get; private set; }
        public bool IsHidden { get; private set; }

        internal ProbePacket()
        {

        }

        public ProbePacket(PacketDotNet.Ieee80211.ProbeRequestFrame probeRequestFrame, DateTime TimeStamp)
        {
            this.TimeStamp = TimeStamp;

            this.SourceMacAddress = probeRequestFrame.SourceAddress.GetAddressBytes();

            foreach (InformationElement element in probeRequestFrame.InformationElements)
            {
                switch (element.Id)
                {
                    case InformationElement.ElementId.VendorSpecific:
                    {
                        VendorSpecificManufacturer = OuiParser.GetOuiByMac(element.Value);
                        break;
                    }
                    case InformationElement.ElementId.ServiceSetIdentity:
                    {
                        SSID = ASCIIEncoding.ASCII.GetString(element.Value);

                        if (SSID != null && SSID.Length > 0)
                        {
                            IsHidden = SSID[0] == 0;
                        }
                        else if(SSID == "")
                        {
                            IsHidden = true;
                        }
                        break;
                    }
                }
            }
        }

        public override string ToString()
        {
            return "[Probe] SSID: " + SSID + ", Is Hidden: " + IsHidden;
        }

        public bool Equals(ProbePacket x, ProbePacket y)
        {
            return x.TimeStamp == y.TimeStamp &&
                   x.SourceMacAddressStr == y.SourceMacAddressStr;
        }

        public int GetHashCode(ProbePacket obj)
        {
            return (int)obj.TimeStamp.ToBinary();
        }
    }
}
