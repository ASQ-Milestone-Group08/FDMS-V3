namespace AircraftTransmissionSystem
{
    /// <summary>
    /// Interface for reading telemetry data from various sources.
    /// Implementations provide one line of telemetry data per call.
    /// </summary>
    public interface ITelemetrySource : IDisposable
    {
        /// <summary>
        /// Function Name: GetNextReading
        /// Description: Retrieves the next line of telemetry data from the source.
        ///              Each call returns one line containing comma-delimited telemetry values.
        ///              This method advances the internal reading position to the next line.
        /// Parameters: None
        /// Return Type: string? (nullable string)
        ///              - Returns a string in format: Timestamp, Accel-X, Accel-Y, Accel-Z, Weight, Altitude, Pitch, Bank
        ///              - Returns null when end of data is reached or no more data is available
        /// </summary>
        /// <returns>
        /// A comma-delimited string containing 8 telemetry fields, or null if no more data is available.
        /// </returns>
        public string? GetNextReading();

        /// <summary>
        /// Property Name: HasMoreData
        /// Description: Indicates whether more telemetry data is available to read.
        ///              This property should be checked before calling GetNextReading() to avoid null returns.
        /// Return Type: bool
        ///              - true: More data is available
        ///              - false: End of data reached or source is closed
        /// </summary>
        public bool HasMoreData { get; }
    }
}
