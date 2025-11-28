namespace AircraftTransmissionSystem.Network
{
    /// <summary>
    /// Interface for network communication with the Ground Terminal.
    /// Defines the contract for sending packets to remote destinations.
    /// Allows different implementations (TCP, UDP, Mock, etc.) using Strategy Pattern.
    /// </summary>
    public interface INetworkClient
    {
        /// <summary>
        /// Function Name: SendData
        /// Description: Sends telemetry data to the Ground Terminal.
        ///              Transmits the data over the network connection.
        /// Parameters:
        ///   - data (string): The telemetry data to send to the Ground Terminal
        ///   - sequenceNumber (int): The sequence number for this transmission
        /// Return Type: bool
        ///              - true: Data sent successfully
        ///              - false: Data transmission failed
        /// </summary>
        /// <param name="data">The telemetry data to transmit.</param>
        /// <param name="sequenceNumber">The sequence number.</param>
        /// <returns>True if data was sent successfully, false otherwise.</returns>
        public bool SendData(string data, int sequenceNumber);

        /// <summary>
        /// Function Name: Connect
        /// Description: Establishes connection to the Ground Terminal.
        ///              Opens network socket and prepares for data transmission.
        /// Parameters: None
        /// Return Type: bool
        ///              - true: Connection established successfully
        ///              - false: Connection failed
        /// </summary>
        /// <returns>True if connection was successful, false otherwise.</returns>
        public bool Connect();

        /// <summary>
        /// Function Name: Disconnect
        /// Description: Closes the connection to the Ground Terminal.
        ///              Properly closes network socket and releases resources.
        /// Parameters: None
        /// Return Type: void
        /// </summary>
        public void Disconnect();

        /// <summary>
        /// Property Name: IsConnected
        /// Description: Indicates whether the client is currently connected to the Ground Terminal.
        /// Return Type: bool
        ///              - true: Connected and ready to send
        ///              - false: Not connected or connection lost
        /// </summary>
        public bool IsConnected { get; }
    }
}
