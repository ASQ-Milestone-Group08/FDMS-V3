namespace AircraftTransmissionSystem.Telemetry
{
    /// <summary>
    /// Represents parsed telemetry data from an aircraft.
    /// Contains all telemetry fields extracted from a single line of telemetry data.
    /// </summary>
    public class TelemetryData
    {
        /// <summary>
        /// Timestamp of the telemetry reading.
        /// Format: M_d_yyyy HH:mm:s (e.g., "7_8_2018 19:34:3")
        /// </summary>
        public required string Timestamp { get; set; }

        /// <summary>
        /// Acceleration in the X-axis (g-force).
        /// </summary>
        public double AccelX { get; set; }

        /// <summary>
        /// Acceleration in the Y-axis (g-force).
        /// </summary>
        public double AccelY { get; set; }

        /// <summary>
        /// Acceleration in the Z-axis (g-force).
        /// </summary>
        public double AccelZ { get; set; }

        /// <summary>
        /// Aircraft weight (pounds or kilograms).
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Aircraft altitude (feet).
        /// </summary>
        public double Altitude { get; set; }

        /// <summary>
        /// Aircraft pitch angle (degrees).
        /// </summary>
        public double Pitch { get; set; }

        /// <summary>
        /// Aircraft bank angle (degrees).
        /// </summary>
        public double Bank { get; set; }
    }
}
