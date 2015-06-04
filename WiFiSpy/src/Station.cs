﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WiFiSpy.src.Packets;

namespace WiFiSpy.src
{
    public class Station
    {
        public ProbePacket InitialProbe { get; private set; }

        private List<ProbePacket> _probes;
        private List<DataFrame> _payloadTraffic;
        private string _deviceVersion = null;

        public ProbePacket[] Probes
        {
            get
            {
                return _probes.ToArray();
            }
        }
        public DataFrame[] DataFrames
        {
            get
            {
                return _payloadTraffic.OrderBy(o => o.TimeStamp.Ticks).ToArray();
            }
        }

        public byte[] SourceMacAddress
        {
            get
            {
                return InitialProbe.SourceMacAddress;
            }
        }

        public string SourceMacAddressStr
        {
            get
            {
                return BitConverter.ToString(SourceMacAddress);
            }
        }

        /// <summary>
        /// Grab the Local IP Addresses from this station
        /// </summary>
        public string[] LocalIpAddresses
        {
            get
            {
                List<string> IPs = new List<string>();
                foreach(DataFrame dataFrame in DataFrames)
                {
                    if(!String.IsNullOrEmpty(dataFrame.SourceIp) && !String.IsNullOrEmpty(dataFrame.DestIp))
                    {

                        if(dataFrame.SourceIp == "0.0.0.0" || dataFrame.DestIp == "0.0.0.0" ||
                           dataFrame.SourceIp == "255.255.255.255" || dataFrame.DestIp == "255.255.255.255")
                        {
                            continue;
                        }

                        if (dataFrame.SourceMacAddressStr == InitialProbe.SourceMacAddressStr)
                        {
                            if (!IPs.Contains(dataFrame.SourceIp))
                                IPs.Add(dataFrame.SourceIp);
                        }
                    }
                }

                return IPs.ToArray();
            }
        }

        /// <summary>
        /// Get all the device names this user had
        /// </summary>
        public string[] DeviceNames
        {
            get
            {
                List<string> Names = new List<string>();
                foreach (DataFrame dataFrame in DataFrames)
                {
                    DhcpInfo dhcp = dataFrame.DHCP;
                    if (dhcp != null)
                    {
                        if (!Names.Contains(dhcp.ClientHostName))
                        {
                            Names.Add(dhcp.ClientHostName);
                        }
                    }
                }
                return Names.ToArray();
            }
        }

        /// <summary>
        /// The name of the Station that is captured from the DHCP traffic
        /// </summary>
        public string StationName { get; internal set; }

        public DateTime TimeStamp
        {
            get
            {
                return InitialProbe.TimeStamp;
            }
        }

        public bool IsAndroidDevice
        {
            get
            {
                return GetDeviceUserAgent("android") != "";
            }
        }

        public bool IsIPhoneDevice
        {
            get
            {
                return GetDeviceUserAgent("iphone") != "";
            }
        }

        public bool IsIPadDevice
        {
            get
            {
                return GetDeviceUserAgent("ipad") != "";
            }
        }

        public bool IsMacDevice
        {
            get
            {
                return GetDeviceUserAgent("mac") != "";
            }
        }

        public string DeviceTypeStr
        {
            get
            {
                if (IsAndroidDevice)
                    return "Android";

                if (IsIPhoneDevice)
                    return "iPhone";

                if (IsIPadDevice)
                    return "iPad";

                if (IsMacDevice)
                    return "Mac OSX";

                //If it's unknown, let's look at the mac address rather then relying on HTTP Traffic

                if (Manufacturer.ToLower().Contains("apple"))
                    return "Apple";

                if (Manufacturer.ToLower().Contains("samsung") ||
                    Manufacturer.ToLower().Contains("xiaomi") ||
                    Manufacturer.ToLower().Contains("motorola"))
                    return "Android";

                if (Manufacturer.ToLower().Contains("nintendo"))
                    return "Nintendo Console";

                if (Manufacturer.ToLower().Contains("nvidia"))
                    return "NVidia Shield";


                return "";
            }
        }

