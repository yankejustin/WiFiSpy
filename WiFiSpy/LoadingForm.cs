using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiFiSpy.src;

namespace WiFiSpy
{
    public partial class LoadingForm : Form
    {
        Stopwatch sw = Stopwatch.StartNew();
        int StationCount = 0;
        int AccessPointCount = 0;
        int DataFrameCount = 0;
        private CaptureInfo captureInfo;

        public LoadingForm()
        {
            InitializeComponent();
        }

        public void SetCapFile(CapFile capFile)
        {
            capFile.onReadAccessPoint += capFile_onReadAccessPoint;
            capFile.onReadBeacon += capFile_onReadBeacon;
            capFile.onReadDataFrame += capFile_onReadDataFrame;
            capFile.onReadStation += capFile_onReadStation;
        }

        public void SetCaptureInfo(CaptureInfo info)
        {
            this.captureInfo = info;
            info.onDataFrameProgress += Info_onDataFrameProgress;
        }

        private void Info_onDataFrameProgress(int Value, int Max)
        {
            if (sw.ElapsedMilliseconds >= 100)
            {
                sw = Stopwatch.StartNew();
                progressBar1.Maximum = Max;
                progressBar1.Value = Value;

                lblLoadType.Text = "Linking " + Max + " Data Frames to " + captureInfo.Stations.Length + " stations, this might take some time...";
                txtLoadName.Text = "";

                Application.DoEvents();
            }
        }

        void capFile_onReadStation(Station station)
        {
            StationCount++;
            RefreshValues();
        }

        void capFile_onReadDataFrame(src.Packets.DataFrame dataFrame)
        {
            DataFrameCount++;
            RefreshValues();
        }

        void capFile_onReadBeacon(src.Packets.BeaconFrame beacon)
        {
            RefreshValues();
        }

        void capFile_onReadAccessPoint(AccessPoint AP)
        {
            AccessPointCount++;
            RefreshValues();
        }

        public void SetFileName(string name)
        {
            txtLoadName.Text = name;
        }

        public void SetMinMaxProgressBar(int min, int max)
        {
            this.progressBar1.Minimum = min;
            this.progressBar1.Maximum = max;
        }

        public void SetProgressBarValue(int value)
        {
            this.progressBar1.Value = value;
        }

        private void RefreshValues()
        {
            if (sw.ElapsedMilliseconds >= 100)
            {
                sw = Stopwatch.StartNew();

                Application.DoEvents();
                ShowValues();
            }
        }

        private void ShowValues()
        {
            txtStationCount.Text = StationCount.ToString();
            txtAPCount.Text = AccessPointCount.ToString();
            txtDataFrameCount.Text = DataFrameCount.ToString();
        }
    }
}