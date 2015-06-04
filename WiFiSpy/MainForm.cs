﻿using PacketDotNet.Ieee80211;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WiFiSpy.src;
using WiFiSpy.src.GpsParsers;
using WiFiSpy.src.Packets;

namespace WiFiSpy
{
    public partial class MainForm : Form
    {
        public List<CapFile> CapFiles { get; private set; }
        public List<GpsLocation> GpsLocations { get; private set; }
        public const string DateTimeFormat = "dd-MM-yyyy HH:mm:ss";

        public MainForm()
        {
            InitializeComponent();
            OuiParser.Initialize("./Data/OUI.txt");
            this.CapFiles = new List<CapFile>();
            this.GpsLocations = new List<GpsLocation>();

            RefreshAll();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void importCapFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Multiselect = true;
                dialog.Title = "Select the capture file(s)";

                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bool RefreshFiles = false;
                    foreach(string filePath in dialog.FileNames)
                    {
                        if(File.Exists(filePath))
                        {
                            try
                            {
                                FileInfo inf = new FileInfo(filePath);
                                File.Copy(filePath, Environment.CurrentDirectory + "\\Data\\Captures\\" + inf.Name, true);
                                RefreshFiles = true;
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }

                    if(RefreshFiles)
                    {
                        ReadCapFiles();
                    }
                }
            }
        }

        private void ReadCapFiles()
        {
            LoadingForm LoadForm = new LoadingForm();
            LoadForm.Show();

            CapFiles.Clear();
            foreach (string capFilePath in Directory.GetFiles(Environment.CurrentDirectory + "\\Data\\Captures", "*.cap"))
            {
                CapFile capFile = new CapFile();


                LoadForm.SetCapFile(capFile);
                LoadForm.SetFileName(new FileInfo(capFilePath).Name);


                capFile.ReadCap(capFilePath);

                CapFiles.Add(capFile);
            }
            LoadForm.Close();
        }

        private void RefreshGpsLocations()
        {
            GpsLocations.Clear();

            foreach (string FilePath in Directory.GetFiles(Environment.CurrentDirectory + "\\Data\\GPS", "*.csv"))
            {
                GpsLocations.AddRange(GpsCsvParser.GetLocations(FilePath));
            }

            foreach (string FilePath in Directory.GetFiles(Environment.CurrentDirectory + "\\Data\\GPS", "*.gpx"))
            {
                GpsLocations.AddRange(GpxParser.GetLocations(FilePath));
            }
        }

        private void RefreshAll()
        {
            ReadCapFiles();
            RefreshGpsLocations();

            FillHourlyChart();
            FillPieChart();
            FillStationWeekOverview();

            FillTrafficChart();

            FillStationList();
            FillExtenderList();
        }

