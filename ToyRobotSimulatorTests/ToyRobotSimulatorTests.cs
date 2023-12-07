using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using ToyRobotSimulator;

namespace ToyRobotSimulatorTests
{
    [TestClass]
    public class ToyRobotSimulatorTests
    {
        [TestMethod]
        public void TestPlaceCommand()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                string commands = "PLACE 1,2,EAST\nREPORT\nEXIT";
                using (StringReader sr = new StringReader(commands))
                {
                    Console.SetIn(sr);

                    // Run the application
                    Program.Main(null);

                    string expectedOutput = "1,2,EAST";
                    string actualOutput = GetCurrentPosition(sw.ToString().Trim());

                Assert.AreEqual(expectedOutput, actualOutput);
                }
            }
        }

        [TestMethod]
        public void TestMoveCommand()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                string commands = "PLACE 1,1,EAST\nMOVE\nREPORT\nEXIT";
                using (StringReader sr = new StringReader(commands))
                {
                    Console.SetIn(sr);

                    // Run the application
                    Program.Main(null);

                    string expectedOutput = "2,1,EAST";
                    string actualOutput = GetCurrentPosition(sw.ToString().Trim());

                Assert.AreEqual(expectedOutput, actualOutput);
                }
            }
        }

        [TestMethod]
        public void TestLeftCommand()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                string commands = "PLACE 1,1,NORTH\nLEFT\nREPORT\nEXIT";
                using (StringReader sr = new StringReader(commands))
                {
                    Console.SetIn(sr);

                    // Run the application
                    Program.Main(null);

                    string expectedOutput = "1,1,WEST";
                    string actualOutput = GetCurrentPosition(sw.ToString().Trim());

                Assert.AreEqual(expectedOutput, actualOutput);
                }
            }
        }

        [TestMethod]
        public void TestRightCommand()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                string commands = "PLACE 1,1,SOUTH\nRIGHT\nREPORT\nEXIT";
                using (StringReader sr = new StringReader(commands))
                {
                    Console.SetIn(sr);

                    // Run the application
                    Program.Main(null);

                    string expectedOutput = "1,1,WEST";
                    string actualOutput = GetCurrentPosition(sw.ToString().Trim());

                Assert.AreEqual(expectedOutput, actualOutput);
                }
            }
        }

        [TestMethod]
        public void TestReportCommand()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                string commands = "PLACE 3,4,NORTH\nREPORT\nEXIT";
                using (StringReader sr = new StringReader(commands))
                {
                    Console.SetIn(sr);

                    // Run the application
                    Program.Main(null);

                    string expectedOutput = "3,4,NORTH";
                    string actualOutput = GetCurrentPosition(sw.ToString().Trim());

                Assert.AreEqual(expectedOutput, actualOutput);
                }
            }
        }

        [TestMethod]
        public void TestInvalidCommands()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                string commands = "INVALID\nPLACE 1,1,EAST\nMOVE\nINVALID\nREPORT\nEXIT";
                using (StringReader sr = new StringReader(commands))
                {
                    Console.SetIn(sr);

                    // Run the application
                    Program.Main(null);

                    string expectedOutput = "1,1,EAST";
                    string actualOutput = GetCurrentPosition(sw.ToString().Trim());

                Assert.AreEqual(expectedOutput, actualOutput);
                }
            }
        }
        static string GetCurrentPosition(string consoleOutput)
        {
            int startIndex = consoleOutput.IndexOf("Current position:");

            if (startIndex != -1)
            {
                // Find the end of the line starting from "Current position:"
                int endIndex = consoleOutput.IndexOf(Environment.NewLine, startIndex);
                if (endIndex == -1)
                {
                    // If a new line is not found, use the end of the string
                    endIndex = consoleOutput.Length;
                }

                // Extract the substring containing "1,2,EAST"
                string currentPositionLine = consoleOutput.Substring(startIndex, endIndex - startIndex).Trim();

                // Extract the position information from the line
                int positionStartIndex = currentPositionLine.IndexOf(":") + 1;
                return currentPositionLine.Substring(positionStartIndex).Trim();
            }

            return null; // or throw an exception, handle accordingly
        }
    }
}