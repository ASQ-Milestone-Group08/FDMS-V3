namespace AircraftTransmissionSystem.Packet
{
    /// <summary>
    /// Interface for calculating checksum from telemetry data.
    /// </summary>
    public interface ICheckSumCalculator
    {
        /// <summary>
        /// Calculates a checksum value based on altitude, pitch, and bank values.
        /// Formula: (Altitude + Pitch + Bank) / 3
        /// </summary>
        /// <param name="altitude">The aircraft altitude value.</param>
        /// <param name="pitch">The aircraft pitch angle.</param>
        /// <param name="bank">The aircraft bank angle.</param>
        /// <returns>An integer representing the calculated checksum.</returns>
        public int Calculate(double altitude, double pitch, double bank);
    }
}
