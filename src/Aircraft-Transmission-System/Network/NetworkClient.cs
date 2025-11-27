using System.Net.Sockets;
using System.Text;

namespace AircraftTransmissionSystem.Network
{
    /// <summary>
    /// TCP/IP network client implementation for the Ground Terminal.
    /// Handles actual socket communication for telemetry data transmission.
    /// </summary>
    public class NetworkClient : INetworkClient
    {
        private readonly string host; // Ground Terminal host address (IP Address)
        private readonly int port;    // Ground Terminal port number

        private TcpClient? tcpClient;
        private NetworkStream? networkStream;
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
        /// Description: Establishes a TCP/IP connection to the Ground Terminal.
        ///              Creates TcpClient and NetworkStream for data transmission.
        /// Parameters: None
        /// Return Type: bool - True if connection successful, false otherwise
        /// </summary>
        /// <returns>True if connection was established successfully, false otherwise.</returns>
        public bool Connect()
        {
            try
            {
                Console.WriteLine($"[NetworkClient] Connecting to Ground Terminal at {this.host}:{this.port}...");

                // Create and connect TCP client
                this.tcpClient = new TcpClient();
                this.tcpClient.Connect(this.host, this.port);

                // Get network stream for reading/writing
                this.networkStream = this.tcpClient.GetStream();

                this.isConnected = true;
                Console.WriteLine("[NetworkClient] Connected successfully.");
                return true;
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"[NetworkClient] ERROR: Connection failed - {ex.Message}");
                this.isConnected = false;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NetworkClient] ERROR: Unexpected error during connection - {ex.Message}");
                this.isConnected = false;
                return false;
            }
        }

        /// <summary>
        /// Function Name: Disconnect
        /// Description: Closes the TCP/IP connection to the Ground Terminal.
        ///              Properly disposes of NetworkStream and TcpClient resources.
        /// Parameters: None
        /// Return Type: void
        /// </summary>
        public void Disconnect()
        {
            if (this.isConnected)
            {
                Console.WriteLine("[NetworkClient] Disconnecting from Ground Terminal...");

                try
                {
                    // Close network stream
                    if (this.networkStream != null)
                    {
                        this.networkStream.Close();
                        this.networkStream.Dispose();
                        this.networkStream = null;
                    }

                    // Close TCP client
                    if (this.tcpClient != null)
                    {
                        this.tcpClient.Close();
                        this.tcpClient.Dispose();
                        this.tcpClient = null;
                    }

                    this.isConnected = false;
                    Console.WriteLine("[NetworkClient] Disconnected.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[NetworkClient] WARNING: Error during disconnect - {ex.Message}");
                    this.isConnected = false;
                }
            }
        }

        /// <summary>
        /// Function Name: SendData
        /// Description: Sends telemetry data to the Ground Terminal via TCP/IP.
        ///              Transmits data over the established network stream.
        /// Parameters:
        ///   - data (string): The telemetry data to send
        ///   - sequenceNumber (int): The sequence number for this transmission
        /// Return Type: bool - True if sent successfully, false otherwise
        /// Exceptions:
        ///   - ArgumentNullException: Thrown when data is null
        /// </summary>
        /// <param name="data">The telemetry data to transmit.</param>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <returns>True if data was sent successfully, false if not connected or transmission failed.</returns>
        /// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
        public bool SendData(string data, int sequenceNumber)
        {
            // Check for null data
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Data cannot be null.");
            }

            // Check connection status
            if (!this.isConnected || this.networkStream == null)
            {
                Console.WriteLine("[NetworkClient] ERROR: Not connected. Cannot send data.");
                return false;
            }

            try
            {
                // Convert data to bytes (UTF-8 encoding)
                byte[] dataBytes = Encoding.UTF8.GetBytes(data + "\n");

                // Send data over network stream
                this.networkStream.Write(dataBytes, 0, dataBytes.Length);

                Console.WriteLine($"[NetworkClient] SENT [SEQ:{sequenceNumber}]: {data}");
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"[NetworkClient] ERROR: Failed to send data - {ex.Message}");
                this.isConnected = false;
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[NetworkClient] ERROR: Unexpected error during send - {ex.Message}");
                return false;
            }
        }
    }
}
