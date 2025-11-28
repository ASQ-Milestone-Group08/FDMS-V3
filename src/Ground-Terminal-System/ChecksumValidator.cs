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
