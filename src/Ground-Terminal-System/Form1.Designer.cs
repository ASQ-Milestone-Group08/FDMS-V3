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
            this.Load += new System.EventHandler(this.Form1_Load);


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


            //SEARCH TAB CONTROLS
            this.lblSearchTail = new System.Windows.Forms.Label();
            this.txtSearchTail = new System.Windows.Forms.TextBox();
            //start date
            this.lblStart = new System.Windows.Forms.Label();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            //end date
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            //parameter
            this.lblParam = new System.Windows.Forms.Label();
            this.cmbParameter = new System.Windows.Forms.ComboBox();
            //search and export buttons
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            //data grids
            this.dgvG = new System.Windows.Forms.DataGridView();
            this.dgvAlt = new System.Windows.Forms.DataGridView();


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
            this.tabMonitoring.Size = new System.Drawing.Size(1200, 760);
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
            this.panelLeft.Width = 350;
            this.panelLeft.MinimumSize = new System.Drawing.Size(50, 0);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;

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

            //control layout of the telemetry panel
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

            //chart panel display
            this.panelCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCharts.Padding = new System.Windows.Forms.Padding(10);

            //G-Force display chart
            this.chartGforce.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartGforce.Height = 300;   // TEMP height to prevent 0px crash
            this.chartGforce.MinimumSize = new System.Drawing.Size(0, 50);  // safety
            this.panelCharts.Controls.Add(this.chartGforce);

            var gArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea("GArea");
            this.chartGforce.ChartAreas.Add(gArea);

            var sNx = new System.Windows.Forms.DataVisualization.Charting.Series("Nx");
            var sNy = new System.Windows.Forms.DataVisualization.Charting.Series("Ny");
            var sNz = new System.Windows.Forms.DataVisualization.Charting.Series("Nz");

            sNx.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            sNy.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            sNz.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            this.chartGforce.Series.Add(sNx);
            this.chartGforce.Series.Add(sNy);
            this.chartGforce.Series.Add(sNz);

            //altitude chart panel
            this.chartAltitude.Dock = System.Windows.Forms.DockStyle.Fill;
            var altArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea("AltArea");
            this.chartAltitude.ChartAreas.Add(altArea);

            var sAlt = new System.Windows.Forms.DataVisualization.Charting.Series("Altitude");
            sAlt.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            this.chartAltitude.Series.Add(sAlt);
            this.panelCharts.Controls.Add(this.chartAltitude);

            //add chart panels to monitoring tab
            this.tabMonitoring.Controls.Add(this.panelCharts);
            this.tabMonitoring.Controls.Add(this.panelLeft);
            this.tabMonitoring.Controls.Add(this.panelTop);

            //SEARCH TAB
            this.tabSearch.Text = "Search Database";
            this.tabSearch.BackColor = System.Drawing.Color.WhiteSmoke;

            int sx = 20;
            int sy = 20;

            //search tail
            this.lblSearchTail.Text = "Tail #:";
            this.lblSearchTail.Location = new System.Drawing.Point(sx, sy);
            this.lblSearchTail.AutoSize = true;
            this.txtSearchTail.Location = new System.Drawing.Point(sx + 80, sy - 3);
            this.txtSearchTail.Width = 100;
            //search parameter
            this.lblParam.Text = "Parameter:";
            this.lblParam.Location = new System.Drawing.Point(sx + 200, sy);
            this.lblParam.AutoSize = true;
            this.cmbParameter.Location = new System.Drawing.Point(sx + 280, sy - 3);
            this.cmbParameter.Width = 150;
            //search start date
            this.lblStart.Text = "Start Date:";
            this.lblStart.Location = new System.Drawing.Point(sx, sy + 45);
            this.lblStart.AutoSize = true;
            this.dtStart.Location = new System.Drawing.Point(sx + 80, sy + 40);
            this.dtStart.Width = 150;
            //search end date
            this.lblEnd.Text = "End Date:";
            this.lblEnd.Location = new System.Drawing.Point(sx + 250, sy + 45);
            this.lblEnd.AutoSize = true;
            this.dtEnd.Location = new System.Drawing.Point(sx + 280, sy + 40);
            this.dtEnd.Width = 150;

            //search button
            this.btnSearch.Text = "Search";
            this.btnSearch.Location = new System.Drawing.Point(sx + 460, sy + 20);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            //export button
            this.btnExport.Text = "Export";
            this.btnExport.Location = new System.Drawing.Point(20, 550);
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);


            //control layout in search mode
            this.tabSearch.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblSearchTail, this.txtSearchTail,
                this.lblParam, this.cmbParameter,
                this.lblStart, this.dtStart,
                this.lblEnd, this.dtEnd,
                this.btnSearch,
                this.dgvG, this.dgvAlt,
                this.btnExport
            });



        }

        #endregion

        //FIELD DECLARATIONS
        //tabs
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMonitoring;
        private System.Windows.Forms.TabPage tabSearch;

        //MONITORING 
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

        //SEARCH TAB
        private System.Windows.Forms.Label lblSearchTail;
        private System.Windows.Forms.TextBox txtSearchTail;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Label lblParam;
        private System.Windows.Forms.ComboBox cmbParameter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnExport;
        //data grids
        private System.Windows.Forms.DataGridView dgvG;
        private System.Windows.Forms.DataGridView dgvAlt;
    }
}
