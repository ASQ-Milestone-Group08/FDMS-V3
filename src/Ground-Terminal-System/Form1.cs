using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;


namespace GroundTerminalSystem
{
    public partial class Form1 : Form
    {

        private CancellationTokenSource simSource;
        private readonly string dbPath = "FDMS_DB";

        public Form1()
        {
            InitializeComponent();
            UpdateRealTimeStatus();
        }

        //REAL-TIME 
        private void toggleRealTime(object sender, EventArgs e)
        {
            UpdateRealTimeStatus();
        }//end toggleRealTime

        private void UpdateRealTimeStatus()
        {
            if (toggleRT.Checked)
            {
                lblRealTimeStatus.Text = "ON";
                lblRealTimeStatus.ForeColor = Color.Green;
            }
            else
            {
                lblRealTimeStatus.Text = "OFF";
                lblRealTimeStatus.ForeColor = Color.Red;
            }
        }//end UpdateRealTimeStatus

        private void btnStart_Click(object sender, EventArgs e)
        {
            simSource = new CancellationTokenSource();
            StartSim(simSource.Token);

            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            simSource?.Cancel();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }


    }//END class
}//END namespace
