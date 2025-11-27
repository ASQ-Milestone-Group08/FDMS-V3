using System;
using System.Collections.Generic;
using System.Text;

namespace AircraftTransmissionSystem
{
    public class ChecksumCalculator : ICheckSumCalculator
    {
        public int Calculate(string telemetryString)
        {
            // Extract values from telemetry string
            string[] parts = telemetryString.Split(',');

            // Telemetry Format: Timestamp, Accel-X, Accel-Y, Accel-Z, Weight, Altitude, Pitch, Bank 
            if (parts.Length != 8)
            {
                throw new ArgumentException("Telemetry string is not in the expected format.");
            }

            // The last 3 values are used for checksum calculation
            // Formula: (Altitude + Pitch + Bank) / 3
            if (!int.TryParse(parts[5], out int altitude) ||
                !int.TryParse(parts[6], out int pitch) ||
                !int.TryParse(parts[7], out int bank))
            {
                throw new ArgumentException("Telemetry string contains invalid numeric values.");
            }

            int checksum = (altitude + pitch + bank) / 3;

            return checksum;
        }
    }
}
