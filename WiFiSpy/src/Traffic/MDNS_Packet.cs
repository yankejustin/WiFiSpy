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