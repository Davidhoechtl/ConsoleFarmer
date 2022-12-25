using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Infrastructure;
using ConsoleGame.StateManagment;

namespace ConsoleGame.Buildings
{
    internal class Booster : BuildingBase
    {
        public override string Name => "Booster";
        public override BuildingType Type => BuildingType.Booster;

        public int BoostRadius => 1;
        public double BoostMultiplier => 1 + ((double)Level / 40);

        public override bool IsBoostable => false;

        public Booster(int posX, int posY)
            : base(posX, posY)
        {
        }

        public override void Render()
        {
            Console.SetCursorPosition(PositionX, PositionY);

            if (boosterActive)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = GetConsoleColorFromLevel();
            }

            Console.Write(MapSymbols.Booster);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public override IEnumerable<IRenderable> Update(GameState context)
        {
            BuildingBase buildingUnderPlayer = context.player.BuildingUnderPlayer;

            if (buildingUnderPlayer != null && buildingUnderPlayer is not Booster && buildingUnderPlayer.IsBoostable)
            {
                if (!boosterActive)
                {
                    boosterActive = IsBoosterActive(buildingUnderPlayer);
                    return new List<IRenderable>() { this };
                }
                else
                {
                    return new List<IRenderable>();
                }
            }
            else if(boosterActive)
            {
                boosterActive = false;
                return new List<IRenderable>() { this };
            }

            return new List<IRenderable>();
        }

        private bool IsBoosterActive(BuildingBase buildingUnderPlayer)
        {
            int distanceX = buildingUnderPlayer.PositionX - PositionX;
            int distanceY = buildingUnderPlayer.PositionY - PositionY;
            int distanceToBuilding = (int)Math.Sqrt((distanceX * distanceX) + (distanceY * distanceY));
            if (BoostRadius >= distanceToBuilding)
            {
                return true;
            }

            return false;
        }

        private bool boosterActive;
    }
}
