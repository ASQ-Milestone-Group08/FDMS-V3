namespace GroundTerminalSystem
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        ///
        //variables
        private System.ComponentModel.IContainer components = null;
        //tab variables
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabRealTime;
        private System.Windows.Forms.TabPage tabSearch;
        //panel variables
        private System.Windows.Forms.Panel panelLeftInfo;
        private System.Windows.Forms.Panel panelTelemetry;
        //data variables
        private System.Windows.Forms.Label lblTailNumber;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.Label lblRealTimeStatus;
        private System.Windows.Forms.Label lblPackets;
        //chart variables
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAltitude;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGForce;


        ///// <summary>
        /////  Clean up any resources being used.
        ///// </summary>
        ///// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabRealTime = new System.Windows.Forms.TabPage();
            this.tabSearch = new System.Windows.Forms.TabPage();

            this.chkRealTime = new System.Windows.Forms.CheckBox();

            this.panelLeftInfo = new System.Windows.Forms.Panel();
            this.panelTelemetry = new System.Windows.Forms.Panel();

            this.lblTailNumber = new System.Windows.Forms.Label();
            this.lblConnection = new System.Windows.Forms.Label();
            this.lblRealTimeStatus = new System.Windows.Forms.Label();
            this.lblPackets = new System.Windows.Forms.Label();

            this.chartAltitude = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartGForce = new System.Windows.Forms.DataVisualization.Charting.Chart();
        }

        #endregion
    }
}
