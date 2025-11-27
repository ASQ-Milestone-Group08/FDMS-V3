namespace GroundTerminalSystem
{
    public class ChecksumValidator
    {

        public bool Validate(float altitude, float pitch, float bank, int receivedChecksum)
        {
            // Checksum = (ALT + Pitch + Bank) / 3
            float calculation = (altitude + pitch + bank) / 3;

            int calculatedChecksum = (int)calculation;

            return calculatedChecksum == receivedChecksum;
        }
    }
}
