using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        private readonly string connectionString =
            "Server=fdms-server.database.windows.net; Database=FDMS_DB; User Id=FDMS-Admin; Password=VeryStrongPW##++;";

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

        private void InitializeDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection failed:\n" + ex.Message);
            }
        }


        private void InsertTelemetry(Telemetry t)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Insert into AltitudeData table
                string sqlAlt = @"
INSERT INTO AltitudeData
    (AircraftTailNumber, TimeOfRecording, TimeReceived, Altitude, Pitch, Bank)
VALUES
    (@tail, @tRec, @tRecv, @alt, @pitch, @bank);";

                using (SqlCommand cmd = new SqlCommand(sqlAlt, conn))
                {
                    cmd.Parameters.AddWithValue("@tail", t.Tail);
                    cmd.Parameters.AddWithValue("@tRec", t.Time);
                    cmd.Parameters.AddWithValue("@tRecv", DateTime.Now);
                    cmd.Parameters.AddWithValue("@alt", t.Altitude);
                    cmd.Parameters.AddWithValue("@pitch", t.Pitch);
                    cmd.Parameters.AddWithValue("@bank", t.Bank);
                    cmd.ExecuteNonQuery();
                }

                // Insert into GForceData table
                string sqlG = @"
                INSERT INTO GForceData
                    (AircraftTailNumber, TimeOfRecording, TimeReceived, AccelerationX, AccelerationY, AccelerationZ, Weight)
                VALUES
                    (@tail, @tRec, @tRecv, @x, @y, @z, @w);";

                using (SqlCommand cmd2 = new SqlCommand(sqlG, conn))
                {
                    cmd2.Parameters.AddWithValue("@tail", t.Tail);
                    cmd2.Parameters.AddWithValue("@tRec", t.Time);
                    cmd2.Parameters.AddWithValue("@tRecv", DateTime.Now);
                    cmd2.Parameters.AddWithValue("@x", t.AccX);
                    cmd2.Parameters.AddWithValue("@y", t.AccY);
                    cmd2.Parameters.AddWithValue("@z", t.AccZ);
                    cmd2.Parameters.AddWithValue("@w", t.Weight);
                    cmd2.ExecuteNonQuery();
                }
            }
        }//end InsertTelemetry



        private void btnSearch_Click(object sender, EventArgs e)
        {
            string tail = txtSearchTail.Text.Trim();
            DateTime start = dtStart.Value.Date;
            DateTime end = dtEnd.Value.Date.AddDays(1).AddSeconds(-1);

            if (tail == "")
            {
                MessageBox.Show("Please enter a tail number.");
                return;
            }

            dgvG.Rows.Clear();
            dgvAlt.Rows.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //altitude search
                string sqlAlt = @"
                SELECT TimeOfRecording, Altitude, Pitch, Bank, 0 AS Weight, TimeReceived
                FROM AltitudeData
                WHERE AircraftTailNumber = @tail
                AND TimeOfRecording BETWEEN @start AND @end
                ORDER BY TimeOfRecording ASC;";

                using (SqlCommand cmdAlt = new SqlCommand(sqlAlt, conn))
                {
                    cmdAlt.Parameters.AddWithValue("@tail", tail);
                    cmdAlt.Parameters.AddWithValue("@start", start);
                    cmdAlt.Parameters.AddWithValue("@end", end);

                    using (SqlDataReader r = cmdAlt.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            dgvAlt.Rows.Add(
                                r["TimeOfRecording"],
                                r["Altitude"],
                                r["Pitch"],
                                r["Bank"],
                                r["Weight"],    // always 0 but OK for format
                                r["TimeReceived"]
                            );
                        }
                    }
                }

                //g-force search
                string sqlG = @"
                SELECT TimeOfRecording, AccelerationX, AccelerationY, AccelerationZ, Weight, TimeReceived
                FROM GForceData
                WHERE AircraftTailNumber = @tail
                AND TimeOfRecording BETWEEN @start AND @end
                ORDER BY TimeOfRecording ASC;";

                using (SqlCommand cmdG = new SqlCommand(sqlG, conn))
                {
                    cmdG.Parameters.AddWithValue("@tail", tail);
                    cmdG.Parameters.AddWithValue("@start", start);
                    cmdG.Parameters.AddWithValue("@end", end);

                    using (SqlDataReader r = cmdG.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            dgvG.Rows.Add(
                                r["TimeOfRecording"],
                                r["AccelerationX"],
                                r["AccelerationY"],
                                r["AccelerationZ"],
                                r["Weight"],
                                r["TimeReceived"]
                            );
                        }
                    }
                }
            }//end using SqlConnection
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
