using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftTransmissionSystem
{
    public interface ICheckSumCalculator
    {
        public int Calculate(string telemetryString);
    }
}
