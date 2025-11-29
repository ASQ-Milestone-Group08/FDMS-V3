/*
 * File Name    : DatabaseManager.cs
 * Description  : This is the class for managing database operations for storing telemetry data.
 * Author       : Andrei Haboc
 * Last Modified: November 28, 2025
 */
using System;
using System.Data.SqlClient;

namespace GroundTerminalSystem
{
    public class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Store valid Attitude (Altitude / Pitch / Bank) values
        public void StoreAltitudeData(TelemetryData data)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();

            string query = @"
                INSERT INTO AltitudeData
                (AircraftTailNumber, TimeOfRecording, TimeReceived, Altitude, Pitch, Bank)
                VALUES (@TailNumber, @TimeOfRecording, @TimeReceived, @Altitude, @Pitch, @Bank);";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TailNumber", data.TailNumber);
            cmd.Parameters.AddWithValue("@TimeOfRecording", data.TimeOfRecording);
            cmd.Parameters.AddWithValue("@TimeReceived", data.TimeReceived);
            cmd.Parameters.AddWithValue("@Altitude", data.Altitude);
            cmd.Parameters.AddWithValue("@Pitch", data.Pitch);
            cmd.Parameters.AddWithValue("@Bank", data.Bank);

            cmd.ExecuteNonQuery();
        }

        // Store valid G-Force data
        public void StoreGForceData(TelemetryData data)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();

            string query = @"
                INSERT INTO GForceData
                (AircraftTailNumber, TimeOfRecording, TimeReceived, AccelerationX, AccelerationY, AccelerationZ, Weight)
                VALUES (@TailNumber, @TimeOfRecording, @TimeReceived, @AccelX, @AccelY, @AccelZ, @Weight);";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TailNumber", data.TailNumber);
            cmd.Parameters.AddWithValue("@TimeOfRecording", data.TimeOfRecording);
            cmd.Parameters.AddWithValue("@TimeReceived", data.TimeReceived);
            cmd.Parameters.AddWithValue("@AccelX", data.AccelX);
            cmd.Parameters.AddWithValue("@AccelY", data.AccelY);
            cmd.Parameters.AddWithValue("@AccelZ", data.AccelZ);
            cmd.Parameters.AddWithValue("@Weight", data.Weight);

            cmd.ExecuteNonQuery();
        }

        public void StoreInvalidPacket(string packet, int expectedChecksum, int calculatedChecksum)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();

            string query = @"
        INSERT INTO PacketErrorData
        (TimeReceived, PacketData, ExpectedCheckSum, CalculatedCheckSum)
        VALUES (@TimeReceived, @PacketData, @Expected, @Calculated);";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TimeReceived", DateTime.Now);
            cmd.Parameters.AddWithValue("@PacketData", packet);
            cmd.Parameters.AddWithValue("@Expected", expectedChecksum);
            cmd.Parameters.AddWithValue("@Calculated", calculatedChecksum);

            cmd.ExecuteNonQuery();
        }



    }
}
