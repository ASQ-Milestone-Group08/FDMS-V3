/*
 * File Name    : Program.cs
 * Description  : This is the main entry point for the Aircraft Transmission System console application.
 *                The user is prompted to select an aircraft, and the system initializes all components.
 *                The user has three aircraft options which are defined in the Aircraft enum.
 *                The TCP/IP network client connects to the Ground Terminal using configuration from appsettings.json.
 *                Finally, all meaningful logs are recorded by the logger in both console and log file.
 * Author       : Chris Park
 * Last Modified: November 28, 2025
 */

using AircraftTransmissionSystem.Controllers;
using AircraftTransmissionSystem.Logging;
using AircraftTransmissionSystem.Network;
using AircraftTransmissionSystem.Telemetry;
using AircraftTransmissionSystem.Packet;
using System.Text.Json;

namespace AircraftTransmissionSystem
{
    /// <summary>
    /// Main entry point for the Aircraft Transmission System.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Main entry point for the console application.
        /// </summary>
        public static void Main(string[] args)
        {
            Console.WriteLine("Aircraft Transmission System");
            Console.WriteLine("============================");
            Console.WriteLine();

            // Initialize all components using dependency injection
            RunTransmissionSystem();
        }

        /// <summary>
        /// Function Name: SelectAircraft
        /// Description: Prompts the user to select an aircraft from the available options.
        /// Parameters: None
        /// Return Type: Aircraft - The selected aircraft enum value
        /// </summary>
        /// <returns>The selected Aircraft enum value.</returns>
        private static Aircraft SelectAircraft()
        {
            Console.WriteLine("Select an aircraft:");
            Console.WriteLine("1. C-FGAX");
            Console.WriteLine("2. C-GEFC");
            Console.WriteLine("3. C-QWWT");
            Console.Write("Enter your choice (1-3): ");

            while (true)
            {
                // Wait for user input
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("Selected: C-FGAX");
                        return Aircraft.C_FGAX;
                    case "2":
                        Console.WriteLine("Selected: C-GEFC");
                        return Aircraft.C_GEFC;
                    case "3":
                        Console.WriteLine("Selected: C-QWWT");
                        return Aircraft.C_QWWT;
                    default:
                        Console.Write("Invalid choice. Please enter 1, 2, or 3: ");
                        break;
                }
            }
        }

        /// <summary>
        /// Function Name: RunTransmissionSystem
        /// Description: Initializes and runs the transmission system with all required components.
        ///              Demonstrates dependency injection pattern.
        /// Parameters: None
        /// Return Type: void
        /// </summary>
        private static void RunTransmissionSystem()
        {
            // Load configuration from appsettings.json
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            string jsonString = File.ReadAllText(configPath);
            using JsonDocument document = JsonDocument.Parse(jsonString);
            JsonElement root = document.RootElement;

            // Select aircraft from user input
            Aircraft selectedAircraft = SelectAircraft();
            Console.WriteLine();

            // Read log configuration from appsettings.json
            // BasePath and FileNamePrefix are required in appsettings.json
            // It will throw null exceptions if required fields are missing
            JsonElement loggingElement = root.GetProperty("Logging");
            string logBasePath = loggingElement.GetProperty("BasePath").GetString()
                ?? throw new InvalidOperationException("Logging:BasePath cannot be null in appsettings.json");
            string logFileNamePrefix = loggingElement.GetProperty("FileNamePrefix").GetString()
                ?? throw new InvalidOperationException("Logging:FileNamePrefix cannot be null in appsettings.json");

            // Construct log file path: {BasePath}/{FileNamePrefix}-yyyyMMdd.log
            string logFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                logBasePath,
                $"{logFileNamePrefix}-{DateTime.Now:yyyyMMdd}.log"
            );

            // Display log file path for debugging
            Console.WriteLine($"Log file will be created at: {logFilePath}");
            Console.WriteLine();

            // Read network configuration from appsettings.json
            // Host and Port are required in appsettings.json
            // It will throw null exceptions if required fields are missing
            JsonElement networkElement = root.GetProperty("Network");
            JsonElement groundTerminalElement = networkElement.GetProperty("GroundTerminal");
            string networkHost = groundTerminalElement.GetProperty("Host").GetString()
                ?? throw new InvalidOperationException("Network:GroundTerminal:Host cannot be null in appsettings.json");
            int networkPort = groundTerminalElement.GetProperty("Port").GetInt32();

            try
            {
                // Initialize components (Dependency Injection)
                Console.WriteLine("Initializing logger...");
                ILogger logger = new Logger(logFilePath);

                logger.LogInfo("Initializing telemetry data source...");
                ITelemetrySource dataSource = new FileTelemetryReader(selectedAircraft);

                logger.LogInfo("Initializing packet builder...");
                // Convert Aircraft enum to tail number string (C_FGAX -> C-FGAX)
                string aircraftTailNumber = selectedAircraft.ToString().Replace('_', '-');
                IPacketBuilder packetBuilder = new PacketBuilder(aircraftTailNumber);

                logger.LogInfo("Initializing network client...");
                INetworkClient networkClient = new NetworkClient(networkHost, networkPort, logger);

                // Create and start transmission controller
                logger.LogInfo("Creating transmission controller...");
                TransmissionController controller = new TransmissionController(
                    dataSource,
                    packetBuilder,
                    networkClient,
                    logger
                );

                logger.LogInfo("Starting transmission...");
                Console.WriteLine("Press any key to stop transmission.");
                Console.WriteLine();

                controller.Start();

                // Wait for user input to stop
                if (!Console.IsInputRedirected)
                {
                    Console.ReadKey(true);
                }
                else
                {
                    // For automated environments, wait for a short period then stop
                    Thread.Sleep(5000);
                }

                controller.Stop();

                Console.WriteLine();
                Console.WriteLine("Transmission system stopped.");
            }
            catch (Exception ex)
            {
                // Logger might not be available in catch block, use Console
                Console.WriteLine($"FATAL ERROR: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");

            // Only wait for key if console input is available
            if (!Console.IsInputRedirected)
            {
                Console.ReadKey();
            }
        }
    }
}
