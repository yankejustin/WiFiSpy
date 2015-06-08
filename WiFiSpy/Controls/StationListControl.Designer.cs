namespace WiFiSpy.Controls
{
    partial class StationListControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbDeviceNameFilter = new System.Windows.Forms.CheckBox();
            this.cbMacAddrFilter = new System.Windows.Forms.CheckBox();
            this.txtMacAddrFilter = new System.Windows.Forms.TextBox();
            this.cbOnlyKnownDevice = new System.Windows.Forms.CheckBox();
            this.cbStationContainsHTTP = new System.Windows.Forms.CheckBox();
            this.btnApplyStationFilter = new System.Windows.Forms.Button();
            this.txtProbeFilter = new System.Windows.Forms.TextBox();
            this.cbProbeFilter = new System.Windows.Forms.CheckBox();
            this.StationList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.StationTrafficList = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.StationHttpLocList = new System.Windows.Forms.ListView();
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StationMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.followDeviceByGPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.StationMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.panel1);
            this.splitContainer4.Panel1.Controls.Add(this.StationList);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer4.Size = new System.Drawing.Size(1326, 535);
            this.splitContainer4.SplitterDistance = 267;
            this.splitContainer4.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbDeviceNameFilter);
            this.panel1.Controls.Add(this.cbMacAddrFilter);
            this.panel1.Controls.Add(this.txtMacAddrFilter);
            this.panel1.Controls.Add(this.cbOnlyKnownDevice);
            this.panel1.Controls.Add(this.cbStationContainsHTTP);
            this.panel1.Controls.Add(this.btnApplyStationFilter);
            this.panel1.Controls.Add(this.txtProbeFilter);
            this.panel1.Controls.Add(this.cbProbeFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1326, 29);
            this.panel1.TabIndex = 1;
            // 
            // cbDeviceNameFilter
            // 
            this.cbDeviceNameFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDeviceNameFilter.AutoSize = true;
            this.cbDeviceNameFilter.Location = new System.Drawing.Point(172, 7);
            this.cbDeviceNameFilter.Name = "cbDeviceNameFilter";
            this.cbDeviceNameFilter.Size = new System.Drawing.Size(166, 17);
            this.cbDeviceNameFilter.TabIndex = 8;
            this.cbDeviceNameFilter.Text = "Device Name must be known";
            this.cbDeviceNameFilter.UseVisualStyleBackColor = true;
            // 
            // cbMacAddrFilter
            // 
            this.cbMacAddrFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMacAddrFilter.AutoSize = true;
            this.cbMacAddrFilter.Location = new System.Drawing.Point(344, 7);
            this.cbMacAddrFilter.Name = "cbMacAddrFilter";
            this.cbMacAddrFilter.Size = new System.Drawing.Size(132, 17);
            this.cbMacAddrFilter.TabIndex = 7;
            this.cbMacAddrFilter.Text = "Mac Address Contains";
            this.cbMacAddrFilter.UseVisualStyleBackColor = true;
            // 
            // txtMacAddrFilter
            // 
            this.txtMacAddrFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMacAddrFilter.Location = new System.Drawing.Point(482, 5);
            this.txtMacAddrFilter.MaxLength = 50;
            this.txtMacAddrFilter.Name = "txtMacAddrFilter";
            this.txtMacAddrFilter.Size = new System.Drawing.Size(146, 20);
            this.txtMacAddrFilter.TabIndex = 6;
            // 
            // cbOnlyKnownDevice
            // 
            this.cbOnlyKnownDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOnlyKnownDevice.AutoSize = true;
            this.cbOnlyKnownDevice.Location = new System.Drawing.Point(670, 8);
            this.cbOnlyKnownDevice.Name = "cbOnlyKnownDevice";
            this.cbOnlyKnownDevice.Size = new System.Drawing.Size(177, 17);
            this.cbOnlyKnownDevice.TabIndex = 4;
            this.cbOnlyKnownDevice.Text = "Only Show Known Device Type";
            this.cbOnlyKnownDevice.UseVisualStyleBackColor = true;
            // 
            // cbStationContainsHTTP
            // 
            this.cbStationContainsHTTP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbStationContainsHTTP.AutoSize = true;
            this.cbStationContainsHTTP.Location = new System.Drawing.Point(853, 7);
            this.cbStationContainsHTTP.Name = "cbStationContainsHTTP";
            this.cbStationContainsHTTP.Size = new System.Drawing.Size(152, 17);
            this.cbStationContainsHTTP.TabIndex = 3;
            this.cbStationContainsHTTP.Text = "Must contain HTTP Traffic";
            this.cbStationContainsHTTP.UseVisualStyleBackColor = true;
            // 
            // btnApplyStationFilter
            // 
            this.btnApplyStationFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyStationFilter.Location = new System.Drawing.Point(1246, 4);
            this.btnApplyStationFilter.Name = "btnApplyStationFilter";
            this.btnApplyStationFilter.Size = new System.Drawing.Size(75, 23);
            this.btnApplyStationFilter.TabIndex = 2;
            this.btnApplyStationFilter.Text = "Apply Filters";
            this.btnApplyStationFilter.UseVisualStyleBackColor = true;
            this.btnApplyStationFilter.Click += new System.EventHandler(this.btnApplyStationFilter_Click);
            // 
            // txtProbeFilter
            // 
            this.txtProbeFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProbeFilter.Location = new System.Drawing.Point(1094, 6);
            this.txtProbeFilter.Name = "txtProbeFilter";
            this.txtProbeFilter.Size = new System.Drawing.Size(146, 20);
            this.txtProbeFilter.TabIndex = 1;
            // 
            // cbProbeFilter
            // 
            this.cbProbeFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProbeFilter.AutoSize = true;
            this.cbProbeFilter.Location = new System.Drawing.Point(1011, 8);
            this.cbProbeFilter.Name = "cbProbeFilter";
            this.cbProbeFilter.Size = new System.Drawing.Size(79, 17);
            this.cbProbeFilter.TabIndex = 0;
            this.cbProbeFilter.Text = "Probe Filter";
            this.cbProbeFilter.UseVisualStyleBackColor = true;
            // 
            // StationList
            // 
            this.StationList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StationList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader10,
            this.columnHeader4,
            this.columnHeader13,
            this.columnHeader17,
            this.columnHeader16,
            this.columnHeader24,
            this.columnHeader25,
            this.columnHeader26,
            this.columnHeader27});
            this.StationList.ContextMenuStrip = this.StationMenuStrip;
            this.StationList.FullRowSelect = true;
            this.StationList.GridLines = true;
            this.StationList.Location = new System.Drawing.Point(0, 30);
            this.StationList.Name = "StationList";
            this.StationList.Size = new System.Drawing.Size(1326, 234);
            this.StationList.TabIndex = 0;
            this.StationList.UseCompatibleStateImageBehavior = false;
            this.StationList.View = System.Windows.Forms.View.Details;
            this.StationList.VirtualMode = true;
            this.StationList.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.StationList_RetrieveVirtualItem);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Mac Address";
            this.columnHeader1.Width = 161;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Manufacturer";
            this.columnHeader2.Width = 178;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Beacon Broadcast Count";
            this.columnHeader3.Width = 156;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Traffic Packets Count";
            this.columnHeader10.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Probe(s)";
            this.columnHeader4.Width = 241;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Device Type";
            this.columnHeader13.Width = 92;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Device Version";
            this.columnHeader17.Width = 92;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Last Seen";
            this.columnHeader16.Width = 120;
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "Local IP Addresses";
            this.columnHeader24.Width = 140;
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "Longitude";
            this.columnHeader25.Width = 100;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "Latitude";
            this.columnHeader26.Width = 85;
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "Device Name";
            this.columnHeader27.Width = 100;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1326, 264);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1318, 238);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Information";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.StationTrafficList);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1318, 238);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Traffic";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // StationTrafficList
            // 
            this.StationTrafficList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader11,
            this.columnHeader7,
            this.columnHeader12,
            this.columnHeader8,
            this.columnHeader9});
            this.StationTrafficList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StationTrafficList.FullRowSelect = true;
            this.StationTrafficList.GridLines = true;
            this.StationTrafficList.Location = new System.Drawing.Point(3, 3);
            this.StationTrafficList.Name = "StationTrafficList";
            this.StationTrafficList.Size = new System.Drawing.Size(1312, 232);
            this.StationTrafficList.TabIndex = 0;
            this.StationTrafficList.UseCompatibleStateImageBehavior = false;
            this.StationTrafficList.View = System.Windows.Forms.View.Details;
            this.StationTrafficList.SelectedIndexChanged += new System.EventHandler(this.StationTrafficList_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Date Time";
            this.columnHeader5.Width = 146;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Source Ip";
            this.columnHeader6.Width = 118;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Source Port";
            this.columnHeader11.Width = 73;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Dest Ip";
            this.columnHeader7.Width = 107;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Dest Port";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Payload Length";
            this.columnHeader8.Width = 91;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "ASCII Payload";
            this.columnHeader9.Width = 576;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.StationHttpLocList);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1677, 289);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "HTTP Locations";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // StationHttpLocList
            // 
            this.StationHttpLocList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader14,
            this.columnHeader15});
            this.StationHttpLocList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StationHttpLocList.FullRowSelect = true;
            this.StationHttpLocList.GridLines = true;
            this.StationHttpLocList.Location = new System.Drawing.Point(3, 3);
            this.StationHttpLocList.Name = "StationHttpLocList";
            this.StationHttpLocList.Size = new System.Drawing.Size(1671, 283);
            this.StationHttpLocList.TabIndex = 0;
            this.StationHttpLocList.UseCompatibleStateImageBehavior = false;
            this.StationHttpLocList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Date Time";
            this.columnHeader14.Width = 146;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "URL";
            this.columnHeader15.Width = 691;
            // 
            // StationMenuStrip
            // 
            this.StationMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.followDeviceByGPSToolStripMenuItem});
            this.StationMenuStrip.Name = "StationMenuStrip";
            this.StationMenuStrip.Size = new System.Drawing.Size(188, 26);
            // 
            // followDeviceByGPSToolStripMenuItem
            // 
            this.followDeviceByGPSToolStripMenuItem.Name = "followDeviceByGPSToolStripMenuItem";
            this.followDeviceByGPSToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.followDeviceByGPSToolStripMenuItem.Text = "Follow Device by GPS";
            this.followDeviceByGPSToolStripMenuItem.Click += new System.EventHandler(this.followDeviceByGPSToolStripMenuItem_Click);
            // 
            // StationListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer4);
            this.Name = "StationListControl";
            this.Size = new System.Drawing.Size(1326, 535);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.StationMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cbDeviceNameFilter;
        private System.Windows.Forms.CheckBox cbMacAddrFilter;
        private System.Windows.Forms.TextBox txtMacAddrFilter;
        private System.Windows.Forms.CheckBox cbOnlyKnownDevice;
        private System.Windows.Forms.CheckBox cbStationContainsHTTP;
        private System.Windows.Forms.Button btnApplyStationFilter;
        private System.Windows.Forms.TextBox txtProbeFilter;
        private System.Windows.Forms.CheckBox cbProbeFilter;
        private System.Windows.Forms.ListView StationList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader24;
        private System.Windows.Forms.ColumnHeader columnHeader25;
        private System.Windows.Forms.ColumnHeader columnHeader26;
        private System.Windows.Forms.ColumnHeader columnHeader27;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView StationTrafficList;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListView StationHttpLocList;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ContextMenuStrip StationMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem followDeviceByGPSToolStripMenuItem;
    }
}
