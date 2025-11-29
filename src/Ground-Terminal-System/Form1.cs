
/*
 * Filename		: Form1.cs
 * Project		: Ground-Terminal-System
 * By			: Erin Garcia
 * Date 		: 2025-11-28
 * Description	: This is the main form for the Ground Terminal System application.
 */

using GroundTerminalSystem.Models;
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
        private SearchController _searchController;
        private DateTime _lastUIUpdate = DateTime.MinValue;


        public Form1()
        {
            InitializeComponent();
            UpdateRealTimeStatus();

            _db = new DatabaseManager(ConfigurationManager.ConnectionStrings["FDMS_DB"].ConnectionString);
            _searchController = new SearchController(ConfigurationManager.ConnectionStrings["FDMS_DB"].ConnectionString);
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


        /* Function		: void toggleRealTime()
         * Description	: This function toggles the real-time data display on or off based on the checkbox state.
         * Parameter	: object sender, EventArgs e
         * Returns		: NONE
         */
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


        /* Function		: void btnStart_Click()
         * Description	: This function starts the network listener when the Start button is clicked.
         * Parameter	: object sender, EventArgs e
         * Returns		: NONE
         */
        private void btnStart_Click(object sender, EventArgs e)
        {
            _listener = new NetworkListener();
            _listener.InitializePort("127.0.0.1", 5000);
            _listener.PacketReceived += OnPacketReceived;
            _listener.Start();

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            Log("Listening started...");
        }//end btnStart_Click()


        /* Function		: void btnStop_Click()
         * Description	: This function stops the network listener when the Stop button is clicked.
         * Parameter	: object sender, EventArgs e
         * Returns		: NONE
         */
        private void btnStop_Click(object sender, EventArgs e)
        {
            _listener?.SendDisconnect();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            Log("Listener stopped.");
        }//end btnStop_Click()



        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchCriteria? parameters = UpdateSearchParameters();
            if (parameters == null)
            {
                return;
            }

            List<TelemetryData> newData = _searchController.ExecuteSearch(parameters);
            UpdateSearchResults(newData);
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            SearchCriteria? parameters = UpdateSearchParameters();
            if (parameters == null)
            {
                return;
            }

            _searchController.ExportToFile(parameters);
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

                    Log("Valid packet processed");
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


        private SearchCriteria? UpdateSearchParameters()
        {
            if (this.txtSearchTail.Text == String.Empty ||
                this.dtStart.Text == String.Empty ||
                this.dtEnd.Text == String.Empty)
            {
                return null;
            }

            SearchCriteria search = new SearchCriteria(txtSearchTail.Text, dtStart.Value, dtEnd.Value);

            return search;
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

        private void UpdateSearchResults(List<TelemetryData> data)
        {
            dgvG.ClearSelection();
            dgvAlt.ClearSelection();
            foreach (TelemetryData t in data)
            {
                dgvG.Rows.Add(t.TimeOfRecording,
                    t.AccelX, t.AccelY, t.AccelZ,
                    t.Weight, t.TimeReceived);
                dgvAlt.Rows.Add(t.TimeOfRecording,
                    t.Altitude, t.Pitch, t.Bank,
                    t.Weight, t.TimeReceived);
            }

            dgvG.Refresh();
            dgvAlt.Refresh();
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

    }//end class
}//end namespace
