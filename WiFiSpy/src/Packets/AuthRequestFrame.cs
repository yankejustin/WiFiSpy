using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiFiSpy.src.Packets
{
    public class AuthRequestFrame : IEqualityComparer<AuthRequestFrame>
    {
        public string SSID { get; private set; }
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

        public long SourceMacAddressLong
        {
            get
            {
                return Utils.MacToLong(SourceMacAddress);
            }
        }

        public byte[] TargetMacAddress
        {
            get;
            private set;
        }

        public string TargetMacAddressStr
        {
            get
            {
                return BitConverter.ToString(TargetMacAddress);
            }
        }

        public long TargetMacAddressLong
        {
            get
            {
                return Utils.MacToLong(TargetMacAddress);
            }
        }

        internal AuthRequestFrame()
        {

        }

        public AuthRequestFrame(PacketDotNet.Ieee80211.AssociationRequestFrame frame, DateTime TimeStamp)
        {
            this.TimeStamp = TimeStamp;
            this.SourceMacAddress = frame.SourceAddress.GetAddressBytes();
            this.TargetMacAddress = frame.DestinationAddress.GetAddressBytes();

            foreach (PacketDotNet.Ieee80211.InformationElement element in frame.InformationElements)
            {
                switch (element.Id)
                {
                    case PacketDotNet.Ieee80211.InformationElement.ElementId.ServiceSetIdentity:
                    {
                        SSID = ASCIIEncoding.ASCII.GetString(element.Value);
                        break;
                    }
                }

            }
        }

        public bool Equals(AuthRequestFrame x, AuthRequestFrame y)
        {
            return x.SSID == y.SSID && x.TimeStamp == y.TimeStamp;
        }

        public int GetHashCode(AuthRequestFrame obj)
        {
            return (int)(obj.SourceMacAddressLong + obj.TargetMacAddressLong + obj.TimeStamp.ToBinary());
        }
    }
}