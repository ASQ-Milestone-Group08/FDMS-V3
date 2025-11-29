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
    static internal class SearchController
    {

        /// <summary>
        /// Searches the database for records using the aircraft tail number, start and end date.
        /// Returns a list of all telemetry records.
        /// </summary>
        /// <param name="conn">Database connection</param>
        /// <param name="criteria">Search Criteria object</param>
        /// <returns>List of telemetry data</returns>
        static public List<Telemetry> ExecuteSearch(SqlConnection conn, SearchCriteria criteria)
        {
            List<Telemetry> results = new();

            try
            {
                conn.Open();

                string commandText = "SELECT AltitudeData.TimeOfRecording, " +
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
                        Telemetry temp = new();
                        temp.Tail = criteria.TailNumber;
                        temp.Time = (DateTime)reader["TimeOfRecording"];
                        temp.AccX = (double)reader["AccelerationX"];
                        temp.AccY = (double)reader["AccelerationY"];
                        temp.AccZ = (double)reader["AccelerationZ"];
                        temp.Weight = (double)reader["Weight"];
                        temp.Altitude = (double)reader["Altitude"];
                        temp.Pitch = (double)reader["Pitch"];
                        temp.Bank = (double)reader["Bank"];

                        results.Add(temp);
                    }
                }

            }
            catch (SqlException ex)
            {
                // Log errors
            }
            finally
            {
                conn.Close();
            }

            return results;
        }


        /// <summary>
        /// Retreives data from the database and writes then to a new text file.
        /// </summary>
        /// <param name="conn">Database connection</param>
        /// <param name="criteria">Search Criteria object</param>
        static public void ExportToFile(SqlConnection conn, SearchCriteria criteria)
        {
            List<Telemetry> results = ExecuteSearch(conn, criteria);

            string newFile = criteria.TailNumber + "_" + criteria.StartTime + "_" + criteria.EndTime;
            File.Create(newFile);

            using (StreamWriter writer = new StreamWriter(newFile))
            {
                foreach (Telemetry temp in results)
                {
                    string newString = temp.Time.ToString() + ", " + temp.AccX.ToString("F6") + ", " +
                        temp.AccY.ToString("F6") + ", " + temp.AccZ.ToString("F6") + ", " +
                        temp.Weight.ToString("F6") + ", " + temp.Altitude.ToString("F6") + ", " +
                        temp.Pitch.ToString("F6") + ", " + temp.Bank.ToString("F6");

                    writer.Write(newString);
                }
            }
        }
    }
}
