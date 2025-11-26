namespace AircraftTransmissionSystem
{
    /// <summary>
    /// Reads telemetry data from ASCII text files.
    /// Supports reading comma-delimited telemetry following APPENDIX D format:
    /// Timestamp, Accel-X, Accel-Y, Accel-Z, Weight, Altitude, Pitch, Bank
    /// </summary>
    public class FileTelemetryReader : ITelemetrySource, IDisposable
    {
        private readonly string filePath;
        private StreamReader? reader;
        private bool disposed = false;

        /// <summary>
        /// Function Name: FileTelemetryReader (Constructor)
        /// Description: Initializes a new instance of the FileTelemetryReader class.
        ///              Opens the specified telemetry file and prepares it for reading.
        ///              Validates that the file path is not null/empty and that the file exists.
        /// Parameters:
        ///   - filePath (string): The absolute or relative path to the ASCII telemetry file to read.
        ///                        Must point to an existing file in APPENDIX D format.
        /// Return Type: N/A (Constructor)
        /// Exceptions:
        ///   - ArgumentNullException: Thrown when filePath is null, empty, or whitespace.
        ///   - FileNotFoundException: Thrown when the specified file does not exist.
        ///   - IOException: Thrown when the file cannot be opened (via InitializeReader).
        /// </summary>
        /// <param name="filePath">The path to the telemetry file to read.</param>
        /// <exception cref="ArgumentNullException">Thrown when filePath is null or empty.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the specified file does not exist.</exception>
        public FileTelemetryReader(string filePath)
        {
            // Check whether filePath is empty or not
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "File path cannot be null or empty.");
            }

            // Check whether the file exist or not
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Telemetry file not found: {filePath}", filePath);
            }

            this.filePath = filePath;
            InitializeReader();
        }

        /// <summary>
        /// Property Name: HasMoreData
        /// Description: Indicates whether more telemetry data is available to read from the file.
        ///              Checks if the StreamReader is still open and has not reached the end of the file.
        ///              This property should be checked before calling GetNextReading() to avoid null returns.
        /// Parameters: None
        /// Return Type: bool
        ///              - true: More data is available, StreamReader is active and not at end of stream
        ///              - false: End of file reached, reader is null, or object is disposed
        /// </summary>
        public bool HasMoreData
        {
            get
            {
                if (this.reader == null || this.disposed)
                {
                    return false;
                }

                return !reader.EndOfStream;
            }
        }

        /// <summary>
        /// Function Name: GetNextReading
        /// Description: Retrieves the next line of telemetry data from the file.
        ///              Reads one line from the current position in the file and advances the cursor.
        ///              Automatically skips empty or whitespace-only lines.
        ///              Returns null if the end of file is reached or if the reader is disposed.
        /// Parameters: None
        /// Return Type: string? (nullable string)
        ///              - Returns a comma-delimited string in format: Timestamp, Accel-X, Accel-Y, Accel-Z, Weight, Altitude, Pitch, Bank
        ///              - Returns null when end of file is reached, reader is null, or object is disposed
        /// Exceptions:
        ///   - IOException: Thrown when a file I/O error occurs during reading.
        /// </summary>
        /// <returns>
        /// A string containing one line of telemetry data, or null if end of file is reached.
        /// </returns>
        /// <exception cref="IOException">Thrown when a file I/O error occurs.</exception>
        public string? GetNextReading()
        {
            // Check whether the reader is empty or already disposed
            if (this.reader == null || this.disposed)
            {
                return null;
            }

            try
            {
                // Check whether there is no content to read or not
                if (this.reader.EndOfStream)
                {
                    return null;
                }

                // Read one line from the opened file
                string? line = this.reader.ReadLine();

                // Skip empty lines
                while (line != null && string.IsNullOrWhiteSpace(line) && !this.reader.EndOfStream)
                {
                    line = this.reader.ReadLine();
                }

                return line;
            }
            catch (IOException ex)
            {
                throw new IOException($"Error reading from telemetry file: {this.filePath}", ex);
            }
        }

        /// <summary>
        /// Function Name: InitializeReader
        /// Description: Initializes the StreamReader for reading the telemetry file.
        ///              Opens the file specified by the filePath field and creates a new StreamReader.
        ///              This is a private helper method called by the constructor and Reset() method.
        /// Parameters: None
        /// Return Type: void
        /// Exceptions:
        ///   - IOException: Thrown when the file cannot be opened (wrapped with context message).
        /// </summary>
        /// <exception cref="IOException">Thrown when the file cannot be opened.</exception>
        private void InitializeReader()
        {
            try
            {
                // Open the file in read mode
                // Place the reading cursor at the beginning of the file
                this.reader = new StreamReader(this.filePath);
            }
            catch (IOException ex)
            {
                throw new IOException($"Failed to open telemetry file: {this.filePath}", ex);
            }
        }

        /// <summary>
        /// Function Name: Reset
        /// Description: Resets the reader to the beginning of the file.
        ///              Closes the current StreamReader, disposes it, and reopens the file from the start.
        ///              Useful for re-reading the same telemetry file without creating a new instance.
        /// Parameters: None
        /// Return Type: void
        /// Exceptions:
        ///   - IOException: May be thrown by InitializeReader() if the file cannot be reopened.
        /// </summary>
        public void Reset()
        {
            this.reader?.Close();   // Close the opened file
            this.reader?.Dispose(); // Free the resource
            InitializeReader();     // Reopen the file
        }

        /// <summary>
        /// Function Name: Dispose
        /// Description: Releases all resources used by the FileTelemetryReader.
        ///              Implements IDisposable pattern to properly close and dispose the StreamReader.
        ///              This method should be called when finished reading telemetry data, or use 'using' statement for automatic disposal.
        /// Parameters: None
        /// Return Type: void
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Disable destructor because StreamReader is already cleaned up
        }

        /// <summary>
        /// Function Name: Dispose (Overload)
        /// Description: Releases the unmanaged resources used by the FileTelemetryReader and optionally releases the managed resources.
        ///              This is the core disposal method implementing the IDisposable pattern.
        ///              Ensures the StreamReader is properly closed and disposed only once.
        /// Parameters:
        ///   - disposing (bool): Indicates whether to release managed resources.
        ///                       true: Called from Dispose(), releases both managed and unmanaged resources.
        ///                       false: Called from finalizer, releases only unmanaged resources.
        /// Return Type: void
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed) // Skip disposing if it is already cleaned
            {
                if (disposing)
                {
                    // '?' is null condition operator >> if the reader is null, it skip the next process
                    this.reader?.Close();   // Close the file stream
                    this.reader?.Dispose(); // Release file handle and close stream
                    this.reader = null;     // Initialize `reader`
                }

                // Update disposed flag to `ture`
                this.disposed = true;
            }
        }

        /// <summary>
        /// Function Name: ~FileTelemetryReader (Finalizer/Destructor)
        /// Description: Finalizer for FileTelemetryReader, called by the garbage collector.
        ///              Ensures resources are cleaned up even if Dispose() was not called.
        ///              Part of the IDisposable pattern for proper resource management.
        /// Parameters: None
        /// Return Type: N/A (Finalizer)
        /// </summary>
        ~FileTelemetryReader()
        {
            Dispose(false);
        }
    }
}
