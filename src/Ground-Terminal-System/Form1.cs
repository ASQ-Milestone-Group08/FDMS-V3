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
    //TELEMETRY CLASS
    public class Telemetry
    {
        public string Tail { get; set; }
        public DateTime Time { get; set; }
        public double AccX { get; set; }
        public double AccY { get; set; }
        public double AccZ { get; set; }
        public double Weight { get; set; }
        public double Altitude { get; set; }
        public double Pitch { get; set; }
        public double Bank { get; set; }
    }//end Telemetry class



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
        }//end btnStart_Click

        private void btnStop_Click(object sender, EventArgs e)
        {
            simSource?.Cancel();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }//end btnStop_Click

        private async void StartSim(CancellationToken token)
        {
            Random rnd = new Random();
            double alt = 5000;

            while (!token.IsCancellationRequested)
            {
                var pkt = new Telemetry
                {
                    Tail = "AC123",
                    Time = DateTime.Now,
                    AccX = rnd.NextDouble(),
                    AccY = rnd.NextDouble(),
                    AccZ = rnd.NextDouble(),
                    Weight = 2150 + rnd.NextDouble(),
                    Altitude = alt,
                    Pitch = Math.Round((rnd.NextDouble() - 0.5) * 0.05, 6),
                    Bank = Math.Round((rnd.NextDouble() - 0.5) * 0.02, 6)
                };

                alt += rnd.Next(200, 600);

                int checksum = ComputeChecksum(pkt);

                // Process packet
                ProcessTelemetry(pkt, checksum);

                await Task.Delay(1000);
            }

        }//end StartSim

        private int ComputeChecksum(Telemetry t)
        {
            return (int)((t.Altitude + t.Pitch + t.Bank) / 3.0);
        }//end ComputeChecksum

    }//END class

    
}//END namespace
