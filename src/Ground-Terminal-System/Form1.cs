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

        private void Form1_Load(object sender, EventArgs e)
        {
            if (panelLeft.Width < 50)
            {
                panelLeft.Width = 350;
            }
            // Avoid 0-height errors
            int h = this.ClientSize.Height - this.panelTop.Height;

            chartGforce.Height = (int)(h * 0.60);
            chartAltitude.Height = (int)(h * 0.40);
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

        private void ProcessTelemetry(Telemetry t, int checksumFromPacket)
        {
            int localCs = ComputeChecksum(t);
            if (localCs != checksumFromPacket)
            {
                return;
            }

            InsertTelemetry(t);

            if (toggleRT.Checked)
            {
                this.Invoke(new Action(() => UpdateRealTimeUI(t)));
            }
        }//end ProcessTelemetry


        private void UpdateRealTimeUI(Telemetry t)
        {
            //textboxes
            txtTail.Text = t.Tail;
            txtTimestamp.Text = t.Time.ToString("HH:mm:ss");
            txtAltitude.Text = t.Altitude.ToString("F0");
            txtWeight.Text = t.Weight.ToString("F2");
            txtPitch.Text = t.Pitch.ToString("F4");
            txtBank.Text = t.Bank.ToString("F4");

            //charts
            //altitude chart limit to last 40 points
            chartAltitude.Series[0].Points.AddY(t.Altitude);
            if (chartAltitude.Series[0].Points.Count > 40)
            {
                chartAltitude.Series[0].Points.RemoveAt(0);
            }

            chartGforce.Series["Nx"].Points.AddY(t.AccX);
            chartGforce.Series["Ny"].Points.AddY(t.AccY);
            chartGforce.Series["Nz"].Points.AddY(t.AccZ);

            //g-force chart limit to last 40 points
            if (chartGforce.Series["Nx"].Points.Count > 40)
            {
                chartGforce.Series["Nx"].Points.RemoveAt(0);
                chartGforce.Series["Ny"].Points.RemoveAt(0);
                chartGforce.Series["Nz"].Points.RemoveAt(0);
            }
        }//end UpdateRealTimeUI


        private void InsertTelemetry(Telemetry t)
        {
            using var con = new SqliteConnection($"Data Source={dbPath}");
            con.Open();

            string q = @"
            INSERT INTO Telemetry
            (Tail, TS, AccX, AccY, AccZ, Weight, Alt, Pitch, Bank, Stored)
            VALUES ($tail, $ts, $x, $y, $z, $w, $a, $p, $b, $stored);";

            using var cmd = new SqliteCommand(q, con);
            cmd.Parameters.AddWithValue("$tail", t.Tail);
            cmd.Parameters.AddWithValue("$ts", t.Time.ToString("o"));
            cmd.Parameters.AddWithValue("$x", t.AccX);
            cmd.Parameters.AddWithValue("$y", t.AccY);
            cmd.Parameters.AddWithValue("$z", t.AccZ);
            cmd.Parameters.AddWithValue("$w", t.Weight);
            cmd.Parameters.AddWithValue("$a", t.Altitude);
            cmd.Parameters.AddWithValue("$p", t.Pitch);
            cmd.Parameters.AddWithValue("$b", t.Bank);
            cmd.Parameters.AddWithValue("$stored", DateTime.Now.ToString("o"));

            cmd.ExecuteNonQuery();
        }//end InsertTelemetry


        private void btnSearch_Click(object sender, EventArgs e)
        {
            var tail = txtSearchTail.Text.Trim();
            DateTime s = dtStart.Value.Date;
            DateTime end = dtEnd.Value.Date.AddDays(1).AddSeconds(-1);

            using var con = new SqliteConnection($"Data Source={dbPath}");
            con.Open();

            string q = @"
            SELECT * FROM Telemetry
            WHERE Tail=$t AND Stored BETWEEN $s AND $e
            ORDER BY Stored;";

            using var cmd = new SqliteCommand(q, con);
            cmd.Parameters.AddWithValue("$t", tail);
            cmd.Parameters.AddWithValue("$s", s.ToString("o"));
            cmd.Parameters.AddWithValue("$e", end.ToString("o"));

            using var r = cmd.ExecuteReader();

            dgvG.Rows.Clear();
            dgvAlt.Rows.Clear();

            while (r.Read())
            {
                dgvG.Rows.Add(
                    r["TS"], r["AccX"], r["AccY"], r["AccZ"], r["Weight"], r["Stored"]
                );

                dgvAlt.Rows.Add(
                    r["TS"], r["Alt"], r["Pitch"], r["Bank"], r["Weight"], r["Stored"]
                );
            }
        }//end btnSearch_Click


        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvAlt.Rows.Count == 0)
            {
                MessageBox.Show("No results to export.");
                return;
            }

            SaveFileDialog s = new SaveFileDialog
            {
                Filter = "Text File|*.txt"
            };
            if (s.ShowDialog() != DialogResult.OK)
                return;

            StringBuilder sb = new StringBuilder();

            foreach (DataGridViewRow row in dgvAlt.Rows)
            {
                sb.AppendLine(string.Join(",",
                    row.Cells[0].Value,
                    row.Cells[1].Value,
                    row.Cells[2].Value,
                    row.Cells[3].Value,
                    row.Cells[4].Value,
                    row.Cells[5].Value));
            }

            File.WriteAllText(s.FileName, sb.ToString());
            MessageBox.Show("Export complete.");
        }//end btnExport_Click


    }//END class
}//END namespace
