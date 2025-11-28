/*
 * File Name    : PacketBuilder.cs
 * Description  : This is the implementation of building data packets from aircraft telemetry data.
 *                It builds a Packet object using the telemetry string and sequence number.
 *                Checksum is calculated using ChecksumCalculator.
 * Author       : Hassan Alqhwaizi
 * Last Modified: November 28, 2025
 */
using AircraftTransmissionSystem.Telemetry;

namespace AircraftTransmissionSystem.Packet
{
    /// <summary>
    /// Builds data packets (<see cref="Packet"/>) from aircraft telemetry data.
    /// </summary>
    /// <param name="aircraftTailNumber">The tail number of the aircraft to build packets for.</param>
    public class PacketBuilder(string aircraftTailNumber) : IPacketBuilder
    {
        // Fields
        private readonly TelemetryParser telemetryParser = new TelemetryParser();
        private readonly ChecksumCalculator checksumCalculator = new ChecksumCalculator();
        private readonly string aircraftTailNumber = aircraftTailNumber;

        /// <summary>
        /// Builds a new telemetry packet to be sent to the Ground Terminal.
        /// </summary>
        /// <param name="aircraftTelemetry">The aircraft telemetry data to include in the packet.</param>
        /// <param name="sequenceNumber">The sequence number for this packet (managed by TransmissionController).</param>
        /// <returns>A Packet instance containing the aircraft data, aircraft tail number, sequence number, and checksum.</returns>
        /// <exception cref="ArgumentException">Thrown if aircraftTelemetry is null, empty, or invalid format.</exception>
        public Packet Build(string aircraftTelemetry, uint sequenceNumber)
        {
            if (string.IsNullOrEmpty(aircraftTelemetry))
            {
                throw new ArgumentException("Aircraft telemetry data cannot be empty.");
            }

            // 1. Parse telemetry string into structured data
            TelemetryData parsedData = telemetryParser.Parse(aircraftTelemetry);

            // 2. Calculate checksum using parsed values
            int checksum = checksumCalculator.Calculate(parsedData.Altitude, parsedData.Pitch, parsedData.Bank);

            // 3. Build the packet
            var packet = new Packet
            {
                AircraftTailNumber = aircraftTailNumber,
                PacketSequenceNumber = sequenceNumber,
                AircraftTelemetry = aircraftTelemetry,
                Checksum = checksum
            };

            return packet;
        }
    }
}
