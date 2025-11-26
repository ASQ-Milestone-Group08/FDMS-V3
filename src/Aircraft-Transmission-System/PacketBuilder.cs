using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftTransmissionSystem
{
    public class PacketBuilder(string aircraftTailNumber) : IPacketBuilder
    {
        // Fields
        private readonly string aircraftTailNumber = aircraftTailNumber;
        private uint packetSequenceCounter = 0;

        // Methods
        public Packet Build(string aircraftData)
        {
            if (string.IsNullOrEmpty(aircraftData))
            {
                throw new ArgumentException("Aircraft telemetry data cannot be empty.");
            }

            // 1. Calculate checksum
            int checksum = 0;

            // 2. Build the packet
            var packet = new Packet
            {
                AircraftTailNumber = aircraftTailNumber,
                PacketSequenceNumber = packetSequenceCounter,
                AircraftData = aircraftData,
                Checksum = checksum
            };

            // 3. Increment sequence for the next packet
            packetSequenceCounter++;

            return packet;
        }
    }
}
