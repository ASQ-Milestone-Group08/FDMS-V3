namespace AircraftTransmissionSystem.Packet
{
    /// <summary>
    /// Interface for calculating checksum from telemetry data.
    /// </summary>
    public interface ICheckSumCalculator
    {
        /// <summary>
        /// Calculates a numeric value (checksum) based on the provided telemetry data string.
        /// </summary>
        /// <param name="aircraftTelemetry">The telemetry data to be processed.</param>
        /// <returns>An integer representing the calculated result from the telemetry data.</returns>
        public int Calculate(string aircraftTelemetry);
    }
}
