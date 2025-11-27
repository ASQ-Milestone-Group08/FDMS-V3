namespace GroundTerminalSystem
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        /// 
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            //TAB CONTROLS
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMonitoring = new System.Windows.Forms.TabPage();
            this.tabSearch = new System.Windows.Forms.TabPage();

            //real-time toggle control
            this.panelTop = new System.Windows.Forms.Panel();
            this.toggleRT = new System.Windows.Forms.CheckBox();
            this.lblRealTimeStatus = new System.Windows.Forms.Label();

            //TELEMETRY DATA
            this.panelLeft = new System.Windows.Forms.Panel();
            //tail #
            this.lblTail = new System.Windows.Forms.Label();
            this.txtTail = new System.Windows.Forms.TextBox();
            //timestamp
            this.lblTimestamp = new System.Windows.Forms.Label();
            this.txtTimestamp = new System.Windows.Forms.TextBox();
            //altitude
            this.lblAltitude = new System.Windows.Forms.Label();
            this.txtAltitude = new System.Windows.Forms.TextBox();
            //weight
            this.lblWeight = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            //pitch
            this.lblPitch = new System.Windows.Forms.Label();
            this.txtPitch = new System.Windows.Forms.TextBox();
            //bank
            this.lblBank = new System.Windows.Forms.Label();
            this.txtBank = new System.Windows.Forms.TextBox();

            //start/stop buttons
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();

            //CHART PANELS
            this.panelCharts = new System.Windows.Forms.Panel();
            this.chartGforce = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartAltitude = new System.Windows.Forms.DataVisualization.Charting.Chart();

            //DISPLAY PROPERTIES
            //form
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.Text = "FDMS Ground Terminal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //tab control
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Controls.Add(this.tabMonitoring);
            this.tabControl.Controls.Add(this.tabSearch);

            //monitoring tab
            this.tabMonitoring.Text = "Monitoring";
            this.tabMonitoring.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 50;
            this.panelTop.Padding = new System.Windows.Forms.Padding(10);

            //toggleRT enabled
            this.toggleRT.Appearance = System.Windows.Forms.Appearance.Button;
            this.toggleRT.Text = "Real-Time";
            this.toggleRT.AutoSize = true;
            this.toggleRT.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.toggleRT.BackColor = System.Drawing.Color.LightGray;
            this.toggleRT.Location = new System.Drawing.Point(10, 10);
            this.toggleRT.CheckedChanged += new System.EventHandler(this.toggleRealTime);

            //status label
            this.lblRealTimeStatus.Text = "OFF";
            this.lblRealTimeStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRealTimeStatus.AutoSize = true;
            this.lblRealTimeStatus.Location = new System.Drawing.Point(130, 14);
            this.panelTop.Controls.Add(this.toggleRT);
            this.panelTop.Controls.Add(this.lblRealTimeStatus);

            //left panel layout
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Width = 350;
            this.panelLeft.Padding = new System.Windows.Forms.Padding(12);

            //variables for layout
            int y = 20;
            int labelX = 10;
            int boxX = 120;
            int w = 180;
            int h = 26;
            int gap = 40;

            //tail
            this.lblTail.Text = "Tail #:";
            this.lblTail.Location = new System.Drawing.Point(labelX, y);
            this.lblTail.AutoSize = true;
            this.txtTail.Location = new System.Drawing.Point(boxX, y - 4);
            this.txtTail.Width = w;
            this.txtTail.ReadOnly = true;
            y += gap;
            //timestamp
            this.lblTimestamp.Text = "Timestamp:";
            this.lblTimestamp.Location = new System.Drawing.Point(labelX, y);
            this.lblTimestamp.AutoSize = true;
            this.txtTimestamp.Location = new System.Drawing.Point(boxX, y - 4);
            this.txtTimestamp.Width = w;
            this.txtTimestamp.ReadOnly = true;
            y += gap;
            //altitude
            this.lblAltitude.Text = "Altitude:";
            this.lblAltitude.Location = new System.Drawing.Point(labelX, y);
            this.lblAltitude.AutoSize = true;
            this.txtAltitude.Location = new System.Drawing.Point(boxX, y - 4);
            this.txtAltitude.Width = w;
            this.txtAltitude.ReadOnly = true;
            y += gap;
            //weight
            this.lblWeight.Text = "Weight (kg):";
            this.lblWeight.Location = new System.Drawing.Point(labelX, y);
            this.lblWeight.AutoSize = true;
            this.txtWeight.Location = new System.Drawing.Point(boxX, y - 4);
            this.txtWeight.Width = w;
            this.txtWeight.ReadOnly = true;
            y += gap;
            //pitch
            this.lblPitch.Text = "Pitch:";
            this.lblPitch.Location = new System.Drawing.Point(labelX, y);
            this.lblPitch.AutoSize = true;
            this.txtPitch.Location = new System.Drawing.Point(boxX, y - 4);
            this.txtPitch.Width = w;
            this.txtPitch.ReadOnly = true;
            y += gap;
            //bank
            this.lblBank.Text = "Bank:";
            this.lblBank.Location = new System.Drawing.Point(labelX, y);
            this.lblBank.AutoSize = true;
            this.txtBank.Location = new System.Drawing.Point(boxX, y - 4);
            this.txtBank.Width = w;
            this.txtBank.ReadOnly = true;
            y += gap;

            //StartSim button
            this.btnStart.Text = "Start Real-Time";
            this.btnStart.Location = new System.Drawing.Point(20, y + 20);
            this.btnStart.Width = 130;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            //StopSim button
            this.btnStop.Text = "Stop Real-Time";
            this.btnStop.Location = new System.Drawing.Point(170, y + 20);
            this.btnStop.Width = 130;
            this.btnStop.Enabled = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

            //control layout oof the telemetry panel
            this.panelLeft.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblTail, this.txtTail,
                this.lblTimestamp, this.txtTimestamp,
                this.lblAltitude, this.txtAltitude,
                this.lblWeight, this.txtWeight,
                this.lblPitch, this.txtPitch,
                this.lblBank, this.txtBank,
                this.btnStart, this.btnStop
            });
        }

        #endregion

        //FIELD DECLARATIONS
        //tabs
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMonitoring;
        private System.Windows.Forms.TabPage tabSearch;
        //real-time toggle/status
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.CheckBox toggleRT;
        private System.Windows.Forms.Label lblRealTimeStatus;
        //telemetry data panel and labels
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblTail;
        private System.Windows.Forms.Label lblTimestamp;
        private System.Windows.Forms.Label lblAltitude;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label lblPitch;
        private System.Windows.Forms.Label lblBank;
        //telemetry data textboxes
        private System.Windows.Forms.TextBox txtTail;
        private System.Windows.Forms.TextBox txtTimestamp;
        private System.Windows.Forms.TextBox txtAltitude;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.TextBox txtPitch;
        private System.Windows.Forms.TextBox txtBank;
        //buttons
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        //chart panels
        private System.Windows.Forms.Panel panelCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGforce;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAltitude;
    }
}
