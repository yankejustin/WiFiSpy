using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiFiSpy.src;
using WiFiSpy.src.Packets;
using System.IO;

namespace WiFiSpy.Controls
{
    public partial class StationListControl : UserControl
    {
        private Station[] FilterStations = new Station[0];
        private Station[] InitialStationList = new Station[0];

        public StationListControl()
        {
            InitializeComponent();
        }

        private void btnApplyStationFilter_Click(object sender, EventArgs e)
        {
            FillStationList(InitialStationList);
        }

        public void FillStationList(Station[] stations)
        {
            StationList.Items.Clear();

            if (cbDeviceNameFilter.Checked)
            {
                List<Station> TempStations = new List<Station>();

                foreach (Station station in stations)
                {
                    if (station.DeviceNames.Length > 0)
                    {
                        TempStations.Add(station);
                    }
                }
                stations = TempStations.ToArray();
            }

            if (cbMacAddrFilter.Checked)
            {
                List<Station> TempStations = new List<Station>();

                foreach (Station station in stations)
                {
                    if (station.SourceMacAddressStr.ToLower().Contains(txtMacAddrFilter.Text.ToLower()))
                    {
                        TempStations.Add(station);
                    }
                }
                stations = TempStations.ToArray();
            }

            if (cbProbeFilter.Checked)
            {
                List<Station> TempStations = new List<Station>();

                foreach (Station station in stations)
                {
                    foreach (ProbePacket probe in station.Probes)
                    {
                        if (!String.IsNullOrEmpty(probe.SSID) && probe.SSID.ToLower().Contains(txtProbeFilter.Text.ToLower()))
                        {
                            TempStations.Add(station);
                            break;
                        }
                    }
                }
                stations = TempStations.ToArray();
            }

            if (cbOnlyKnownDevice.Checked)
            {
                List<Station> TempStations = new List<Station>();

                foreach (Station station in stations)
                {
                    if (station.DeviceTypeStr.Length > 0)
                    {
                        TempStations.Add(station);
                    }
                }
                stations = TempStations.ToArray();
            }

            if (cbStationContainsHTTP.Checked)
            {
                List<Station> TempStations = new List<Station>();

                foreach (Station station in stations)
                {
                    foreach (WiFiSpy.src.Packets.DataFrame frame in station.DataFrames)
                    {
                        if ((frame.isIPv4 && (frame.isTCP || frame.isUDP)) && frame.PortDest == 80 || frame.PortSource == 80)
                        {
                            TempStations.Add(station);
                            break;
                        }
                    }
                }
                stations = TempStations.ToArray();
            }

            FilterStations = stations.OrderByDescending(o => o.LastSeenDate).ToArray();
            StationList.VirtualListSize = FilterStations.Length;
        }

        private void StationList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            Station station = FilterStations[e.ItemIndex];

            string IPs = "";
            string Names = "";
            string[] IpAddresses = station.LocalIpAddresses;
            string[] DeviceNames = station.DeviceNames;
            GpsLocation location = station.GetFirstGpsLocation(MainForm.GpsLocations.ToArray());

            for (int i = 0; i < IpAddresses.Length; i++)
            {
                IPs += IpAddresses[i];

                if (i + 1 < IpAddresses.Length)
                    IPs += ", ";
            }

            for (int i = 0; i < DeviceNames.Length; i++)
            {
                Names += DeviceNames[i];

                if (i + 1 < DeviceNames.Length)
                    Names += ", ";
            }

            ListViewItem item = new ListViewItem(new string[]
            {
                station.SourceMacAddressStr,
                station.Manufacturer,
                station.Probes.Length.ToString(),
                station.DataFrames.Length.ToString(),
                station.ProbeNames,
                station.DeviceTypeStr,
                station.DeviceVersion,
                station.LastSeenDate.ToString(MainForm.DateTimeFormat),
                IPs,
                location != null ? location.Longitude.ToString() : "",
                location != null ? location.Latitude.ToString() : "",
                Names
            });
            item.Tag = station;

            e.Item = item;
        }

        private void StationTrafficList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StationList.SelectedIndices.Count > 0)
            {
                Station station = this.FilterStations[StationList.SelectedIndices[0]];

                if (station != null)
                {
                    StationTrafficList.Items.Clear();
                    StationHttpLocList.Items.Clear();

                    foreach (WiFiSpy.src.Packets.DataFrame frame in station.DataFrames)
                    {
                        ListViewItem item = new ListViewItem(new string[]
                        {
                            frame.TimeStamp.ToString(MainForm.DateTimeFormat),
                            frame.SourceIp,
                            frame.PortSource.ToString(),
                            frame.DestIp,
                            frame.PortDest.ToString(),
                            frame.Payload.Length.ToString(),
                            ASCIIEncoding.ASCII.GetString(frame.Payload)
                        });
                        item.Tag = frame;
                        StationTrafficList.Items.Add(item);

                        string HttpLocation = "";
                        if ((HttpLocation = frame.GetHttpLocation()).Length > 0)
                        {
                            ListViewItem HttpItem = new ListViewItem(new string[]
                            {
                                frame.TimeStamp.ToString(MainForm.DateTimeFormat),
                                HttpLocation
                            });
                            HttpItem.Tag = frame;
                            StationHttpLocList.Items.Add(HttpItem);
                        }
                    }
                }
            }
        }

        private void followDeviceByGPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StationList.SelectedIndices.Count > 0)
            {
                Station station = this.FilterStations[StationList.SelectedIndices[0]];

                if (station != null)
                {
                    if (station.HasGpsLocation(MainForm.GpsLocations.ToArray()))
                    {
                        using (SaveFileDialog dialog = new SaveFileDialog())
                        {
                            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                File.WriteAllText(dialog.FileName, GpsLocation.ToKML(station.GetGpsLocations(MainForm.GpsLocations.ToArray())));
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No GPS Data is known for this station");
                    }
                }
            }
        }
    }
}
