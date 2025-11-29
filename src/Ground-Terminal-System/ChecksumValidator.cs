/*
 * File Name    : ChecksumValidator.cs
 * Description  : This is the class for validating checksums of incoming telemetry data packets.
 * Author       : Andrei Haboc
 * Last Modified: November 28, 2025
 */
namespace GroundTerminalSystem
{
    public class ChecksumValidator
    {

        public bool Validate(double altitude, double pitch, double bank, int receivedChecksum)
        {
            // Checksum = (ALT + Pitch + Bank) / 3
            double calculation = ((altitude + pitch + bank) / 3);

            int calculatedChecksum = (int)calculation;

            return calculatedChecksum == receivedChecksum;
        }
    }
}
