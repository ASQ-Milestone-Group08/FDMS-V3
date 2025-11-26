namespace AircraftTransmissionSystem
{
    /// <summary>
    /// Interface for reading telemetry data from various sources.
    /// Implementations provide one line of telemetry data per call.
    /// </summary>
    public interface ITelemetrySource : IDisposable
    {
        /// <summary>
        /// Retrieves the next line of telemetry data.
        /// </summary>
        /// <returns>
        /// A string containing telemetry data in the format:
        /// Timestamp, Accel-X, Accel-Y, Accel-Z, Weight, Altitude, Pitch, Bank
        /// Returns null when no more data is available.
        /// </returns>
        public string? GetNextReading();

        /// <summary>
        /// Gets a value indicating whether more telemetry data is available.
        /// </summary>
        public bool HasMoreData { get; }
    }
}
