namespace AircraftTransmissionSystem.Packet
{
    public class ChecksumCalculator : ICheckSumCalculator
    {
        /// <summary>
        /// Calculates the checksum value based on the telemetry data provided.
        /// </summary>
        /// <param name="aircraftTelemetry">A comma-separated string containing telemetry data in the format: Timestamp, Accel-X, Accel-Y, Accel-Z,
        /// Weight, Altitude, Pitch, Bank. The Altitude, Pitch, and Bank values must be valid numeric values.</param>
        /// <returns>The integer result of averaging the Altitude, Pitch, and Bank (rounded down).</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="aircraftTelemetry"/> is not in the expected format or contains invalid numeric
        /// values for Altitude, Pitch, or Bank.</exception>
        public int Calculate(string aircraftTelemetry)
        {
            // Extract values from telemetry string
            string[] parts = aircraftTelemetry.Split(',');

            // Telemetry Format: Timestamp, Accel-X, Accel-Y, Accel-Z, Weight, Altitude, Pitch, Bank
            if (parts.Length != 8)
            {
                throw new ArgumentException("Telemetry string is not in the expected format.");
            }

            // The last 3 values are used for checksum calculation
            // Formula: (Altitude + Pitch + Bank) / 3
            // Note: Values are doubles in the telemetry file, need to parse as double then convert to int
            if (!double.TryParse(parts[5].Trim(), out double altitude) ||
                !double.TryParse(parts[6].Trim(), out double pitch) ||
                !double.TryParse(parts[7].Trim(), out double bank))
            {
                throw new ArgumentException("Telemetry string contains invalid numeric values.");
            }

            // Calculate average and convert to int (truncate decimal part)
            int checksum = (int)((altitude + pitch + bank) / 3);

            return checksum;
        }
    }
}
