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
            InitializeDatabase();
            UpdateRealTimeStatus();
        }


    }//END class
}//END namespace
