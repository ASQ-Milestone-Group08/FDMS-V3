/*
 * FILENAME:        SearchController.cs
 * ASSIGNMENT:      Advanced Software Quality - Final Project
 * DESCRIPTION:     Performs the search functionality for the ground terminal. Also provides
 *                  functionality to export data from the database to a text file.
 */

using GroundTerminalSystem.Models;
using System.Data.SqlClient;

namespace GroundTerminalSystem
{
    internal class SearchController
    {

        private readonly string connectionString;

        public SearchController(string connString)
        {
            connectionString = connString;
        }


        /// <summary>
        /// Searches the database for records using the aircraft tail number, start and end date.
        /// Returns a list of all telemetry records.
        /// </summary>
        /// <param name="criteria">Search Criteria object</param>
        /// <returns>List of telemetry data</returns>
        public List<TelemetryData> ExecuteSearch(SearchCriteria criteria)
        {
            List<TelemetryData> results = new();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string commandText = "SELECT AltitudeData.TimeOfRecording, AltitudeData.TimeReceived," +
                                            "AccelerationX, AccelerationY, AccelerationZ, Weight, " +
                                            "Altitude, Pitch, Bank " +
                                        "FROM AltitudeData INNER JOIN GForceData " +
                                            "ON AltitudeData.AircraftTailNumber = GForceData.AircraftTailNumber " +
                                        "WHERE AltitudeData.AircraftTailNumber = @tail " +
                                            "AND AltitudeData.TimeOfRecording BETWEEN @start AND @end " +
                                        "ORDER BY TimeOfRecording ASC; ";
                SqlCommand cmd = new SqlCommand(commandText, conn);

                cmd.Parameters.AddWithValue("@tail", criteria.TailNumber);
                cmd.Parameters.AddWithValue("@start", criteria.StartTime);
                cmd.Parameters.AddWithValue("@end", criteria.EndTime);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TelemetryData temp = new TelemetryData
                        {
                            TailNumber = criteria.TailNumber,
                            TimeOfRecording = (DateTime)reader["TimeOfRecording"],
                            TimeReceived = (DateTime)reader["TimeReceived"],
                            AccelX = (double)reader["AccelerationX"],
                            AccelY = (double)reader["AccelerationY"],
                            AccelZ = (double)reader["AccelerationZ"],
                            Weight = (double)reader["Weight"],
                            Altitude = (double)reader["Altitude"],
                            Pitch = (double)reader["Pitch"],
                            Bank = (double)reader["Bank"]
                        };

                        results.Add(temp);
                    }
                }
            }

            return results;
        }


        /// <summary>
        /// Retreives data from the database and writes then to a new text file.
        /// </summary>
        /// <param name="criteria">Search Criteria object</param>
        public void ExportToFile(SearchCriteria criteria)
        {
            List<TelemetryData> results = ExecuteSearch(criteria);

            string newFile = criteria.TailNumber;
            using (StreamWriter writer = new StreamWriter(newFile + ".txt"))
            {
                foreach (TelemetryData temp in results)
                {
                    string newString = temp.TimeReceived.ToString() + ", " + temp.AccelX.ToString("F6") + ", " +
                        temp.AccelY.ToString("F6") + ", " + temp.AccelZ.ToString("F6") + ", " +
                        temp.Weight.ToString("F6") + ", " + temp.Altitude.ToString("F6") + ", " +
                        temp.Pitch.ToString("F6") + ", " + temp.Bank.ToString("F6");

                    writer.WriteLine(newString);
                }
            }
        }
    }
}
