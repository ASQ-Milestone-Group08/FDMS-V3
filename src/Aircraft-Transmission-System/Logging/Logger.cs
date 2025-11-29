/*
 * File Name    : Logger.cs
 * Description  : This logger class is designed to log system events and errors
 *               in both console and a file.
 *               It has three log levels: INFO, WARNING, and ERROR .
 * Author       : Chris Park
 * Last Modified: November 28, 2025
 */


namespace AircraftTransmissionSystem.Logging
{
    /// <summary>
    /// File-based logger implementation.
    /// Writes log messages to a file with timestamps and severity levels.
    /// Thread-safe for concurrent logging operations.
    /// </summary>
    public class Logger : ILogger
    {
        private enum LogLevel { INFO, WARNING, ERROR };
        private readonly string logFilePath;
        private readonly object lockObject = new();

        /// <summary>
        /// Function Name: Logger (Constructor)
        /// Description: Initializes a new instance of the Logger class.
        ///              Creates the log file if it doesn't exist.
        /// Parameters:
        ///   - logFilePath (string): The path to the log file
        /// Return Type: N/A (Constructor)
        /// Exceptions:
        ///   - ArgumentNullException: Thrown when logFilePath is null or empty
        /// </summary>
        /// <param name="logFilePath">The path to the log file.</param>
        /// <exception cref="ArgumentNullException">Thrown when logFilePath is null or empty.</exception>
        public Logger(string logFilePath)
        {
            if (string.IsNullOrWhiteSpace(logFilePath))
            {
                throw new ArgumentNullException(nameof(logFilePath), "Log file path cannot be null or empty.");
            }

            this.logFilePath = logFilePath;

            // Ensure the directory exists
            string? directory = Path.GetDirectoryName(logFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Function Name: LogInfo
        /// Description: Logs an informational message to the file.
        ///              Prefixes message with timestamp and [INFO] tag.
        /// Parameters:
        ///   - message (string): The informational message to log
        /// Return Type: void
        /// </summary>
        /// <param name="message">The informational message to log.</param>
        public void LogInfo(string message)
        {
            WriteLog(LogLevel.INFO, message);
        }

        /// <summary>
        /// Function Name: LogError
        /// Description: Logs an error message to the file.
        ///              Prefixes message with timestamp and [ERROR] tag.
        /// Parameters:
        ///   - message (string): The error message to log
        /// Return Type: void
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public void LogError(string message)
        {
            WriteLog(LogLevel.ERROR, message);
        }

        /// <summary>
        /// Function Name: LogWarning
        /// Description: Logs a warning message to the file.
        ///              Prefixes message with timestamp and [WARNING] tag.
        /// Parameters:
        ///   - message (string): The warning message to log
        /// Return Type: void
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public void LogWarning(string message)
        {
            WriteLog(LogLevel.WARNING, message);
        }

        /// <summary>
        /// Function Name: WriteLog
        /// Description: Internal helper method to write log messages to file.
        ///              Thread-safe using lock to prevent concurrent write conflicts.
        /// Parameters:
        ///   - level (LogLevel): The log level (INFO, WARNING, ERROR)
        ///   - message (string): The message to log
        /// Return Type: void
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="message">The message to log.</param>
        private void WriteLog(LogLevel level, string message)
        {
            lock (this.lockObject)
            {
                try
                {
                    // Format log entry
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string logEntry = $"[{timestamp}] [{level}] {message}";

                    // Write to log file
                    File.AppendAllText(this.logFilePath, logEntry + Environment.NewLine);

                    // Also write to console for immediate feedback
                    Console.WriteLine(logEntry);
                }
                catch (Exception ex)
                {
                    // If logging fails, write to console as fallback
                    Console.WriteLine($"[LOGGER ERROR] Failed to write to log file: {ex.Message}");
                    Console.WriteLine($"[{level}] {message}");
                }
            }
        }
    }
}
