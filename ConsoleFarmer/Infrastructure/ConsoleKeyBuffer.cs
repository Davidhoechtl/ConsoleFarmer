using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Infrastructure
{
    internal class ConsoleKeyBuffer
    {
        public static bool KeyAvailable => KeyBuffer.Count > 0;
        private static Stack<ConsoleKeyInfo> KeyBuffer = new();

        public static void Flush()
        {
            KeyBuffer = new Stack<ConsoleKeyInfo>();
        }

        public static ConsoleKeyInfo GetNext(bool delete)
        {
            if (delete)
            {
                return KeyBuffer.Pop();
            }
            else
            {
                return KeyBuffer.Peek();
            }
        }

        public void ReadConsoleBuffer()
        {
            while (Console.KeyAvailable)
            {
                KeyBuffer.Push(Console.ReadKey(true));
            }
        }
    }
}
