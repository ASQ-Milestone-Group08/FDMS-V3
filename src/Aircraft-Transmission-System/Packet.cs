using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftTransmissionSystem
{
    public class Packet
    {
        // Header
        public required string AircraftTailNumber { get; set; }
        public uint PacketSequenceNumber { get; set; }

        // Body
        public required string AircraftData { get; set; }

        // Trailer
        public int Checksum { get; set; }
    }
}
