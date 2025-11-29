using System;
using System.Configuration;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroundTerminalSystem
{
    public partial class Form1 : Form
    {
        private NetworkListener _listener;
        private PacketParser _parser = new PacketParser();
        private DatabaseManager _db;
        private DateTime _lastUIUpdate = DateTime.MinValue;


        public Form1()
        {
            InitializeComponent();
            UpdateRealTimeStatus();

            _db = new DatabaseManager(ConfigurationManager.ConnectionStrings["FDMS_DB"].ConnectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (chartGforce.Series.IndexOf("Nx") < 0)
                chartGforce.Series.Add("Nx");
            if (chartGforce.Series.IndexOf("Ny") < 0)
                chartGforce.Series.Add("Ny");
            if (chartGforce.Series.IndexOf("Nz") < 0)
                chartGforce.Series.Add("Nz");

            chartGforce.Series["Nx"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chartGforce.Series["Ny"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chartGforce.Series["Nz"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            if (chartAltitude.Series.IndexOf("Altitude") < 0)
                chartAltitude.Series.Add("Altitude");

            chartAltitude.Series["Altitude"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

        }


        private void toggleRealTime(object sender, EventArgs e)
        {
            UpdateRealTimeStatus();
        }

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
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _listener = new NetworkListener();
            _listener.InitializePort("127.0.0.1", 5000);
            _listener.PacketReceived += OnPacketReceived;
            _listener.Start();

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            Log("Listening started...");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _listener?.SendDisconnect();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            Log("Listener stopped.");
        }


        private void OnPacketReceived(string packet)
        {
            Log($"Received: {packet}");

            Task.Run(() =>
            {
                if (_parser.TryParse(packet, out TelemetryData data))
                {
                    try
                    {
                        // DB writes OFF UI / network thread
                        _db.StoreAltitudeData(data);
                        _db.StoreGForceData(data);
                    }
                    catch (Exception ex)
                    {
                        Log($"DB error: {ex.Message}");
                    }

                    // Throttle UI update and always use BeginInvoke
                    var now = DateTime.Now;
                    if (now - _lastUIUpdate > TimeSpan.FromMilliseconds(300))
                    {
                        _lastUIUpdate = now;

                        if (!IsHandleCreated || IsDisposed)
                            return;

                        try
                        {
                            BeginInvoke(new Action(() =>
                            {
                                try
                                {
                                    // ignoring it for now
                                    // if (toggleRT.Checked)
                                    UpdateRealTimeDisplay(data);
                                }
                                catch (Exception ex)
                                {
                                    Log($"UI update error: {ex.Message}");
                                }
                            }));
                        }
                        catch (ObjectDisposedException)
                        {
                            // form closed while packet still arriving
                        }
                    }

                    Log("✔ Valid packet processed");
                }
                else
                {
                    string tail = packet.Split('|')[0];
                    try
                    {
                        //ignoring for now
                        //_db.StoreInvalidPacket(packet, tail);
                    }
                    catch (Exception ex)
                    {
                        Log($"DB invalid insert error: {ex.Message}");
                    }

                    Log("Invalid checksum stored");
                }
            });
        }


        private void UpdateRealTimeDisplay(TelemetryData t)
        {
            // INFO LABELS
            lblTailValue.Text = t.TailNumber;
            lblTimestampValue.Text = t.TimeOfRecording.ToString("M_d_yyyy H:mm:s");
            lblAltitudeValue.Text = t.Altitude.ToString("F0");
            lblWeightValue.Text = t.Weight.ToString("F2");
            lblPitchValue.Text = t.Pitch.ToString("F4");
            lblBankValue.Text = t.Bank.ToString("F4");

            // make sure series exist 
            if (chartAltitude.Series.Count > 0)
            {
                chartAltitude.Series[0].Points.AddY(t.Altitude);
                if (chartAltitude.Series[0].Points.Count > 40)
                    chartAltitude.Series[0].Points.RemoveAt(0);
            }

            if (chartGforce.Series.IndexOf("Nx") >= 0)
            {
                chartGforce.Series["Nx"].Points.AddY(t.AccelX);
                chartGforce.Series["Ny"].Points.AddY(t.AccelY);
                chartGforce.Series["Nz"].Points.AddY(t.AccelZ);

                if (chartGforce.Series["Nx"].Points.Count > 40)
                {
                    chartGforce.Series["Nx"].Points.RemoveAt(0);
                    chartGforce.Series["Ny"].Points.RemoveAt(0);
                    chartGforce.Series["Nz"].Points.RemoveAt(0);
                }
            }
            chartAltitude.Invalidate();
            chartGforce.Invalidate();
        }

        private void Log(string message)
        {
            try
            {
                if (!txtDebug.IsHandleCreated || txtDebug.IsDisposed)
                    return;

                if (txtDebug.InvokeRequired)
                {
                    txtDebug.BeginInvoke(new Action(() => AppendLog(message)));
                }
                else
                {
                    AppendLog(message);
                }
            }
            catch
            {
                // ignore logging errors
            }
        }


        private void AppendLog(string message)
        {
            txtDebug.AppendText($"{DateTime.Now:HH:mm:ss}  {message}{Environment.NewLine}");
        }

    }
}
