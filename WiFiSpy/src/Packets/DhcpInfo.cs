using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiFiSpy.src.Packets
{
    public class DhcpInfo
    {
        public int SecondsElapsed { get; private set; }
        public string ClientIpAddress { get; private set; }
        public string YourIpAddress { get; private set; }
        public string NextServerIpAddress { get; private set; }
        public string RelayAgentIpAddress { get; private set; }

        public byte[] ClientMacAddress { get; private set; }
        public string ClientHardwareAddressPadding { get; private set; }
        public string[] MagicCookie { get; private set; }

        public string ClientHostName
        {
            get
            {
                foreach (DhcpProperty prop in Properties)
                {
                    if (prop.MessageType == DhcpPropertyType.HostName)
                    {
                        return ASCIIEncoding.ASCII.GetString(prop.PropertyData);
                    }
                }
                return "";
            }
        }

        public DhcpMessageType MessageType
        {
            get
            {
                foreach (DhcpProperty prop in Properties)
                {
                    if (prop.MessageType == DhcpPropertyType.MessageType)
                    {
                        return prop.PropertyData.Length > 0 ? (DhcpMessageType)prop.PropertyData[0] : DhcpMessageType.Unknown;
                    }
                }
                return DhcpMessageType.Unknown;
            }
        }

        public string ClientMacAddressStr
        {
            get
            {
                if (ClientMacAddress == null || (ClientMacAddress != null && ClientMacAddress.Length < 6))
                    return "";

                return ClientMacAddress[0].ToString("X2") + "-" +
                       ClientMacAddress[1].ToString("X2") + "-" +
                       ClientMacAddress[2].ToString("X2") + "-" +
                       ClientMacAddress[3].ToString("X2") + "-" +
                       ClientMacAddress[4].ToString("X2") + "-" +
                       ClientMacAddress[5].ToString("X2");
            }
        }

        public DhcpProperty[] Properties { get; private set; }

        public DhcpInfo(byte[] Payload)
        {
            if (!IsDhcpMessage(Payload))
                return;

            int SecondsElapsed_Offset = 8;
            int ClientIpAddress_Offset = 12;
            int YourIpAddress_Offset = 16;
            int NextServerIpAddress_Offset = 20;
            int RelayAgentIpAddress_Offset = 24;
            int ClientMacAddress_Offset = 28;
            int Properties_Offset = 240;

            SecondsElapsed = BitConverter.ToInt16(new byte[] { Payload[SecondsElapsed_Offset + 1], Payload[SecondsElapsed_Offset + 0] }, 0);

            ClientIpAddress = Payload[ClientIpAddress_Offset] + "." + Payload[ClientIpAddress_Offset + 1] + "." + Payload[ClientIpAddress_Offset + 2] + "." + Payload[ClientIpAddress_Offset + 3];
            YourIpAddress = Payload[YourIpAddress_Offset] + "." + Payload[YourIpAddress_Offset + 1] + "." + Payload[YourIpAddress_Offset + 2] + "." + Payload[YourIpAddress_Offset + 3];
            NextServerIpAddress = Payload[NextServerIpAddress_Offset] + "." + Payload[NextServerIpAddress_Offset + 1] + "." + Payload[NextServerIpAddress_Offset + 2] + "." + Payload[NextServerIpAddress_Offset + 3];
            RelayAgentIpAddress = Payload[RelayAgentIpAddress_Offset] + "." + Payload[RelayAgentIpAddress_Offset + 1] + "." + Payload[RelayAgentIpAddress_Offset + 2] + "." + Payload[RelayAgentIpAddress_Offset + 3];

            this.ClientMacAddress = new byte[6];
            Array.Copy(Payload, ClientMacAddress_Offset, ClientMacAddress, 0, ClientMacAddress.Length);

            List <DhcpProperty> props = new List<DhcpProperty>();
                
            while (Properties_Offset + 2 < Payload.Length)
            {
                DhcpProperty property = new DhcpProperty(Payload, Properties_Offset);

                props.Add(property);
                Properties_Offset += property.TotalLength;

                if (property.MessageType == DhcpPropertyType.End)
                    break;
            }
            this.Properties = props.ToArray();
        }

        public static bool IsDhcpMessage(byte[] Payload)
        {
            if (Payload.Length < 240)
            {
                return false;
            }

            //check  the magic cookie
            int ReadOffset = 236;
            return Payload[ReadOffset] == 0x63 && Payload[++ReadOffset] == 0x82 && Payload[++ReadOffset] == 0x53 && Payload[++ReadOffset] == 0x63;
        }

        public class DhcpProperty
        {
            public DhcpPropertyType MessageType { get; private set; }

            public int Length { get; private set; }
            public int TotalLength { get { return Length + 2; } } // +2 = Length Byte + MessageType Byte
            public byte[] PropertyData { get; private set; }

            public DhcpProperty(byte[] Payload, int Offset)
            {
                this.MessageType = (DhcpPropertyType)Payload[Offset++];
                this.Length = Payload[Offset++];
                
                if (Offset + Length < Payload.Length)
                {
                    this.PropertyData = new byte[Length];
                    Array.Copy(Payload, Offset, PropertyData, 0, Length);
                }
                else
                {
                    this.PropertyData = new byte[Payload.Length - Offset];
                    Array.Copy(Payload, Offset, PropertyData, 0, PropertyData.Length);
                }
            }
        }
    }
}