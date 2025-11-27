using System;

namespace GroundTerminalSystem
{
    public class PacketParser
    {
        private readonly ChecksumValidator _validator = new ChecksumValidator();

        public bool TryParse(string packet, out TelemetryData result)
        {
            result = null;

            try
            {
                // Tail|Sequence|Timestamp,AccelX,AccelY,AccelZ,Weight,Altitude,Pitch,Bank|Checksum

                var segments = packet.Split('|');

                if (segments.Length != 4)
                {
                    System.Diagnostics.Debug.WriteLine("Packet format incorrect: wrong number of segments");
                    return false;
                }

                string tailNumber = segments[0];
                string sequence = segments[1];
                string body = segments[2];
                int receivedChecksum = int.Parse(segments[3]);

                var values = body.Split(',');

                if (values.Length != 8)
                {
                    System.Diagnostics.Debug.WriteLine("Packet body incorrect: missing telemetry fields");
                    return false;
                }

                // Build telemetry data object
                var data = new TelemetryData
                {
                    Timestamp = values[0],
                    AccelX = float.Parse(values[1]),
                    AccelY = float.Parse(values[2]),
                    AccelZ = float.Parse(values[3]),
                    Weight = float.Parse(values[4]),
                    Altitude = float.Parse(values[5]),
                    Pitch = float.Parse(values[6]),
                    Bank = float.Parse(values[7]),
                    Checksum = receivedChecksum
                };

                // Validate checksum according to APPENDIX C
                bool valid = _validator.Validate(data.Altitude, data.Pitch, data.Bank, receivedChecksum);

                if (!valid)
                {
                    System.Diagnostics.Debug.WriteLine("Invalid checksum detected.");
                    return false;
                }

                result = data;
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"PacketParser Exception: {e.Message}");
                return false;
            }
        }
    }
}
