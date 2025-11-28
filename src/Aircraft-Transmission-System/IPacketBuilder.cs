namespace AircraftTransmissionSystem
{
    /// <summary>
    /// Interface for building data packets (<see cref="Packet"/>) from aircraft telemetry data.
    /// </summary>
    public interface IPacketBuilder
    {
        /// <summary>
        /// Builds a <see cref="Packet"/> object using the provided aircraft telemetry data.
        /// </summary>
        /// <param name="aircraftTelemetry">A string containing the aircraft telemetry data.</param>
        /// <returns>A <see cref="Packet"/> object constructed from the provided telemetry data.</returns>
        public Packet Build(string aircraftTelemetry);
    }
}
