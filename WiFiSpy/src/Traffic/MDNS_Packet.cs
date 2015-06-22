using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiFiSpy.src.Packets;

namespace WiFiSpy.src.Traffic
{
    public class MDNS_Packet
    {
        public ushort TransactionId { get; private set; }
        public MDNS_Response Response { get; private set; }
        public int Questions { get; private set; }
        public int AnswerRRs { get; private set; }
        public int AuthorityRRs { get; private set; }
        public int AdditionalRRs { get; private set; }


        public MDNS_Packet(DataFrame DataFrame)
        {
            byte[] Payload = DataFrame.Payload;
            this.TransactionId = BitConverter.ToUInt16(Payload, 0);
            this.Response = (MDNS_Response)BitConverter.ToUInt16(Payload, 2);
            this.Questions = BitConverter.ToUInt16(Payload, 4);
            this.AnswerRRs = BitConverter.ToUInt16(Payload, 6);
            this.AuthorityRRs = BitConverter.ToUInt16(Payload, 8);
            this.AdditionalRRs = BitConverter.ToUInt16(Payload, 10);


        }

        public static bool IsMulticastDNSMessage(DataFrame frame)
        {
            return frame.IsValidPacket && frame.PortDest == 5353 || frame.PortSource == 5353 && frame.PayloadLen > 12;
        }

        public class MDNS_Answer
        {
            public string Name { get; private set; }
            public int Type { get; private set; }

            public int DnsType { get; private set; }

            public int TimeToLive { get; private set; }
            public int DataLength { get; private set; }
            public int TextLength { get; private set; }
            public int Text { get; private set; }
        }

    }
}