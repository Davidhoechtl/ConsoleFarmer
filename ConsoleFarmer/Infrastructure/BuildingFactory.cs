using ConsoleGame.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Infrastructure
{
    internal class BuildingFactory
    {
        public BuildingBase CreateBuilding(BuildingType type, int posX, int postY)
        {
            switch (type)
            {
                case BuildingType.Factory:
                    return new Factory(posX, postY);
                case BuildingType.Booster:
                    return new Booster(posX, postY);
                case BuildingType.Spawner:
                    return new RessourceSpawner(posX, postY);
                case BuildingType.Bank:
                    return new Bank(posX, postY);
                default:
                    throw new InvalidOperationException($"No Building with type {Enum.GetName(type)} found.");
            }
        }
    }
}
