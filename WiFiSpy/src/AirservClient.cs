using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace WiFiSpy.src
{
    public class AirservClient
    {
        public const int BUFFER_SIZE = 8192;
        public const int HEADER_SIZE = 5;
        private byte[] Buffer = new byte[BUFFER_SIZE];

        public delegate void PacketArrivedCallback(Packet packet, DateTime ArrivalTime);
        public event PacketArrivedCallback onPacketArrival;
        private Socket client;

        //receive info
        private ReceiveType ReceiveState = ReceiveType.Header;
        private int ReadOffset = 0;
        private int WriteOffset = 0;
        private int ReadableDataLen = 0;
        private int PayloadLen { get { return (int)net.nh_len; } }

        net_hdr net = new net_hdr();

        public enum PacketType
        {
            NET_RC = 1,
            NET_GET_CHAN = 2,
            NET_SET_CHAN = 3,
            NET_WRITE = 4,
            NET_PACKET = 5,
            NET_GET_MAC = 6,
            NET_MAC = 7,
            NET_GET_MONITOR = 8,
            NET_GET_RATE = 9,
            NET_SET_RATE = 10,
        }

        public enum ReceiveType
        {
            Header,
            Payload
        }

        public AirservClient(string Host, int port)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(Host, port);

            client.BeginReceive(Buffer, 0, Buffer.Length, SocketFlags.None, onBeginReceive, null);
        }

        private void onBeginReceive(IAsyncResult ar)
        {
            int BytesTransferred = 0;
            try
            {
                BytesTransferred = client.EndReceive(ar);

                if (BytesTransferred <= 0)
                {
                    Disconnect();
                    return;
                }
            }
            catch { }

            ReadableDataLen += BytesTransferred;

            bool Process = true;

            while (Process)
            {
                if (ReceiveState == ReceiveType.Header)
                {
                    Process = ReadableDataLen >= HEADER_SIZE;

                    if (ReadableDataLen >= HEADER_SIZE)
                    {
                        net.ReadHeader(Buffer, ReadOffset);


                        ReadableDataLen -= HEADER_SIZE;
                        ReadOffset += HEADER_SIZE;
                        ReceiveState = ReceiveType.Payload;
                    }
                }
                else if (ReceiveState == ReceiveType.Payload)
                {
                    Process = ReadableDataLen >= PayloadLen;
                    if (ReadableDataLen >= PayloadLen)
                    {
                        net.ReadPayload(Buffer, ReadOffset);
                        //Debug.WriteLine("Command: " + net.nh_type + ", Len: " + net.nh_len + ", " + BitConverter.ToString(net.nh_data, 0, net.nh_data.Length > 100 ? 100 : net.nh_data.Length));

                        Packet packet = PacketDotNet.Packet.ParsePacket(PacketDotNet.LinkLayers.Ieee80211, net.nh_data);

                        if (packet != null)
                        {
                            DateTime ArrivalTime = DateTime.Now;
                            onPacketArrival(packet, ArrivalTime);
                        }

                        ReadOffset += PayloadLen;
                        ReadableDataLen -= PayloadLen;
                        ReceiveState = ReceiveType.Header;
                    }
                }
            }

            int len = ReceiveState == ReceiveType.Header ? HEADER_SIZE : (int)net.nh_len;
            if (ReadOffset + len >= this.Buffer.Length)
            {
                //no more room for this data size, at the end of the buffer ?

                //copy the buffer to the beginning
                Array.Copy(this.Buffer, ReadOffset, this.Buffer, 0, ReadableDataLen);

                WriteOffset = ReadableDataLen;
                ReadOffset = 0;
            }
            else
            {
                //payload fits in the buffer from the current offset
                //use BytesTransferred to write at the end of the payload
                //so that the data is not split
                WriteOffset += BytesTransferred;
            }

            client.BeginReceive(this.Buffer, WriteOffset, Buffer.Length - WriteOffset, SocketFlags.None, onBeginReceive, null);
        }

        public void Disconnect()
        {
            if (client != null)
                client.Close();

            Buffer = null;
        }

        private class net_hdr
        {
            //Thanks to: https://github.com/aircrack-ng/aircrack-ng/blob/master/src/osdep/network.h
            public PacketType nh_type { get; private set; }
            public uint nh_len { get; private set; }

            public byte[] PayloadHeader { get; private set; }
            public byte[] nh_data { get; private set; }

            public net_hdr()
            {

            }

            public void ReadHeader(byte[] Data, int Offset)
            {
                this.nh_type = (PacketType)Data[Offset];

                Array.Reverse(Data, Offset + 1, 4); //reverse-endian
                this.nh_len = BitConverter.ToUInt32(Data, Offset + 1);
            }

            public void ReadPayload(byte[] Data, int Offset)
            {
                this.PayloadHeader = new byte[(int)nh_len];
                Array.Copy(Data, Offset, PayloadHeader, 0, 32);

                this.nh_data = new byte[(int)nh_len - 32];
                Array.Copy(Data, Offset + 32, nh_data, 0, nh_data.Length);
            }
        }
    }
}