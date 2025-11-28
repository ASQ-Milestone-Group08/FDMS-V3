namespace AircraftTransmissionSystem.Packet
{
    /// <summary>
    /// Calculates checksum values from telemetry data.
    /// Implements the checksum algorithm defined in APPENDIX C.
    /// </summary>
    public class ChecksumCalculator : ICheckSumCalculator
    {
        /// <summary>
        /// Function Name: Calculate
        /// Description: Calculates the checksum value using altitude, pitch, and bank.
        ///              Formula: (Altitude + Pitch + Bank) / 3
        ///              The result is truncated to an integer (decimal part discarded).
        /// Parameters:
        ///   - altitude (double): The aircraft altitude value
        ///   - pitch (double): The aircraft pitch angle
        ///   - bank (double): The aircraft bank angle
        /// Return Type: int - The calculated checksum value
        /// </summary>
        /// <param name="altitude">The aircraft altitude value.</param>
        /// <param name="pitch">The aircraft pitch angle.</param>
        /// <param name="bank">The aircraft bank angle.</param>
        /// <returns>The integer checksum value (truncated).</returns>
        public int Calculate(double altitude, double pitch, double bank)
        {
            // APPENDIX C: Checksum = (Altitude + Pitch + Bank) / 3
            // Cast to int truncates the decimal part
            int checksum = (int)((altitude + pitch + bank) / 3);

            return checksum;
        }
    }
}
