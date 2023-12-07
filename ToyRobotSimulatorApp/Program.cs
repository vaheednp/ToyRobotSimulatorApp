using System;

namespace ToyRobotSimulator
{
    public enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    public class ToyRobot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Facing { get; set; }

        public bool IsPlaced { get; set; }
    }

    public class ToyRobotSimulator
    {
        private const int TableSize = 5;
        private ToyRobot robot;
        public ToyRobotSimulator()
        {
            robot = new ToyRobot();
        }

        public void ExecuteCommand(string command)
        {
            string[] parts = command.Split(' ');
            string action = parts[0].ToUpper();

            switch (action)
            {
                case "PLACE":
                    PlaceRobot(parts);
                    break;
                case "MOVE":
                    MoveRobot();
                    break;
                case "LEFT":
                    TurnLeft();
                    break;
                case "RIGHT":
                    TurnRight();
                    break;
                case "REPORT":
                    Report();
                    break;
                case "EXIT":
                    Console.WriteLine("Exiting the simulator.");
                    break;
                default:
                    Console.WriteLine("Invalid command. Try again.");
                    break;
            }
        }

        private void PlaceRobot(string[] parts)
        {
            if (parts.Length == 2)
            {
                string[] coordinates = parts[1].Split(',');
                if (coordinates.Length == 3 &&
                    int.TryParse(coordinates[0], out int x) &&
                    int.TryParse(coordinates[1], out int y) &&
                    Enum.TryParse(coordinates[2], out Direction facing) &&
                    x >= 0 && x < TableSize &&
                    y >= 0 && y < TableSize)
                {
                    robot.X = x;
                    robot.Y = y;
                    robot.Facing = facing;
                    robot.IsPlaced = true;
                }
                else
                {
                    Console.WriteLine("Invalid PLACE command. Try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid PLACE command. Try again.");
            }
        }

        private void MoveRobot()
        {
            if (robot.IsPlaced)
            {
                int newX = robot.X;
                int newY = robot.Y;

                switch (robot.Facing)
                {
                    case Direction.NORTH:
                        newY++;
                        break;
                    case Direction.EAST:
                        newX++;
                        break;
                    case Direction.SOUTH:
                        newY--;
                        break;
                    case Direction.WEST:
                        newX--;
                        break;
                }

                if (IsValidPosition(newX, newY))
                {
                    robot.X = newX;
                    robot.Y = newY;
                }
            }
        }

        private void TurnLeft()
        {
            if (robot.IsPlaced)
            {
                robot.Facing = (Direction)(((int)robot.Facing + 3) % 4);
            }
        }

        private void TurnRight()
        {
            if (robot.IsPlaced)
            {
                robot.Facing = (Direction)(((int)robot.Facing + 1) % 4);
            }
        }

        private void Report()
        {
            if (robot.IsPlaced)
            {
                Console.WriteLine($"Current position: {robot.X},{robot.Y},{robot.Facing}");
            }
        }

        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < TableSize && y >= 0 && y < TableSize;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            ToyRobotSimulator simulator = new ToyRobotSimulator();
            string command;

            do
            {
                Console.Write("Enter command: ");
                command = Console.ReadLine();
                simulator.ExecuteCommand(command);
            } while (!string.Equals(command, "EXIT", StringComparison.OrdinalIgnoreCase));
        }
    }
}
