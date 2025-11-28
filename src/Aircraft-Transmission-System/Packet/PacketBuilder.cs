namespace AircraftTransmissionSystem.Packet
{
    /// <summary>
    /// Builds data packets (<see cref="Packet"/>) from aircraft telemetry data.
    /// </summary>
    /// <param name="aircraftTailNumber">The tail number of the aircraft to build packets for.</param>
    public class PacketBuilder(string aircraftTailNumber) : IPacketBuilder
    {
        // Fields
        private readonly ChecksumCalculator checksumCalculator = new ChecksumCalculator();
        private readonly string aircraftTailNumber = aircraftTailNumber;

        /// <summary>
        /// Builds a new telemetry packet to be sent to the Ground Terminal.
        /// </summary>
        /// <param name="aircraftTelemetry">The aircraft telemetry data to include in the packet.</param>
        /// <param name="sequenceNumber">The sequence number for this packet (managed by TransmissionController).</param>
        /// <returns>A Packet instance containing the aircraft data, aircraft tail number, sequence number, and checksum.</returns>
        /// <exception cref="ArgumentException">Thrown if aircraftTelemetry is null or empty.</exception>
        public Packet Build(string aircraftTelemetry, uint sequenceNumber)
        {
            if (string.IsNullOrEmpty(aircraftTelemetry))
            {
                throw new ArgumentException("Aircraft telemetry data cannot be empty.");
            }

            // 1. Calculate checksum
            int checksum = checksumCalculator.Calculate(aircraftTelemetry);

            // 2. Build the packet
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
