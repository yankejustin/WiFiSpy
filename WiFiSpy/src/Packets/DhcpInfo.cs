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
        public string ClientMacAddress { get; private set; }
        public string ClientHardwareAddressPadding { get; private set; }
        public string[] MagicCookie { get; private set; }

        public DhcpMessageType MessageType { get; private set; }
        public int MaxMessageSize { get; private set; }
        public string VendorClassIdentifier { get; private set; }

        public string ClientHostName
        {
            get
            {
                foreach (DhcpProperty prop in Properties)
                {
                    if (prop.MessageType == DhcpMessageType.HostName)
                    {
                        return ASCIIEncoding.ASCII.GetString(prop.PropertyData);
                    }
                }
                return "";
            }
        }

        public DhcpProperty[] Properties { get; private set; }

        public DhcpInfo(byte[] Payload)
        {
            if (!IsDhcpMessage(Payload))
                return;

            if (Payload[4] == 0x23 && Payload[5] == 0xDE && Payload[6] == 0x48 && Payload[7] == 0x38)
            {

            }

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

            ClientMacAddress = Payload[ClientMacAddress_Offset + 0].ToString("X2") + ":" +
                               Payload[ClientMacAddress_Offset + 1].ToString("X2") + ":" +
                               Payload[ClientMacAddress_Offset + 2].ToString("X2") + ":" +
                               Payload[ClientMacAddress_Offset + 3].ToString("X2") + ":" +
                               Payload[ClientMacAddress_Offset + 4].ToString("X2") + ":" +
                               Payload[ClientMacAddress_Offset + 5].ToString("X2");
            
            List<DhcpProperty> props = new List<DhcpProperty>();
                
            while (Properties_Offset + 2 < Payload.Length)
            {
                DhcpProperty property = new DhcpProperty(Payload, Properties_Offset);


                props.Add(property);
                Properties_Offset += property.TotalLength;

                if (property.MessageType == DhcpMessageType.End)
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
            public DhcpMessageType MessageType { get; private set; }

            public int Length { get; private set; }
            public int TotalLength { get { return Length + 2; } } // +2 = Length Byte + MessageType Byte

            public byte[] PropertyData { get; private set; }

            public DhcpProperty(byte[] Payload, int Offset)
            {
                this.MessageType = (DhcpMessageType)Payload[Offset];
                this.Length = Payload[Offset + 1];

                this.PropertyData = new byte[Length];
                Array.Copy(Payload, Offset + 2, PropertyData, 0, Length);
            }
        }
    }
}