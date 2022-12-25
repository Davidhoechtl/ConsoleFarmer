

namespace ConsoleGame.HUD
{
    internal class EventOutput
    {
        public static int PositionX {get; set;}
        public static int PositionY { get; set;}

        public EventOutput(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public static void AddMessage( string msg)
        {
            messages.Add(msg);
            ClearMessages();
            Render();
        }

        public static void Render()
        {
            Console.SetCursorPosition(PositionX, PositionY);
            Console.WriteLine("Events:");
            int indexLimit = (messages.Count - maxStackSize);
            indexLimit = indexLimit < 0 ? 0 : indexLimit;

            int rowIndex = 1;
            for (int i = messages.Count - 1; i >= indexLimit; i--)
            {
                Console.SetCursorPosition(PositionX + 1, PositionY + rowIndex);

                if (rowIndex == maxStackSize)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                else if (rowIndex >= maxStackSize - 2)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write(messages[i]);
                rowIndex++;
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ClearMessages()
        {
            for (int row = PositionY; row < PositionY + maxStackSize + 1; row++)
            {
                for (int column = PositionX; column < Console.BufferWidth; column++)
                {
                    Console.SetCursorPosition(column, row);
                    Console.Write(' ');
                }
            }
        }

        private static int maxStackSize = 13;
        private static List<string> messages = new();
    }
}
