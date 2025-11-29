/*
 * Filename		: Form1.Designer.cs
 * Project		: Ground-Terminal-System
 * By			: Erin Garcia
 * Date 		: 2025-11-28
 * Description	: This is the designer partial class for the main form of the Ground Terminal System application.
 */


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
            this.lblTailValue = new System.Windows.Forms.Label();
            //timestamp
            this.lblTimestamp = new System.Windows.Forms.Label();
            this.lblTimestampValue = new System.Windows.Forms.Label();
            //altitude
            this.lblAltitude = new System.Windows.Forms.Label();
            this.lblAltitudeValue = new System.Windows.Forms.Label();
            //weight
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblWeightValue = new System.Windows.Forms.Label();
            //pitch
            this.lblPitch = new System.Windows.Forms.Label();
            this.lblPitchValue = new System.Windows.Forms.Label();
            //bank
            this.lblBank = new System.Windows.Forms.Label();
            this.lblBankValue = new System.Windows.Forms.Label();

            //start/stop buttons
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();

            //CHART PANELS - NEW STABLE LAYOUT
            this.panelCharts = new System.Windows.Forms.Panel();
            this.panelG = new System.Windows.Forms.Panel();
            this.panelAlt = new System.Windows.Forms.Panel();
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
            Font valueFont = new Font("Segoe UI", 10F, FontStyle.Bold);


            //tail
            this.lblTail.Text = "Tail #:";
            this.lblTail.Location = new System.Drawing.Point(labelX, y);
            this.lblTail.AutoSize = true;
            this.lblTailValue.Location = new Point(boxX, y);
            this.lblTailValue.Width = w;
            this.lblTailValue.Font = valueFont;
            y += gap;
            //timestamp
            this.lblTimestamp.Text = "Timestamp:";
            this.lblTimestamp.Location = new System.Drawing.Point(labelX, y);
            this.lblTimestamp.AutoSize = true;
            this.lblTimestampValue.Location = new Point(boxX, y);
            this.lblTimestampValue.Width = w;
            this.lblTimestampValue.Font = valueFont;
            y += gap;
            //altitude
            this.lblAltitude.Text = "Altitude:";
            this.lblAltitude.Location = new System.Drawing.Point(labelX, y);
            this.lblAltitude.AutoSize = true;
            this.lblAltitudeValue.Location = new Point(boxX, y);
            this.lblAltitudeValue.Width = w;
            this.lblAltitudeValue.Font = valueFont;
            y += gap;
            //weight
            this.lblWeight.Text = "Weight (kg):";
            this.lblWeight.Location = new System.Drawing.Point(labelX, y);
            this.lblWeight.AutoSize = true;
            this.lblWeightValue.Location = new Point(boxX, y);
            this.lblWeightValue.Width = w;
            this.lblWeightValue.Font = valueFont;
            y += gap;
            //pitch
            this.lblPitch.Text = "Pitch:";
            this.lblPitch.Location = new System.Drawing.Point(labelX, y);
            this.lblPitch.AutoSize = true;
            this.lblPitchValue.Location = new Point(boxX, y);
            this.lblPitchValue.Width = w;
            this.lblPitchValue.Font = valueFont;
            y += gap;
            //bank
            this.lblBank.Text = "Bank:";
            this.lblBank.Location = new System.Drawing.Point(labelX, y);
            this.lblBank.AutoSize = true;
            this.lblBankValue.Location = new Point(boxX, y);
            this.lblBankValue.Width = w;
            this.lblBankValue.Font = valueFont;
            y += gap;

            //StartSim button
            this.btnStart.Text = "Start Real-Time";
            this.btnStart.Location = new System.Drawing.Point(20, y + 20);
            this.btnStart.Width = 130;
            this.btnStart.Height = 50;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            //StopSim button
            this.btnStop.Text = "Stop Real-Time";
            this.btnStop.Location = new System.Drawing.Point(170, y + 20);
            this.btnStop.Width = 130;
            this.btnStop.Height = 50;
            this.btnStop.Enabled = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

            //control layout of the telemetry panel
            this.panelLeft.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.lblTail, this.lblTailValue,
                this.lblTimestamp, this.lblTimestampValue,
                this.lblAltitude, this.lblAltitudeValue,
                this.lblWeight, this.lblWeightValue,
                this.lblPitch, this.lblPitchValue,
                this.lblBank, this.lblBankValue,
                this.btnStart, this.btnStop
            });

            // DEBUG LOG WINDOW
            //this.txtDebug = new System.Windows.Forms.TextBox();
            //this.txtDebug.Multiline = true;
            //this.txtDebug.ScrollBars = ScrollBars.Vertical;
            //this.txtDebug.Dock = DockStyle.Bottom;
            //this.txtDebug.Height = 120;
            //this.txtDebug.Font = new Font("Consolas", 9F, FontStyle.Regular);
            //this.txtDebug.BackColor = Color.Black;
            //this.txtDebug.ForeColor = Color.Lime;
            //this.txtDebug.ReadOnly = true;

            // Add debug box to monitoring tab
            this.tabMonitoring.Controls.Add(this.txtDebug);


            // ==========================
            // NEW STABLE CHART LAYOUT
            // ==========================

            // Parent chart panel
            this.panelCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCharts.Padding = new System.Windows.Forms.Padding(10);

            // Top panel (G-Force) - fixed height
            this.panelG.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelG.Height = 350; // fixed height prevents 0px errors
            this.panelG.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);

            // Create chartGforce
            this.chartGforce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartGforce.MinimumSize = new System.Drawing.Size(100, 100); // safety

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

            this.panelG.Controls.Add(this.chartGforce);

            // Bottom panel (Altitude) - fill
            this.panelAlt.Dock = System.Windows.Forms.DockStyle.Fill;

            this.chartAltitude.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartAltitude.MinimumSize = new System.Drawing.Size(100, 100); // safety

            var altArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea("AltArea");
            this.chartAltitude.ChartAreas.Add(altArea);

            var sAlt = new System.Windows.Forms.DataVisualization.Charting.Series("Altitude");
            sAlt.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            this.chartAltitude.Series.Add(sAlt);

            this.panelAlt.Controls.Add(this.chartAltitude);

            // Add both chart panels to parent chart panel
            this.panelCharts.Controls.Add(this.panelAlt);
            this.panelCharts.Controls.Add(this.panelG);

            // Add chart panel to monitoring tab
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
            //parameter dropdown items
            this.cmbParameter.Items.Add("All");
            this.cmbParameter.Items.Add("Altitude");
            this.cmbParameter.Items.Add("G-Force");
            this.cmbParameter.SelectedIndex = 0;
            //search start date
            this.lblStart.Text = "Start Date:";
            this.lblStart.Location = new System.Drawing.Point(sx, sy + 45);
            this.lblStart.AutoSize = true;
            this.dtStart.Location = new System.Drawing.Point(sx + 80, sy + 40);
            this.dtStart.Width = 150;
            //search end date
            this.lblEnd.Text = "End Date:";
            this.lblEnd.Location = new System.Drawing.Point(sx + 240, sy + 45);
            this.lblEnd.AutoSize = true;
            this.dtEnd.Location = new System.Drawing.Point(sx + 315, sy + 40);
            this.dtEnd.Width = 150;

            //search button
            this.btnSearch.Text = "Search";
            this.btnSearch.Location = new System.Drawing.Point(sx + 500, sy + 20);
            this.btnSearch.Height = 50;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            //export button
            this.btnExport.Text = "Export";
            this.btnExport.Location = new System.Drawing.Point(20, 550);
            this.btnExport.Height = 50;
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

            //added the data grids
            this.dgvG.Location = new System.Drawing.Point(20, 120);
            this.dgvG.Size = new System.Drawing.Size(1120, 180);
            this.dgvG.ReadOnly = true;
            this.dgvG.AllowUserToAddRows = false;

            this.dgvG.Columns.Add("TS", "Timestamp");
            this.dgvG.Columns.Add("X", "Accel X");
            this.dgvG.Columns.Add("Y", "Accel Y");
            this.dgvG.Columns.Add("Z", "Accel Z");
            this.dgvG.Columns.Add("W", "Weight");
            this.dgvG.Columns.Add("Stored", "StoredAt");

            this.dgvAlt.Location = new System.Drawing.Point(20, 330);
            this.dgvAlt.Size = new System.Drawing.Size(1120, 200);
            this.dgvAlt.ReadOnly = true;
            this.dgvAlt.AllowUserToAddRows = false;

            this.dgvAlt.Columns.Add("TS", "Timestamp");
            this.dgvAlt.Columns.Add("ALT", "Altitude");
            this.dgvAlt.Columns.Add("P", "Pitch");
            this.dgvAlt.Columns.Add("B", "Bank");
            this.dgvAlt.Columns.Add("W", "Weight");
            this.dgvAlt.Columns.Add("Stored", "StoredAt");

            this.Controls.Add(this.tabControl);
            this.ResumeLayout(false);

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
        //telemetry data labels values
        private System.Windows.Forms.Label lblTailValue;
        private System.Windows.Forms.Label lblTimestampValue;
        private System.Windows.Forms.Label lblAltitudeValue;
        private System.Windows.Forms.Label lblWeightValue;
        private System.Windows.Forms.Label lblPitchValue;
        private System.Windows.Forms.Label lblBankValue;
        //buttons
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        //chart panels
        private System.Windows.Forms.Panel panelCharts;
        private System.Windows.Forms.Panel panelG;
        private System.Windows.Forms.Panel panelAlt;
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
        //debug
        private System.Windows.Forms.TextBox txtDebug;

    }//end partial class
}//end namespace
