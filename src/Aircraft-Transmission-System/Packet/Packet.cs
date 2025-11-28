/*
 * File Name    : Packet.cs
 * Description  : This is the form of data packet containing 
 *               all necessary information for communication with Ground Terminal.
 * Author       : Hassan Alqhwaizi
 * Last Modified: November 28, 2025
 */
namespace AircraftTransmissionSystem.Packet
{
    /// <summary>
    /// Represents a data packet containing aircraft telemetry information.
    /// </summary>
    public class Packet
    {
        // Header
        public required string AircraftTailNumber { get; set; }
        public uint PacketSequenceNumber { get; set; }

        // Body
        public required string AircraftTelemetry { get; set; }

        // Trailer
        public int Checksum { get; set; }
    }
}
