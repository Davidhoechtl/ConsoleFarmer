using ConsoleGame.HUD;
using ConsoleGame.Infrastructure;
using ConsoleGame.StateManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Buildings
{
    internal class Factory : BuildingBase, IHasRessourceGainOverTime
    {
        public override string Name => "Factory";
        public override BuildingType Type => BuildingType.Factory;

        public int CurrentGainPerTick { get; private set; }

        public DateTime LastTick { get; private set; } = DateTime.Now;
        public int TimeToNextTickInSeconds => 10/1;
        public int ressourceCountOnTick => 1 + Level;

        public override bool IsBoostable => true;

        public Factory(int posX, int posY)
            : base(posX, posY)
        {
        }

        public override void Render()
        {
            Console.SetCursorPosition(PositionX, PositionY);

            Console.ForegroundColor = GetConsoleColorFromLevel();
            Console.Write(MapSymbols.Factory);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public override IEnumerable<IRenderable> Update(GameState context)
        {
            if(DateTime.Now > LastTick + new TimeSpan(0, 0, TimeToNextTickInSeconds))
            {
                IEnumerable<Booster> boostersInReach = GetBoostersInRange(
                    context.buildings.Where(b => b is Booster).Select(b=> b as Booster)
                );

                int gain = ressourceCountOnTick;
                foreach(Booster booster in boostersInReach)
                {
                    gain += (int)(gain * booster.BoostMultiplier);
                }

                CurrentGainPerTick = gain;
                context.player.RessourceCount += CurrentGainPerTick;
                EventOutput.AddMessage($"Factory produced {CurrentGainPerTick} Ressources!");

                LastTick = DateTime.Now;
                return new List<IRenderable>() { this, context.GetMenuItemByType<RessourceCounter>(), context.GetMenuItemByType<RessourceGainViewer>() };
            }

            return new List<IRenderable>();
        }

        private IEnumerable<Booster> GetBoostersInRange(IEnumerable<Booster> allBoosters)
        {
            foreach(Booster booster in allBoosters)
            {
                int distanceX = booster.PositionX - PositionX;
                int distanceY = booster.PositionY - PositionY;
                int distanceToFactory = (int)Math.Sqrt((distanceX*distanceX)+(distanceY*distanceY));
                if(booster.BoostRadius >= distanceToFactory)
                {
                    yield return booster;
                }
            }
        }
    }
}
