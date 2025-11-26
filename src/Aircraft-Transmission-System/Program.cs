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

            // TODO: Initialize TransmissionController and start transmission
            // For now, just test the FileTelemetryReader

            TestTelemetryReader();
        }

        /// <summary>
        /// Test method for FileTelemetryReader functionality.
        /// </summary>
        private static void TestTelemetryReader()
        {
            string telemetryFilePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "telemetry-data",
                "C-FGAX.txt"
            );

            try
            {
                Console.WriteLine($"Reading telemetry from: {telemetryFilePath}");
                Console.WriteLine();

                using (ITelemetrySource reader = new FileTelemetryReader(telemetryFilePath))
                {
                    int lineCount = 0;
                    while (reader.HasMoreData && lineCount < 5)
                    {
                        string? telemetry = reader.GetNextReading();
                        if (telemetry != null)
                        {
                            Console.WriteLine($"Line {++lineCount}: {telemetry}");
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine($"Successfully read {lineCount} lines of telemetry data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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