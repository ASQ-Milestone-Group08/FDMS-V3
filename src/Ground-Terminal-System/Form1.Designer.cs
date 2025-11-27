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

            //tab controls
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMonitoring = new System.Windows.Forms.TabPage();
            this.tabSearch = new System.Windows.Forms.TabPage();

            //real-time toggle control
            this.panelTop = new System.Windows.Forms.Panel();
            this.toggleRealTime = new System.Windows.Forms.CheckBox();
            this.lblRealTimeStatus = new System.Windows.Forms.Label();

            //telemetry data
            this.panelLefft = new System.Windows.Forms.Panel();
            //tail #
            this lblTail = new System.Windows.Forms.Label();
            this.txtTail = new System.Windows.Forms.TextBox();
            //timestamp
            this lblTimestamp = new System.Windows.Forms.Label();
            this.txtTimestamp = new System.Windows.Forms.TextBox();
            //altitude
            this lblAltitiude = new System.Windows.Forms.Label();
            this.txtAltitiude = new System.Windows.Forms.TextBox();
            //weight
            this lblWeight = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            //pitch
            this lblPitch = new System.Windows.Forms.Label();
            this.txtPitch = new System.Windows.Forms.TextBox();

            //tab control
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Controls.Add(this.tabMonitoring);
            this.tabControl.Controls.Add(this.tabSearch);

        }

        #endregion
    }
}
