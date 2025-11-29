/*
 * File Name    : PacketParser.cs
 * Description  : This is the class for parsing incoming data packets from aircraft.
 * Author       : Andrei Haboc
 * Last Modified: November 28, 2025
 */
using System;

namespace GroundTerminalSystem
{
    public class PacketParser
    {
        private readonly ChecksumValidator _validator = new ChecksumValidator();
        public int LastExpectedChecksum { get; private set; }
        public int LastCalculatedChecksum { get; private set; }

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
                int sequence = int.Parse(segments[1]);
                string body = segments[2];
                int receivedChecksum = int.Parse(segments[3]);

                var values = body.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                if (values.Length < 8)
                {
                    System.Diagnostics.Debug.WriteLine($"Packet body incorrect: expected 8 fields, got {values.Length}");
                    return false;
                }



                // Extract values in groups
                var gForce = ExtractGForce(values);
                var attitude = ExtractAttitude(values);


                // Build telemetry data object
                result = new TelemetryData
                {
                    TailNumber = tailNumber,
                    Sequence = sequence,
                    TimeOfRecording = DateTime.ParseExact(values[0], "M_d_yyyy H:mm:s", null)
,
                    TimeReceived = DateTime.Now,

                    AccelX = gForce.AccelX,
                    AccelY = gForce.AccelY,
                    AccelZ = gForce.AccelZ,
                    Weight = gForce.Weight,

                    Altitude = attitude.Altitude,
                    Pitch = attitude.Pitch,
                    Bank = attitude.Bank,

                    Checksum = receivedChecksum
                };

                // Validate checksum according to APPENDIX C
                bool valid = _validator.Validate(result.Altitude, result.Pitch, result.Bank, receivedChecksum);

                if (!valid)
                {
                    System.Diagnostics.Debug.WriteLine("Invalid checksum detected.");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"PacketParser Exception: {e.Message}");
                return false;
            }
        }

        // Extract G-Force values
        private (double AccelX, double AccelY, double AccelZ, double Weight) ExtractGForce(string[] values)
        {
            return (
                AccelX: double.Parse(values[1]),
                AccelY: double.Parse(values[2]),
                AccelZ: double.Parse(values[3]),
                Weight: double.Parse(values[4])
            );
        }

        // Extract Attitude values
        private (double Altitude, double Pitch, double Bank) ExtractAttitude(string[] values)
        {
            return (
                Altitude: double.Parse(values[5]),
                Pitch: double.Parse(values[6]),
                Bank: double.Parse(values[7])
            );
        }
    }
}
