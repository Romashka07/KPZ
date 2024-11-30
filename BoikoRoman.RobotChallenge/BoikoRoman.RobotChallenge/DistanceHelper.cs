using Robot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoikoRoman.RobotChallenge
{
    public class DistanceHelper
    {
        public static int CalculateDistance(Position a, Position b)
        {
            return (int)(Math.Pow((double)(a.X - b.X), 2.0) + Math.Pow((double)(a.Y - b.Y), 2.0));
        }
    }
}
