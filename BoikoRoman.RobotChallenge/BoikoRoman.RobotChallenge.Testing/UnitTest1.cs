using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robot.Common;
using System;
using System.Collections.Generic;

namespace BoikoRoman.RobotChallenge.Testing
{
    [TestClass]
    public class UnitTest1
    {
        /// Тест перевіряє, що метод IsFreePosition повертає true для вільної позиції.
        [TestMethod]
        public void TestIsFreePosition_ValidPosition_ReturnsTrue()
        {
            var robots = new List<Robot.Common.Robot>();
            var position = new Position(50, 50);
            var map = new Map();

            bool result = WinStrategy.IsFreePosition(map, robots, position, "Roman Boiko");

            Assert.IsTrue(result);
        }

        /// Тест перевіряє, що метод IsFreePosition повертає false, коли позиція зайнята роботом.
        [TestMethod]
        public void TestIsFreePosition_TakenPosition_ReturnsFalse()
        {
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot { Position = new Position(50, 50), OwnerName = "Roman Boiko" }
            };
            var position = new Position(50, 50);
            var map = new Map();

            bool result = WinStrategy.IsFreePosition(map, robots, position, "Roman Boiko");

            Assert.IsFalse(result);
        }

        /// Тест перевіряє, що метод CollectEnergyNearby повертає 0, якщо поруч немає енергетичних станцій.
        [TestMethod]
        public void TestCollectEnergyNearby_NoStations_ReturnsZero()
        {
            var map = new Map();
            var position = new Position(50, 50);

            int energy = WinStrategy.CollectEnergyNearby(map, position, 2);

            Assert.AreEqual(0, energy);
        }

        /// Тест перевіряє, що метод CollectEnergyNearby збирає максимум 40 одиниць енергії з найближчої станції.
        [TestMethod]
        public void TestCollectEnergyNearby_WithStations_ReturnsEnergy()
        {
            var map = new Map();
            map.Stations.Add(new EnergyStation { Position = new Position(51, 51), Energy = 60 });
            var position = new Position(50, 50);

            int energy = WinStrategy.CollectEnergyNearby(map, position, 2);

            Assert.AreEqual(40, energy); 
        }

        /// Тест перевіряє, що метод GetRandomPosition повертає нову позицію в межах карти (0-99).
        [TestMethod]
        public void TestGetRandomPosition_WithinBounds()
        {
            var current = new Position(50, 50);
            var map = new Map();

            Position newPos = WinStrategy.GetRandomPosition(current, map);

            Assert.IsTrue(newPos.X >= 0 && newPos.X < 100 && newPos.Y >= 0 && newPos.Y < 100);
        }


        /// Тест перевіряє, що метод AnalyzeBestPositionConsideringStations повертає список позицій біля станцій.
        [TestMethod]
        public void TestAnalyzeBestPositionConsideringStations_ValidPositions()
        {
            var map = new Map();
            map.Stations.Add(new EnergyStation { Position = new Position(60, 60), Energy = 100 });
            var robotPosition = new Position(50, 50);

            List<KeyValuePair<int, Position>> positions = WinStrategy.AnalyzeBestPositionConsideringStations(map, robotPosition, 5);

            Assert.IsTrue(positions.Count > 0);
        }

        /// Тест перевіряє, що метод CountClostBots правильно рахує кількість роботів поруч.
        [TestMethod]
        public void TestCountClostBots_CorrectCount()
        {
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot { Position = new Position(50, 50) },
                new Robot.Common.Robot { Position = new Position(51, 50) },
                new Robot.Common.Robot { Position = new Position(55, 50) }
            };
            var position = new Position(50, 50);

            int count = WinStrategy.CountClostBots(robots, position);

            Assert.AreEqual(2, count); // Два роботи поруч
        }

        /// Тест перевіряє, що метод CountRobotsByAuthor повертає 0, якщо роботів немає.
        [TestMethod]
        public void TestCountRobotsByAuthor_NoRobots_ReturnsZero()
        {
            var robots = new List<Robot.Common.Robot>();

            int count = WinStrategy.CountRobotsByAuthor(robots, "Roman Boiko");

            Assert.AreEqual(0, count);
        }

        /// Тест перевіряє, що метод CountRobotsByAuthor повертає правильну кількість роботів одного автора.
        [TestMethod]
        public void TestCountRobotsByAuthor_WithRobots_ReturnsCorrectCount()
        {
            var robots = new List<Robot.Common.Robot>
            {
                new Robot.Common.Robot { OwnerName = "Roman Boiko" },
                new Robot.Common.Robot { OwnerName = "Roman Boiko" },
                new Robot.Common.Robot { OwnerName = "Other Author" }
            };

            int count = WinStrategy.CountRobotsByAuthor(robots, "Roman Boiko");

            Assert.AreEqual(2, count);
        }


        /// Тест перевіряє, що метод NearbyRobots повертає true, якщо робот знаходиться в радіусі 1 клітинки.
        [TestMethod]
        public void TestNearbyRobots_WithinRadius_ReturnsTrue()
        {
            var center = new Position(50, 50);
            var point = new Position(51, 50);

            bool result = WinStrategy.NearbyRobots(center, point, 1);

            Assert.IsTrue(result);
        }

        /// Тест перевіряє, що метод AnalyzeBestPositionConsideringStations враховує енергетичні станції з різним рівнем енергії.
        [TestMethod]
        public void TestAnalyzeBestPositionConsideringStations_ConsidersEnergyLevel()
        {
            var map = new Map();
            map.Stations.Add(new EnergyStation { Position = new Position(60, 60), Energy = 200 });
            map.Stations.Add(new EnergyStation { Position = new Position(61, 61), Energy = 100 });
            var robotPosition = new Position(50, 50);

            List<KeyValuePair<int, Position>> positions = WinStrategy.AnalyzeBestPositionConsideringStations(map, robotPosition, 10);

            // Перевірка, що позиція біля станції з більшою енергією є більш привабливою
            Assert.IsTrue(positions[0].Key > positions[1].Key);
        }
        /// Тест перевіряє, що метод NearbyRobots повертає false, якщо робот знаходиться за межами радіусу 1 клітинки.
        [TestMethod]
        public void TestNearbyRobots_OutsideRadius_ReturnsFalse()
        {
            var center = new Position(50, 50);
            var point = new Position(53, 50);

            bool result = WinStrategy.NearbyRobots(center, point, 1);

            Assert.IsFalse(result);
        }
    }
}