        public string DeviceVersion
        {
            get
            {
                if (_deviceVersion != null)
                    return _deviceVersion;

                string agent = GetDeviceUserAgent(null);

                if(agent.ToLower().Contains("android") && agent.Contains(";") && agent.Contains("("))
                {
                    //just a simple guess that this will give us the: model + android version
                    _deviceVersion = agent.Substring(agent.IndexOf('('));
                }
                else
                {
                    _deviceVersion = "";
                }

                return _deviceVersion;
            }
        }

        public string Manufacturer {get; private set; }

        public DateTime LastSeenDate
        {
            get
            {
                DateTime LastSeenDate = new DateTime();

                foreach(ProbePacket probe in Probes)
                {
                    if (probe.TimeStamp > LastSeenDate)
                    {
                        LastSeenDate = probe.TimeStamp;
                    }
                }
                return LastSeenDate;
            }
        }

        public string ProbeNames
        {
            get
            {
                List<string> TempProbeNames = new List<string>();
                StringBuilder ProbeNames = new StringBuilder();

                foreach(ProbePacket probe in Probes)
                {
                    if (!String.IsNullOrEmpty(probe.SSID) && !TempProbeNames.Contains(probe.SSID))
                    {
                        ProbeNames.Append(probe.SSID + ",   ");
                        TempProbeNames.Add(probe.SSID);
                    }
                }
                return ProbeNames.ToString();
            }
        }

        public Station(ProbePacket InitialProbe)
        {
            this.InitialProbe = InitialProbe;
            this._probes = new List<ProbePacket>();
            this._payloadTraffic = new List<DataFrame>();
            this.Manufacturer = OuiParser.GetOuiByMac(SourceMacAddress);
        }

        internal void AddProbe(ProbePacket probe)
        {
            lock (_probes)
            {
                this._probes.Add(probe);
            }
        }

        internal void AddDataFrame(DataFrame dataFrame)
        {
            lock (_payloadTraffic)
            {
                this._payloadTraffic.Add(dataFrame);
            }
        }

        private string GetDeviceUserAgent(string contains)
        {
            foreach (DataFrame frame in DataFrames)
            {
                if (frame.IsValidPacket && frame.PortDest == 80 || frame.PortSource == 80)
                {
                    string PayloadStr = ASCIIEncoding.ASCII.GetString(frame.Payload);

                    if (PayloadStr.ToLower().Contains("user-agent:"))
                    {
                        string[] temp = PayloadStr.Split('\n');

                        if (temp != null && temp.Length > 0)
                        {
                            for (int i = 0; i < temp.Length; i++)
                            {
                                if (temp[i].ToLower().StartsWith("user-agent:"))
                                {
                                    if(contains != null)
                                    {
                                        if (temp[i].ToLower().Contains(contains))
                                        {
                                            return temp[i];
                                        }
                                    }
                                    else
                                    {
                                        return temp[i];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return "";
        }

        public bool HasGpsLocation(GpsLocation[] locations)
        {
            for (int i = 0; i < _probes.Count; i++)
            {
                for (int j = 0; j < locations.Length; j++)
                {
                    if (locations[j].IsNearby(_probes[i].TimeStamp))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public GpsLocation GetFirstGpsLocation(GpsLocation[] locations)
        {
            for (int i = 0; i < _probes.Count; i++)
            {
                for (int j = 0; j < locations.Length; j++)
                {
                    if (locations[j].IsNearby(_probes[i].TimeStamp))
                    {
                        return locations[j];
                    }
                }
            }
            return null;
        }

        public GpsLocation[] GetGpsLocations(GpsLocation[] locations)
        {
            List<GpsLocation> gpsLocs = new List<GpsLocation>();
            for (int i = 0; i < _probes.Count; i++)
            {
                for (int j = 0; j < locations.Length; j++)
                {
                    if (locations[j].IsNearby(_probes[i].TimeStamp))
                    {
                        if (gpsLocs.FirstOrDefault(o => o.Time.Ticks == locations[j].Time.Ticks) == null)
                        {
                            gpsLocs.Add(locations[j]);
                        }
                    }
                }
            }
            return gpsLocs.ToArray();
        }
    }
}