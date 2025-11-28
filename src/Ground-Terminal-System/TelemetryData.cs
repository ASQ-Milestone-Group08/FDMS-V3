namespace GroundTerminalSystem
{
    public class TelemetryData
    {
        public string Timestamp { get; set; }
        public double AccelX { get; set; }
        public double AccelY { get; set; }
        public double AccelZ { get; set; }
        public double Weight { get; set; }
        public double Altitude { get; set; }
        public double Pitch { get; set; }
        public double Bank { get; set; }
        public int Checksum { get; set; }
    }
}
