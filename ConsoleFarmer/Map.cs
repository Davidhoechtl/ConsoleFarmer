
namespace ConsoleGame
{
    using ConsoleGame.Infrastructure;
    using Newtonsoft.Json;
    using System.Drawing;

    internal class Map : IRenderable
    {
        public int Width { get; init; }
        public int Height { get; init; }

        [JsonIgnore]
        public Cell[,] Cells { get; set; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Init()
        {
            Cells = new Cell[Width, Height];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (x == 0 || x == Width - 1 || y == 0 || y == Height - 1)
                    {
                        Cells[x, y] = new Cell(x, y, MapSymbols.Wall);
                    }
                    else
                    {
                        Cells[x, y] = new Cell(x, y, MapSymbols.Empty);
                    }
                }
            }
        }

        public void Render()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Cell current = Cells[x, y];

                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = GetCellColorFromRessourceValue(current);
                    Console.Write(Cells[x, y].Value);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PlaceResscouce(Point pos, int resscourceValue)
        {
            Cell current = this[pos.X, pos.Y];
            current.RessourceValue = resscourceValue;
            current.Value = MapSymbols.Ressource;

            Console.SetCursorPosition(pos.X, pos.Y);
            Console.ForegroundColor = GetCellColorFromRessourceValue(current);
            Console.Write(current.Value);
        }

        public Cell this[int x, int y]
        {
            get => Cells[x, y];
        }

        private ConsoleColor GetCellColorFromRessourceValue(Cell cell)
        {
            switch (cell.RessourceValue)
            {
                case <= 1:
                    return ConsoleColor.White;
                case <= 3:
                    return ConsoleColor.Yellow;
                case <= 5:
                    return ConsoleColor.Green;
                case <= 7:
                    return ConsoleColor.DarkRed;
                case >= 8:
                    return ConsoleColor.Magenta;
            }
        }

        private Map lastUpdate;
    }
}
