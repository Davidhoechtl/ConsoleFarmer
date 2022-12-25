using ConsoleGame.Buildings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Infrastructure
{
    internal static class LocationHelper
    {
        public static int GetDistanceToBuilding(Point point, BuildingBase building)
        {
            int distanceX = point.X - building.PositionX;
            int distanceY = point.Y - building.PositionY;
            int distanceToFactory = (int)Math.Sqrt((distanceX * distanceX) + (distanceY * distanceY));
            return distanceToFactory;
        }
    }
}
