namespace AircraftTransmissionSystem.Network
{
    /// <summary>
    /// Stub implementation of network client for the Ground Terminal.
    /// Simulates network communication for testing and development.
    /// Will be replaced with actual TCP/UDP implementation later.
    /// </summary>
    public class NetworkClient : INetworkClient
    {
        private readonly string host; // Ground Terminal host address (IP Address)
        private readonly int port;    // Ground Terminal port number
        private bool isConnected;

        /// <summary>
        /// Property Name: IsConnected
        /// Description: Indicates whether the client is currently connected to the Ground Terminal.
        /// Return Type: bool - Connection status
        /// </summary>
        public bool IsConnected => this.isConnected;

        /// <summary>
        /// Function Name: NetworkClient (Constructor)
        /// Description: Initializes a new instance of the NetworkClient class.
        /// Parameters:
        ///   - host (string): The Ground Terminal host address (e.g., "127.0.0.1")
        ///   - port (int): The Ground Terminal port number (e.g., 5000)
        /// Return Type: N/A (Constructor)
        /// Exceptions:
        ///   - ArgumentNullException: Thrown when host is null or empty
        ///   - ArgumentOutOfRangeException: Thrown when port is out of valid range
        /// </summary>
        /// <param name="host">The Ground Terminal host address.</param>
        /// <param name="port">The Ground Terminal port number.</param>
        /// <exception cref="ArgumentNullException">Thrown when host is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when port is out of valid range (1-65535).</exception>
        public NetworkClient(string host, int port)
        {
            if (string.IsNullOrWhiteSpace(host))
            {
                throw new ArgumentNullException(nameof(host), "Host cannot be null or empty.");
            }

            if (port < 1 || port > 65535)
            {
                throw new ArgumentOutOfRangeException(nameof(port), "Port must be between 1 and 65535.");
            }

            // Initialize fields
            this.host = host;
            this.port = port;
            this.isConnected = false;
        }

        /// <summary>
        /// Function Name: Connect
        /// Description: Simulates establishing connection to the Ground Terminal.
        ///              In the stub, this always succeeds immediately.
        /// Parameters: None
        /// Return Type: bool - Always true in stub implementation
        /// </summary>
        /// <returns>True indicating successful connection.</returns>
        public bool Connect()
        {
            Console.WriteLine($"[NetworkClient] Connecting to Ground Terminal at {this.host}:{this.port}...");
            this.isConnected = true;
            Console.WriteLine("[NetworkClient] Connected successfully.");
            return true;
        }

        /// <summary>
        /// Function Name: Disconnect
        /// Description: Simulates closing the connection to the Ground Terminal.
        ///              Releases simulated network resources.
        /// Parameters: None
        /// Return Type: void
        /// </summary>
        public void Disconnect()
        {
            if (this.isConnected)
            {
                Console.WriteLine("[NetworkClient] Disconnecting from Ground Terminal...");
                this.isConnected = false;
                Console.WriteLine("[NetworkClient] Disconnected.");
            }
        }

        /// <summary>
        /// Function Name: SendData
        /// Description: Simulates sending telemetry data to the Ground Terminal.
        ///              In the stub, this just logs the data to console.
        /// Parameters:
        ///   - data (string): The telemetry data to send
        ///   - sequenceNumber (int): The sequence number for this transmission
        /// Return Type: bool - True if connected, false otherwise
        /// Exceptions:
        ///   - ArgumentNullException: Thrown when data is null
        /// </summary>
        /// <param name="data">The telemetry data to transmit.</param>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <returns>True if data was "sent" successfully, false if not connected.</returns>
        /// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
        public bool SendData(string data, int sequenceNumber)
        {
            // Check for null data
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Data cannot be null.");
            }

            // Check connection status
            if (!this.isConnected)
            {
                Console.WriteLine("[NetworkClient] ERROR: Not connected. Cannot send data.");
                return false;
            }

            // Simulate data transmission (stub implementation)
            Console.WriteLine($"[NetworkClient] SENT [SEQ:{sequenceNumber}]: {data}");
            return true;
        }
    }
}
