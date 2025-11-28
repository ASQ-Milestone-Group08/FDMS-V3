using System;
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

            _db = new DatabaseManager("Server=(localdb)\\MSSQLLocalDB;Database=FDMS;Trusted_Connection=True;");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (panelLeft.Width < 50)
                panelLeft.Width = 350;

            int h = this.ClientSize.Height - this.panelTop.Height;
            chartGforce.Height = (int)(h * 0.60);
            chartAltitude.Height = (int)(h * 0.40);
        }

        //--------------------------------------
        // REAL-TIME UI TOGGLE
        //--------------------------------------
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

        //--------------------------------------
        // START/STOP LISTENER
        //--------------------------------------
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

        //--------------------------------------
        // RECEIVE PACKET CALLBACK
        //--------------------------------------
        private void OnPacketReceived(string packet)
        {
            // This method is called on a background thread from NetworkListener
            Log($"Received: {packet}");

            TelemetryData data;
            if (!_parser.TryParse(packet, out data))
            {
                // invalid: store in error table (on background task)
                var tail = packet.Split('|')[0];

                Task.Run(() =>
                {
                    try
                    {
                        _db.StoreInvalidPacket(packet, tail);
                    }
                    catch (Exception ex)
                    {
                        Log($"DB invalid insert error: {ex.Message}");
                    }
                });

                Log("Invalid checksum stored");
                return;
            }

            // Valid packet – store in DB on a background worker (so listener thread stays light)
            Task.Run(() =>
            {
                try
                {
                    _db.StoreAltitudeData(data);
                    _db.StoreGForceData(data);
                }
                catch (Exception ex)
                {
                    Log($"DB valid insert error: {ex.Message}");
                }
            });

            // (only every 300ms) 
            if (!this.IsHandleCreated || this.IsDisposed)
                return;

            var now = DateTime.Now;
            if (now - _lastUIUpdate > TimeSpan.FromMilliseconds(300))
            {
                _lastUIUpdate = now;

                try
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        try
                        {  
                            if (toggleRT.Checked)
                            {
                                UpdateRealTimeDisplay(data);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log($"UI update error: {ex.Message}");
                        }
                    }));
                }
                catch (ObjectDisposedException)
                {
                    // form closed while packets still arriving → just ignore
                }
            }

            Log("Valid packet processed");
        }




        //--------------------------------------
        // UPDATE LIVE UI DISPLAY
        //--------------------------------------
        private void UpdateRealTimeDisplay(TelemetryData t)
        {
            // INFO LABELS
            lblTailValue.Text = t.TailNumber;
            lblTimestampValue.Text = t.TimeOfRecording;
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
        }

        //--------------------------------------
        // LOGGING UTIL
        //--------------------------------------
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
