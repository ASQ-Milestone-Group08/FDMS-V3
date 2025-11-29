/*
 * FILENAME:        ErrorPacket.cs
 * ASSIGNMENT:      Advanced Software Quality - Final Project
 * DESCRIPTION:     Data model used when retreiving and displaying Packet Errors from either the
 *                  database or Aircraft Transmission data with an invalid packet.
 */

namespace GroundTerminalSystem.Models
{
    public class ErrorPacket
    {
        private int ErrorID { get; }
        private DateTime TimeReceived { get; }
        private string PacketData { get; }
        private int ExpectedChecksum { get; }
        private int CalculatedChecksum { get; }

        public ErrorPacket(int id, DateTime timeReceived, string packetData, int expectedChecksum,
                           int calculatedChecksum)
        {
            ErrorID = id;
            TimeReceived = timeReceived;
            PacketData = packetData;
            ExpectedChecksum = expectedChecksum;
            CalculatedChecksum = calculatedChecksum;
        }
    }
}
