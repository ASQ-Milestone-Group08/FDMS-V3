using System.Timers;
using AircraftTransmissionSystem.Network;
using AircraftTransmissionSystem.Logging;

namespace AircraftTransmissionSystem.Controllers
{
    /// <summary>
    /// Main controller that orchestrates the Aircraft Transmission System.
    /// Manages the complete transmission cycle: reading telemetry, building packets, and transmitting to Ground Terminal.
    /// Implements REQ-FN-080 and REQ-FN-090: Transmit telemetry data every second.
    /// </summary>
    public class TransmissionController
    {
        private readonly ITelemetrySource dataSource;   // Interface for telemetry data 
        private readonly INetworkClient networkClient;  // Interface for TCP/IP network client
        private readonly ILogger logger;                // Interface for logging
        private System.Timers.Timer? transmissionTimer; // 
        private int sequenceNumber;                     // Unique number of each packet
        private bool isRunning;                         // Flag for system running

        /// <summary>
        /// Function Name: TransmissionController (Constructor)
        /// Description: Initializes a new instance of the TransmissionController class.
        ///              Uses dependency injection for all required components.
        /// Parameters:
        ///   - dataSource (ITelemetrySource): The telemetry data source
        ///   - networkClient (INetworkClient): The network client for transmission
        ///   - logger (ILogger): The logger for recording events
        /// Return Type: N/A (Constructor)
        /// Exceptions:
        ///   - ArgumentNullException: Thrown when any parameter is null
        /// </summary>
        /// <param name="dataSource">The telemetry data source.</param>
        /// <param name="networkClient">The network client.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
        public TransmissionController(
            ITelemetrySource dataSource,
            INetworkClient networkClient,
            ILogger logger)
        {
            // Null exception checks for dependencies
            this.dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
            this.networkClient = networkClient ?? throw new ArgumentNullException(nameof(networkClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Initialize state
            this.sequenceNumber = 0;
            this.isRunning = false;
        }

        /// <summary>
        /// Function Name: Start
        /// Description: Initializes and begins the transmission cycle.
        ///              Connects to Ground Terminal and starts the 1-second interval timer.
        /// Parameters: None
        /// Return Type: void
        /// </summary>
        public void Start()
        {
            if (this.isRunning)
            {
                this.logger.LogWarning("TransmissionController is already running.");
                return;
            }

            this.logger.LogInfo("Starting Aircraft Transmission System...");

            // Connect to Ground Terminal
            if (!this.networkClient.Connect())
            {
                this.logger.LogError("Failed to connect to Ground Terminal. Transmission aborted.");
                return;
            }

            // Initialize sequence number
            this.sequenceNumber = 0;

            // Initialize and start timer for 1-second intervals (1000 milliseconds)
            this.transmissionTimer = new System.Timers.Timer(1000);
            this.transmissionTimer.Elapsed += OnTimerElapsed; // Event handler for timer - It is triggered every second
            this.transmissionTimer.AutoReset = true;          // Enable recurring events of OnTimerElapsed
            this.transmissionTimer.Start();                   // Start the timer

            this.isRunning = true;
            this.logger.LogInfo("Transmission cycle started. Transmitting every 1 second.");
        }

        /// <summary>
        /// Function Name: Stop
        /// Description: Halts the transmission cycle and cleans up resources.
        ///              Stops the timer, disconnects from Ground Terminal, and disposes resources.
        /// Parameters: None
        /// Return Type: void
        /// </summary>
        public void Stop()
        {
            if (!this.isRunning)
            {
                this.logger.LogWarning("TransmissionController is not running.");
                return;
            }

            this.logger.LogInfo("Stopping Aircraft Transmission System...");

            // Stop and dispose timer
            if (this.transmissionTimer != null)
            {
                this.transmissionTimer.Stop();
                this.transmissionTimer.Elapsed -= OnTimerElapsed;
                this.transmissionTimer.Dispose();
                this.transmissionTimer = null;
            }

            // Disconnect from Ground Terminal
            this.networkClient.Disconnect();

            // Dispose data source
            this.dataSource.Dispose();

            this.isRunning = false;
            this.logger.LogInfo($"Transmission stopped. Total packets sent: {this.sequenceNumber}");
        }

        /// <summary>
        /// Function Name: OnTimerElapsed
        /// Description: Event handler for timer elapsed events.
        ///              Delegates to OnTick for the actual transmission logic.
        /// Parameters:
        ///   - sender (object?): The timer object that raised this event. 
        ///                       Required by the ElapsedEventHandler delegate signature.
        ///   - e (ElapsedEventArgs): Event arguments containing the signal time.
        ///                          Required by the ElapsedEventHandler delegate signature.
        /// Return Type: void
        /// </summary>
        /// <param name="sender">The timer object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            OnTick(); // Execute one transmission cycle
        }

        /// <summary>
        /// Function Name: OnTick
        /// Description: Executes one transmission cycle.
        ///              Flow: Read telemetry → Send to Ground Terminal
        ///              Handles end-of-file gracefully by stopping transmission.
        ///              Note: Packet building and checksum calculation will be done by another team member.
        /// Parameters: None
        /// Return Type: void
        /// </summary>
        private void OnTick()
        {
            try
            {
                // Check if more data is available
                if (!this.dataSource.HasMoreData)
                {
                    // No more data to read >> stop transmission
                    this.logger.LogInfo("End of telemetry data reached. Stopping transmission.");
                    Stop();
                    return;
                }

                // Read one line of telemetry data
                string? telemetryData = this.dataSource.GetNextReading();
                if (string.IsNullOrWhiteSpace(telemetryData))
                {
                    this.logger.LogWarning("Empty telemetry data received. Skipping this cycle.");
                    return;
                }

                // Transmit to Ground Terminal
                bool success = this.networkClient.SendData(telemetryData, this.sequenceNumber);

                if (success)
                {
                    this.logger.LogInfo($"Data #{this.sequenceNumber} transmitted successfully.");
                    this.sequenceNumber++; // Increment sequence number for next transmission
                }
                else
                {
                    this.logger.LogError($"Failed to transmit data #{this.sequenceNumber}.");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Error during transmission cycle: {ex.Message}");
            }
        }
    }
}
