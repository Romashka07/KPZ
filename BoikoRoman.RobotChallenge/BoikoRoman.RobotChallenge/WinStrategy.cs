using Robot.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BoikoRoman.RobotChallenge
{
    public class WinStrategy
    {

        public const int DistanceToCollect = 2;
        public const int MaxEnergyCollection = 40;
        public const int MaxEnergyStation = 20000;
        public const int NewRobotEnergyCost = 100;

        public static bool IsFreePosition(
          Map map,
          IList<Robot.Common.Robot> robots,
          Position position,
          string author)
        {
            return position.X < 100 && position.X >= 0 && position.Y < 100 && position.Y >= 0 && !robots.Any<Robot.Common.Robot>((Func<Robot.Common.Robot, bool>)(robot => robot.Position.Equals(position) && robot.OwnerName == author));
        }

        public static int CollectEnergyNearby(Map map, Position position, int radius)
        {
            return map.GetNearbyResources(position, radius).OfType<EnergyStation>().Sum<EnergyStation>((Func<EnergyStation, int>)(s => Math.Min(s.Energy, 40)));
        }

        public static Position GetRandomPosition(Position current, Map map)
        {
            Random random = new Random();
            int num1 = random.Next(-2, 3);
            int num2 = random.Next(-2, 3);
            return new Position(Math.Max(0, Math.Min(99, current.X + num1)), Math.Max(0, Math.Min(99, current.Y + num2)));
        }


        public static List<KeyValuePair<int, Position>> AnalyzeBestPositionConsideringStations(
          Map map,
          Position robotsPosition,
          int radius)
        {
            int length = 2 * radius + 1;
            int[,] numArray1 = new int[length, length];
            int[,] numArray2 = new int[length, length];
            int robotsLowestY = robotsPosition.Y - radius;
            int robotsLowestX = robotsPosition.X - radius;
            foreach (EnergyStation station in (IEnumerable<EnergyStation>)map.Stations)
            {
                if (WinStrategy.IsNearbyStation(station, length, robotsLowestY, robotsLowestX))
                {
                    int stationsLowestX = station.Position.X - robotsLowestX;
                    int stationsLowestY = station.Position.Y - robotsLowestY;
                    int num = Math.Min(station.Energy, 40);
                    for (int i = -2; i <= 2; ++i)
                    {
                        for (int j = -2; j <= 2; ++j)
                        {
                            if (WinStrategy.IsCollectRangeInRadius(length, stationsLowestX, stationsLowestY, i, j))
                            {
                                numArray1[stationsLowestY + i, stationsLowestX + j] += num;
                                ++numArray2[stationsLowestY + i, stationsLowestX + j];
                            }
                        }
                    }
                }
            }
            List<KeyValuePair<int, Position>> consideringStations = new List<KeyValuePair<int, Position>>();
            for (int index1 = 0; index1 < length; ++index1)
            {
                for (int index2 = 0; index2 < length; ++index2)
                {
                    Position distant = new Position(robotsLowestX + index2, robotsLowestY + index1);
                    int num = WinStrategy.ComputeDistance(robotsPosition, distant);
                    int key = numArray1[index1, index2] * numArray2[index1, index2] - num;
                    consideringStations.Add(new KeyValuePair<int, Position>(key, distant));
                }
            }
            consideringStations.Sort((Comparison<KeyValuePair<int, Position>>)((pair1, pair2) => pair2.Key.CompareTo(pair1.Key)));
            return consideringStations;
        }

        private static bool IsNearbyStation(
          EnergyStation station,
          int length,
          int robotsLowestY,
          int robotsLowestX)
        {
            return station.Position.X >= robotsLowestX - 2 && station.Position.X < robotsLowestX + length + 2 && station.Position.Y >= robotsLowestY - 2 && station.Position.Y < robotsLowestY + length + 2;
        }

        private static bool IsCollectRangeInRadius(
          int length,
          int stationsLowestX,
          int stationsLowestY,
          int i,
          int j)
        {
            return stationsLowestX + j >= 0 && stationsLowestX + j < length && stationsLowestY + i >= 0 && stationsLowestY + i < length;
        }
        public static int ComputeDistance(Position center, Position distant)
        {
            return (center.X - distant.X) * (center.X - distant.X) + (center.Y - distant.Y) * (center.Y - distant.Y);
        }

        public static int CountRobotsByAuthor(IList<Robot.Common.Robot> robots, string author)
        {
            return robots.Count<Robot.Common.Robot>((Func<Robot.Common.Robot, bool>)(robot => robot.OwnerName == author));
        }

        public static int CountClostBots(IList<Robot.Common.Robot> robots, Position position)
        {
            return robots.Count<Robot.Common.Robot>((Func<Robot.Common.Robot, bool>)(robot => WinStrategy.NearbyRobots(position, robot.Position, 1)));
        }

        public static bool NearbyRobots(Position center, Position point, int radius)
        {
            return Math.Abs(center.X - point.X) <= radius && Math.Abs(center.Y - point.Y) <= radius;
        }

    }
}