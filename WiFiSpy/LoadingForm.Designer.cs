namespace WiFiSpy
{
    partial class LoadingForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtLoadingFile = new System.Windows.Forms.Label();
            this.txtStationCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAPCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDataFrameCount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loading file: ";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 102);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(521, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // txtLoadingFile
            // 
            this.txtLoadingFile.AutoSize = true;
            this.txtLoadingFile.Location = new System.Drawing.Point(92, 80);
            this.txtLoadingFile.Name = "txtLoadingFile";
            this.txtLoadingFile.Size = new System.Drawing.Size(37, 13);
            this.txtLoadingFile.TabIndex = 2;
            this.txtLoadingFile.Text = "..........";
            // 
            // txtStationCount
            // 
            this.txtStationCount.AutoSize = true;
            this.txtStationCount.Location = new System.Drawing.Point(92, 9);
            this.txtStationCount.Name = "txtStationCount";
            this.txtStationCount.Size = new System.Drawing.Size(37, 13);
            this.txtStationCount.TabIndex = 4;
            this.txtStationCount.Text = "..........";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Stations: ";
            // 
            // txtAPCount
            // 
            this.txtAPCount.AutoSize = true;
            this.txtAPCount.Location = new System.Drawing.Point(92, 31);
            this.txtAPCount.Name = "txtAPCount";
            this.txtAPCount.Size = new System.Drawing.Size(37, 13);
            this.txtAPCount.TabIndex = 6;
            this.txtAPCount.Text = "..........";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Access Points: ";
            // 
            // txtDataFrameCount
            // 
            this.txtDataFrameCount.AutoSize = true;
            this.txtDataFrameCount.Location = new System.Drawing.Point(396, 9);
            this.txtDataFrameCount.Name = "txtDataFrameCount";
            this.txtDataFrameCount.Size = new System.Drawing.Size(37, 13);
            this.txtDataFrameCount.TabIndex = 10;
            this.txtDataFrameCount.Text = "..........";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(323, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Data Frames: ";
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 143);
            this.ControlBox = false;
            this.Controls.Add(this.txtDataFrameCount);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAPCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStationCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLoadingFile);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoadingForm";
            this.ShowIcon = false;
            this.Text = "Loading cap files...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label txtLoadingFile;
        private System.Windows.Forms.Label txtStationCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtAPCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label txtDataFrameCount;
        private System.Windows.Forms.Label label9;

    }
}