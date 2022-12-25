namespace ConsoleGame
{
    internal class Cell
    {
        /// <summary>
        /// Position on the X-Achsis
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Position on the Y-Achsis
        /// </summary>
        public int Y { get; private set; }

        public char Value { get; set; }

        public int RessourceValue { get; set; }

        public Cell(int x, int y, char value)
        {
            X = x;
            Y = y;
            Value = value;
        }
    }
}
