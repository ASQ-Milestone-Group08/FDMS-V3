/*
 * File Name    : ChecksumCalculatorTests.cs
 * Description  : Unit tests for the ChecksumCalculator class.
 *                Tests TS-ATS-CC-001: Checksum Calculation Accuracy
 * Author       : Chris Park
 * Last Modified: November 28, 2025
 */

using AircraftTransmissionSystem.Packet;

namespace AircraftTransmissionSystem.Tests
{
    /// <summary>
    /// Unit tests for ChecksumCalculator class.
    /// TS-ATS-CC-001: Checksum Calculation Accuracy
    /// </summary>
    [TestFixture]
    public class ChecksumCalculatorTests
    {
        private ChecksumCalculator calculator;

        [SetUp]
        public void Setup()
        {
            calculator = new ChecksumCalculator();
        }

        /// <summary>
        /// TS-ATS-CC-001: Verify checksum calculation conforms to APPENDIX C algorithm.
        /// APPENDIX C Algorithm: (Altitude + Pitch + Bank) / 3, truncated to int
        /// Test with standard values to verify the formula is correctly implemented.
        /// </summary>
        [Test]
        public void Calculate_VerifyAPPENDIXCAlgorithm_ReturnsCorrectChecksum()
        {
            // Arrange
            double altitude = 1000.0;
            double pitch = 5.0;
            double bank = 3.0;
            // Expected: (1000 + 5 + 3) / 3 = 336 (truncated)
            int expected = 336;

            // Act
            int result = calculator.Calculate(altitude, pitch, bank);

            // Assert
            Assert.That(result, Is.EqualTo(expected),
                "Checksum should be calculated as (Altitude + Pitch + Bank) / 3 and truncated to int");
        }

        /// <summary>
        /// TS-ATS-CC-001: Test with valid telemetry data from C-FGAX aircraft file.
        /// Real data: 7_8_2018 19:34:3,-0.319754, -0.716176, 1.797150, 2154.670410, 1643.844116, 0.022278, 0.033622
        /// </summary>
        [Test]
        public void Calculate_WithRealDataFromCFGAX_ReturnsCorrectChecksum()
        {
            // Arrange - Real telemetry data from C-FGAX.txt
            double altitude = 1643.844116;
            double pitch = 0.022278;
            double bank = 0.033622;
            // Expected: (1643.844116 + 0.022278 + 0.033622) / 3 = 547 (truncated from 547.966672)
            int expected = 547;

            // Act
            int result = calculator.Calculate(altitude, pitch, bank);

            // Assert
            Assert.That(result, Is.EqualTo(expected),
                "Checksum calculation should work correctly with C-FGAX telemetry data");
        }

        /// <summary>
        /// TS-ATS-CC-001: Test with valid telemetry data from C-GEFC aircraft file.
        /// Real data: 9_5_2018 9:44:3,-0.000587, 0.008818, 0.055259, 2250.823975, 4002.945313, -0.017309, -0.001649
        /// </summary>
        [Test]
        public void Calculate_WithRealDataFromCGEFC_ReturnsCorrectChecksum()
        {
            // Arrange - Real telemetry data from C-GEFC.txt
            double altitude = 4002.945313;
            double pitch = -0.017309;
            double bank = -0.001649;
            // Expected: (4002.945313 + (-0.017309) + (-0.001649)) / 3 = 1334 (truncated from 1334.308785)
            int expected = 1334;

            // Act
            int result = calculator.Calculate(altitude, pitch, bank);

            // Assert
            Assert.That(result, Is.EqualTo(expected),
                "Checksum calculation should work correctly with C-GEFC telemetry data");
        }

        /// <summary>
        /// TS-ATS-CC-001: Test with valid telemetry data from C-QWWT aircraft file.
        /// Real data: 9_5_2018 9:40:57,-0.022142, 0.058109, 0.345075, 2252.778320, 3987.080566, -0.072016, -0.006201
        /// </summary>
        [Test]
        public void Calculate_WithRealDataFromCQWWT_ReturnsCorrectChecksum()
        {
            // Arrange - Real telemetry data from C-QWWT.txt
            double altitude = 3987.080566;
            double pitch = -0.072016;
            double bank = -0.006201;
            // Expected: (3987.080566 + (-0.072016) + (-0.006201)) / 3 = 1329 (truncated from 1329.000783)
            int expected = 1329;

            // Act
            int result = calculator.Calculate(altitude, pitch, bank);

            // Assert
            Assert.That(result, Is.EqualTo(expected),
                "Checksum calculation should work correctly with C-QWWT telemetry data");
        }
    }
}
