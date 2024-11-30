using Robot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoikoRoman.RobotChallenge
{
    public class BoikoRomanAlgorithm : IRobotAlgorithm
    {
        private static int round;
        private const int MaxRobots = 100;
        private const int MinEnergyForMove = 40;

        public string Author => "Roman Boiko";

        public int Round { get; set; }

        public BoikoRomanAlgorithm() => Logger.OnLogRound += new LogRoundEventHandler(this.LogRound);

        private void LogRound(object sender, LogRoundEventArgs e) => ++this.Round;

        public Position FindNearestFreeStation(Robot.Common.Robot movingRobot, Map map, IList<Robot.Common.Robot> robots)
        {
            EnergyStation energyStation = (EnergyStation)null;
            int num = int.MaxValue;
            foreach (EnergyStation station in (IEnumerable<EnergyStation>)map.Stations)
            {
                if (this.IsStationFree(station, movingRobot, robots))
                {
                    int distance = DistanceHelper.CalculateDistance(station.Position, movingRobot.Position);
                    if (distance < num)
                    {
                        num = distance;
                        energyStation = station;
                    }
                }
            }
            return energyStation == null ? (Position)null : energyStation.Position;
        }
        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            if (BoikoRomanAlgorithm.round == 51)
                return (RobotCommand)new CollectEnergyCommand();
            Robot.Common.Robot robot = robots[robotToMoveIndex];
            if (WinStrategy.CountRobotsByAuthor(robots, this.Author) < 100 && robot.Energy > 250)
                return (RobotCommand)new CreateNewRobotCommand();
            IList<EnergyStation> list = (IList<EnergyStation>)map.GetNearbyResources(robot.Position, 2).OfType<EnergyStation>().ToList<EnergyStation>();
            if (list.Count > 0)
            {
                foreach (EnergyStation energyStation in (IEnumerable<EnergyStation>)list)
                {
                    if (energyStation.Energy > 40)
                        return (RobotCommand)new CollectEnergyCommand();
                }
            }
            foreach (KeyValuePair<int, Position> consideringStation in WinStrategy.AnalyzeBestPositionConsideringStations(map, robot.Position, 30))
            {
                if (WinStrategy.IsFreePosition(map, robots, consideringStation.Value, this.Author) && robot.Energy > WinStrategy.ComputeDistance(robot.Position, consideringStation.Value) && robot.Energy > 40)
                    return (RobotCommand)new MoveCommand()
                    {
                        NewPosition = consideringStation.Value
                    };
            }
            return (RobotCommand)new CollectEnergyCommand();
        }

        public bool IsStationFree(EnergyStation station, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            return this.IsCellFree(station.Position, movingRobot, robots);
        }

        public bool IsCellFree(Position cell, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            foreach (Robot.Common.Robot robot in (IEnumerable<Robot.Common.Robot>)robots)
            {
                if (robot != movingRobot && robot.Position == cell)
                    return false;
            }
            return true;
        }

        public static double CalculateStationScore(EnergyStation station, Position robotPosition)
        {
            double num = Math.Sqrt((double)WinStrategy.ComputeDistance(robotPosition, station.Position));
            return (double)Math.Min(station.Energy, 20000) / 20000.0 * (1.0 / (1.0 + num));
        }

    }
}