        private void FillExtenderList()
        {
            LvRepeaterNames.Items.Clear();
            LvRepeaterList.Items.Clear();

            SortedList<string, List<AccessPoint>> repeaters = CapManager.GetPossibleExtenders(CapFiles.ToArray());

            for (int i = 0; i < repeaters.Count; i++)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    repeaters.Keys[i],
                    repeaters.Values[i].Count.ToString()
                });
                item.Tag = repeaters.Values[i];
                LvRepeaterNames.Items.Add(item);
            }
        }

        private void FillStationList()
        {
            StationList.Items.Clear();
            Station[] stations = CapManager.GetStations(CapFiles.ToArray());

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

            if(cbProbeFilter.Checked)
            {
                List<Station> TempStations = new List<Station>();

                foreach(Station station in stations)
                {
                    foreach(ProbePacket probe in station.Probes)
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

            if(cbOnlyKnownDevice.Checked)
            {
                List<Station> TempStations = new List<Station>();

                foreach (Station station in stations)
                {
                    if(station.DeviceTypeStr.Length > 0)
                    {
                        TempStations.Add(station);
                    }
                }
                stations = TempStations.ToArray();
            }

            if(cbStationContainsHTTP.Checked)
            {
                List<Station> TempStations = new List<Station>();

                foreach (Station station in stations)
                {
                    foreach(WiFiSpy.src.Packets.DataFrame frame in station.DataFrames)
                    {
                        if((frame.isIPv4 && (frame.isTCP || frame.isUDP)) && frame.PortDest == 80 || frame.PortSource == 80)
                        {
                            TempStations.Add(station);
                            break;
                        }
                    }
                }
                stations = TempStations.ToArray();
            }

            List<ListViewItem> ListItems = new List<ListViewItem>();
            foreach(Station station in stations)
            {
                StringBuilder IPs = new StringBuilder();
                StringBuilder Names = new StringBuilder();
                string[] IpAddresses = station.LocalIpAddresses;
                string[] DeviceNames = station.DeviceNames;
                GpsLocation location = station.GetFirstGpsLocation(GpsLocations.ToArray());

                for (int i = 0; i < IpAddresses.Length; i++)
                {
                    IPs.Append(IpAddresses[i]);

                    if (i + 1 < IpAddresses.Length)
                        IPs.Append(", ");
                }

                for (int i = 0; i < DeviceNames.Length; i++)
                {
                    Names.Append(DeviceNames[i]);

                    if (i + 1 < DeviceNames.Length)
                        Names.Append(", ");
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
                    station.LastSeenDate.ToString(DateTimeFormat),
                    IPs.ToString(),
                    location != null ? location.Longitude.ToString() : "",
                    location != null ? location.Latitude.ToString() : "",
                    Names.ToString()
                });
                item.Tag = station;
                //StationList.Items.Add(item);
                ListItems.Add(item);
            }
            StationList.Items.AddRange(ListItems.OrderByDescending(o => (o.Tag as Station).LastSeenDate).ToArray());
        }

        private void FillHourlyChart()
        {
            AllHourlyChart.Titles.Clear();
            AllHourlyChart.Series.Clear();


            AllHourlyChart.Titles.Add("Hourly Overview");

            Series APSerie = AllHourlyChart.Series.Add("Access Points");
            APSerie.LegendText = "Access Points";

            Series HiddenAPSerie = AllHourlyChart.Series.Add("Hidden Access Points");
            HiddenAPSerie.LegendText = "Hidden Access Points";

            Series StationsSerie = AllHourlyChart.Series.Add("Stations");
            StationsSerie.LegendText = "Stations";

            int TotalAPCount = 0;
            int TotalHiddenAPCount = 0;
            int TotalStationCount = 0;

            for (int i = 1; i < 24; i++)
            {
                int count = 0;
                int hiddenCount = 0;
                int stationCount = 0;
                foreach (CapFile capFile in CapFiles)
                {
                    count += capFile.AccessPoints.Where(o => o.TimeStamp.Hour == i).Count();
                    hiddenCount += capFile.AccessPoints.Where(o => o.BeaconFrame.IsHidden && o.TimeStamp.Hour == i).Count();
                    stationCount += capFile.Stations.Where(o => o.TimeStamp.Hour == i).Count();
                }

                APSerie.Points.AddXY(i, count);
                HiddenAPSerie.Points.AddXY(i, hiddenCount);
                StationsSerie.Points.AddXY(i, stationCount);

                TotalAPCount += count;
                TotalHiddenAPCount += hiddenCount;
                TotalStationCount += stationCount;
            }

            APSerie.LegendText += " (" + TotalAPCount + ")";
            HiddenAPSerie.LegendText += " (" + TotalHiddenAPCount + ")";
            StationsSerie.LegendText += " (" + TotalStationCount + ")";
        }

        private void FillPieChart()
        {
            APInfoPieChart.Titles.Clear();
            APInfoPieChart.Series.Clear();

            int APCount_Pie = 0;
            int hiddenCount_Pie = 0;
            int WpsEnabledCount_Pie = 0;

            foreach (CapFile capFile in CapFiles)
            {
                APCount_Pie += capFile.AccessPoints.Where(o => !o.BeaconFrame.IsHidden).Count();
                hiddenCount_Pie += capFile.AccessPoints.Where(o => o.BeaconFrame.IsHidden).Count();
                WpsEnabledCount_Pie += capFile.AccessPoints.Where(o => o.WPS_Enabled).Count();
            }

            Series APSerie_Pie = new Series
            {
                Name = "Access Points",
                IsVisibleInLegend = true,
                ChartType = SeriesChartType.Pie
            };

            DataPoint DataPointPie = APSerie_Pie.Points.Add(APCount_Pie);
            DataPointPie.LegendText = "Access Points (" + APCount_Pie + ")";

            DataPoint hiddenDataPointPie = APSerie_Pie.Points.Add(hiddenCount_Pie);
            hiddenDataPointPie.LegendText = "Hidden Access Points (" + hiddenCount_Pie + ")";

            DataPoint WpsEnabledDataPointPie = APSerie_Pie.Points.Add(WpsEnabledCount_Pie);
            WpsEnabledDataPointPie.LegendText = "WPS Enabled (" + WpsEnabledCount_Pie + ")";



            APInfoPieChart.Series.Add(APSerie_Pie);
        }

        private void FillStationWeekOverview()
        {
            WeekStationOverviewChart.Titles.Clear();
            WeekStationOverviewChart.Series.Clear();

            DateTime WeekStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            WeekStartDate = WeekStartDate.AddDays(DayOfWeek.Monday - WeekStartDate.DayOfWeek);

            SortedList<string, int> DeviceList = new SortedList<string, int>();
            SortedList<string, int> DeviceListCount = new SortedList<string, int>();

            Series UniqueStationsSerie = WeekStationOverviewChart.Series.Add("Unique Stations");
            UniqueStationsSerie.LegendText = "Unique Stations";

            foreach (Station station in CapManager.GetStations(CapFiles.ToArray()))
            {
                if (station.DeviceTypeStr.Length > 0)
                {
                    if (!DeviceList.ContainsKey(station.DeviceTypeStr))
                    {
                        Series TempSerie = WeekStationOverviewChart.Series.Add(station.DeviceTypeStr);
                        TempSerie.LegendText = station.DeviceTypeStr;

                        DeviceList.Add(station.DeviceTypeStr, 0);
                        DeviceListCount.Add(station.DeviceTypeStr, 0);
                    }
                }
            }

            int TotalStationCount = 0;
            SortedList<DayOfWeek, List<string>> StationMacs = new SortedList<DayOfWeek, List<string>>();
            StationMacs.Add(DayOfWeek.Monday, new List<string>());
            StationMacs.Add(DayOfWeek.Tuesday, new List<string>());
            StationMacs.Add(DayOfWeek.Wednesday, new List<string>());
            StationMacs.Add(DayOfWeek.Thursday, new List<string>());
            StationMacs.Add(DayOfWeek.Friday, new List<string>());
            StationMacs.Add(DayOfWeek.Saturday, new List<string>());
            StationMacs.Add(DayOfWeek.Sunday, new List<string>());

            for(int i = 0; i < StationMacs.Count; i++)
            {
                foreach (CapFile capFile in CapFiles)
                {
                    foreach(Station station in capFile.Stations.Where(o => WeekStartDate.Year == o.TimeStamp.Year &&
                                                                           WeekStartDate.Month == o.TimeStamp.Month &&
                                                                           WeekStartDate.Day == o.TimeStamp.Day))
                    {
                        if(!String.IsNullOrEmpty(station.SourceMacAddressStr))
                        {
                            if(!StationMacs.Values[i].Contains(station.SourceMacAddressStr))
                            {
                                if(DeviceList.ContainsKey(station.DeviceTypeStr))
                                {
                                    DeviceList[station.DeviceTypeStr]++;
                                    DeviceListCount[station.DeviceTypeStr]++;
                                }

                                StationMacs.Values[i].Add(station.SourceMacAddressStr);
                                TotalStationCount++;
                            }
                        }
                    }
                }
                UniqueStationsSerie.Points.AddXY(StationMacs.Keys[i].ToString(), StationMacs.Values[i].Count);

                for (int j = 0; j < DeviceList.Count; j++)
                {
                    WeekStationOverviewChart.Series[DeviceList.Keys[j]].Points.AddXY(DeviceList.Keys[j], DeviceList.Values[j]);
                    DeviceList[DeviceList.Keys[j]] = 0;
                }

                WeekStartDate = WeekStartDate.AddDays(1);
            }

            UniqueStationsSerie.LegendText += " (" + TotalStationCount + ")";
            StationWeekOverviewBox.Text = "Stations week overview (" + WeekStartDate.ToShortDateString() + " - " + WeekStartDate.AddDays(7).ToShortDateString() + ")";

            for (int i = 0; i < DeviceListCount.Count; i++)
            {
                WeekStationOverviewChart.Series[DeviceListCount.Keys[i]].LegendText += " (" + DeviceListCount.Values[i] + ")";
            }
        }

        private void FillTrafficChart()
        {
            TrafficPieChart.Titles.Clear();
            TrafficPieChart.Series.Clear();

            int FTP = 0;
            int SFTP = 0;
            int HTTP = 0;
            int HTTPS = 0;
            int SMTP = 0;
            int DNS = 0;
            int VNC = 0;

            foreach (CapFile capFile in CapFiles)
            {
                FTP += capFile.DataFrames.Where(o => o.isIPv4 && o.isTCP && (o.PortSource == 20 || o.PortDest == 20)).Count();
                SFTP += capFile.DataFrames.Where(o => o.isIPv4 && o.isTCP && (o.PortSource == 22 || o.PortDest == 22)).Count();
                HTTP += capFile.DataFrames.Where(o => o.isIPv4 && o.isTCP && (o.PortSource == 80 || o.PortDest == 80)).Count();
                HTTPS += capFile.DataFrames.Where(o => o.isIPv4 && o.isTCP && (o.PortSource == 443 || o.PortDest == 443)).Count();
                DNS += capFile.DataFrames.Where(o => o.isIPv4 && o.isTCP && (o.PortSource == 53 || o.PortDest == 53)).Count();

                VNC += capFile.DataFrames.Where(o => o.isIPv4 && o.isTCP && (o.PortSource == 5500 || o.PortDest == 5500)).Count();
                VNC += capFile.DataFrames.Where(o => o.isIPv4 && o.isTCP && (o.PortSource == 5800 || o.PortDest == 5800)).Count();
                VNC += capFile.DataFrames.Where(o => o.isIPv4 && o.isTCP && (o.PortSource == 5900 || o.PortDest == 5900)).Count();
            }

            Series APSerie_Pie = new Series
            {
                Name = "Traffic",
                IsVisibleInLegend = true,
                ChartType = SeriesChartType.Pie
            };

            DataPoint FTPPie = APSerie_Pie.Points.Add(FTP);
            FTPPie.LegendText = "FTP (" + FTP + " packets)";

            DataPoint SFTPPie = APSerie_Pie.Points.Add(SFTP);
            SFTPPie.LegendText = "SFTP (" + SFTP + " packets)";

            DataPoint HTTPPie = APSerie_Pie.Points.Add(HTTP);
            HTTPPie.LegendText = "HTTP (" + HTTP + " packets)";

            DataPoint HTTPSPie = APSerie_Pie.Points.Add(HTTPS);
            HTTPSPie.LegendText = "HTTPS (" + HTTPS + " packets)";

            DataPoint SMTPPie = APSerie_Pie.Points.Add(SMTP);
            SMTPPie.LegendText = "SMTP (" + SMTP + " packets)";

            DataPoint DNSPie = APSerie_Pie.Points.Add(DNS);
            DNSPie.LegendText = "DNS (" + DNS + " packets)";

            DataPoint VNCPie = APSerie_Pie.Points.Add(VNC);
            VNCPie.LegendText = "VNC (" + VNC + " packets)";


            TrafficPieChart.Series.Add(APSerie_Pie);
        }

        private void btnApplyStationFilter_Click(object sender, EventArgs e)
        {
            FillStationList();
        }

        private void StationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(StationList.SelectedItems.Count > 0)
            {
                Station station = StationList.SelectedItems[0].Tag as Station;

                if(station != null)
                {
                    StationTrafficList.Items.Clear();
                    StationHttpLocList.Items.Clear();

                    foreach (WiFiSpy.src.Packets.DataFrame frame in station.DataFrames)
                    {
                        ListViewItem item = new ListViewItem(new string[]
                        {
                            frame.TimeStamp.ToString(DateTimeFormat),
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
                        if((HttpLocation = frame.GetHttpLocation()).Length > 0)
                        {
                            ListViewItem HttpItem = new ListViewItem(new string[]
                            {
                                frame.TimeStamp.ToString(DateTimeFormat),
                                HttpLocation
                            });
                            HttpItem.Tag = frame;
                            StationHttpLocList.Items.Add(HttpItem);
                        }
                    }
                }
            }
        }

        private void LvRepeaterNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LvRepeaterNames.SelectedItems.Count > 0)
            {
                LvRepeaterList.Items.Clear();

                List<AccessPoint> Repeaters = LvRepeaterNames.SelectedItems[0].Tag as List<AccessPoint>;

                if (Repeaters != null)
                {
                    foreach (AccessPoint AP in Repeaters)
                    {
                        ListViewItem item = new ListViewItem(new string[]
                        {
                            AP.MacAddress,
                            AP.Manufacturer,
                            AP.BeaconFrame.TimeStamp.ToString(DateTimeFormat),
                            AP.WPS_Enabled.ToString()
                        });
                        item.Tag = AP;
                        LvRepeaterList.Items.Add(item);
                    }
                }
            }
        }

        private void followDeviceByGPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StationList.SelectedItems.Count > 0)
            {
                Station station = StationList.SelectedItems[0].Tag as Station;

                if (station != null)
                {
                    if (station.HasGpsLocation(GpsLocations.ToArray()))
                    {
                        using (SaveFileDialog dialog = new SaveFileDialog())
                        {
                            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                File.WriteAllText(dialog.FileName, GpsLocation.ToKML(station.GetGpsLocations(this.GpsLocations.ToArray())));
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