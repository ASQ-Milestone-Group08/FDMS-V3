namespace AircraftTransmissionSystem.Telemetry
{
    /// <summary>
    /// Parses raw telemetry strings into structured TelemetryData objects.
    /// Handles the comma-delimited format from telemetry files.
    /// </summary>
    public class TelemetryParser
    {
        /// <summary>
        /// Function Name: Parse
        /// Description: Parses a raw telemetry string into a TelemetryData object.
        ///              Handles trailing commas and whitespace in the input string.
        /// Parameters:
        ///   - telemetryString (string): Raw telemetry data in format:
        ///                               Timestamp, Accel-X, Accel-Y, Accel-Z, Weight, Altitude, Pitch, Bank
        /// Return Type: TelemetryData - Parsed telemetry data object
        /// Exceptions:
        ///   - ArgumentException: Thrown when the string format is invalid or contains non-numeric values
        /// </summary>
        /// <param name="telemetryString">The raw telemetry string to parse.</param>
        /// <returns>A TelemetryData object containing all parsed fields.</returns>
        /// <exception cref="ArgumentException">Thrown when the string format is invalid.</exception>
        public TelemetryData Parse(string telemetryString)
        {
            // Split by comma and remove empty entries (handles trailing comma)
            string[] parts = telemetryString.Split(',', StringSplitOptions.RemoveEmptyEntries);

            // Expected format: Timestamp, Accel-X, Accel-Y, Accel-Z, Weight, Altitude, Pitch, Bank (8 fields)
            if (parts.Length != 8)
            {
                throw new ArgumentException(
                    $"Telemetry string is not in the expected format. Expected 8 fields, got {parts.Length}. " +
                    $"Format: Timestamp, Accel-X, Accel-Y, Accel-Z, Weight, Altitude, Pitch, Bank"
                );
            }

            try
            {
                // Parse all fields
                var telemetryData = new TelemetryData
                {
                    Timestamp = parts[0].Trim(),
                    AccelX = double.Parse(parts[1].Trim()),
                    AccelY = double.Parse(parts[2].Trim()),
                    AccelZ = double.Parse(parts[3].Trim()),
                    Weight = double.Parse(parts[4].Trim()),
                    Altitude = double.Parse(parts[5].Trim()),
                    Pitch = double.Parse(parts[6].Trim()),
                    Bank = double.Parse(parts[7].Trim())
                };

                return telemetryData;
            }
            catch (FormatException ex)
            {
                throw new ArgumentException(
                    "Failed to parse telemetry values. One or more fields contain invalid numeric data.",
                    ex
                );
            }
        }
    }
}
