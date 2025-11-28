namespace AircraftTransmissionSystem.Logging
{
    /// <summary>
    /// Interface for logging system events and errors.
    /// Defines the contract for logging messages at different severity levels.
    /// Allows different implementations (file, console, database) using Strategy Pattern.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Function Name: LogInfo
        /// Description: Logs an informational message.
        ///              Used for general system events and normal operations.
        /// Parameters:
        ///   - message (string): The informational message to log
        /// Return Type: void
        /// </summary>
        /// <param name="message">The informational message to log.</param>
        public void LogInfo(string message);

        /// <summary>
        /// Function Name: LogError
        /// Description: Logs an error message.
        ///              Used for errors and exceptions that occur during operation.
        /// Parameters:
        ///   - message (string): The error message to log
        /// Return Type: void
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public void LogError(string message);

        /// <summary>
        /// Function Name: LogWarning
        /// Description: Logs a warning message.
        ///              Used for potential issues that don't prevent operation.
        /// Parameters:
        ///   - message (string): The warning message to log
        /// Return Type: void
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public void LogWarning(string message);
    }
}
