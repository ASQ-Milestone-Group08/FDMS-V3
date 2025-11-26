using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftTransmissionSystem
{
    public interface IPacketBuilder
    {
        public Packet Build(string aircraftData);
    }
}
