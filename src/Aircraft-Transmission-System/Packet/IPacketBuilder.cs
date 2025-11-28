/*
 * File Name    : IPacketBuilder.cs
 * Description  : This is the interface for building data packets from aircraft telemetry data. 
 * Author       : Hassan Alqhwaizi
 * Last Modified: November 28, 2025
 */
namespace AircraftTransmissionSystem.Packet
{
    /// <summary>
    /// Interface for building data packets (<see cref="Packet"/>) from aircraft telemetry data.
    /// </summary>
    public interface IPacketBuilder
    {
        /// <summary>
        /// Builds a <see cref="Packet"/> object using the provided aircraft telemetry data and sequence number.
        /// </summary>
        /// <param name="aircraftTelemetry">A string containing the aircraft telemetry data.</param>
        /// <param name="sequenceNumber">The sequence number for this packet.</param>
        /// <returns>A <see cref="Packet"/> object constructed from the provided telemetry data.</returns>
        public Packet Build(string aircraftTelemetry, uint sequenceNumber);
    }
}
