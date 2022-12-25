using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Infrastructure;
using ConsoleGame.StateManagment;

namespace ConsoleGame.HUD
{
    internal interface IMenuItem : IRenderable
    {
        public int PositionX { get; init; }
        public int PositionY { get; init; }

        public IEnumerable<IRenderable> Update(GameState context);
    }
}
