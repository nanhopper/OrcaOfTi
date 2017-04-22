using System;
using System.Linq;

namespace Hello
{
    class Program
    {
        static char[][] Matrix;
        static int PositionX;
        static int PositionY;
        static ConsoleKey[] Arrows = new[] { ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.UpArrow };

        static void Main(string[] args)
        {
            InitMatrix();
            var matrixUpdated = false;
            WriteMatrix();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape) break;
                matrixUpdated = UpdateMatrix(key);
            }
            Console.SetCursorPosition(0, 32);
            Console.WriteLine("Stopped");
            Console.ReadLine();
        }

        private static bool UpdateMatrix(ConsoleKeyInfo key)
        {
            if (Arrows.Contains(key.Key))
            {
                int deltaX, deltaY;
                if (TryMove(key, out deltaX, out deltaY))
                {
                    Matrix[PositionY][PositionX] = ' ';
                    Console.SetCursorPosition(PositionX + 1, PositionY + 1);
                    Console.Write(' ');
                    PositionX += deltaX;
                    PositionY += deltaY;
                    Matrix[PositionY][PositionX] = 'X';
                    Console.SetCursorPosition(PositionX + 1, PositionY + 1);
                    Console.Write('X');
                    return true;
                }
            }
            return false;
        }

        private static bool TryMove(ConsoleKeyInfo key, out int deltaX, out int deltaY)
        {
            deltaX = 0;
            deltaY = 0;
            if (key.Key == ConsoleKey.DownArrow && PositionY < 29) deltaY = 1;
            if (key.Key == ConsoleKey.UpArrow && PositionY > 0) deltaY = -1;
            if (key.Key == ConsoleKey.RightArrow && PositionX < 29) deltaX = 1;
            if (key.Key == ConsoleKey.LeftArrow && PositionX > 0) deltaX = -1;
            return deltaX != 0 || deltaY != 0;
        }

        private static void WriteMatrix()
        {
            Console.Clear();
            Console.Write(' ');
            for (int x = 0; x < 30; x++)
            {
                Console.Write('-');
            }
            Console.Write('\n');
            for (int i = 0; i < 30; i++)
            {
                Console.Write('|');
                for (int j = 0; j < 30; j++)
                {
                    Console.Write(Matrix[i][j]);
                }
                Console.Write("|\n");
            }
            Console.Write(' ');
            for (int x = 0; x < 30; x++)
            {
                Console.Write('-');
            }
            Console.Write('\n');
        }

        private static void InitMatrix()
        {
            Console.CursorVisible = false;
            Console.WindowHeight = 40;
            Console.WindowWidth = 40;
            Console.BufferHeight = 40;
            Console.BufferWidth = 40;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            Matrix = new char[30][];
            for (int i = 0; i < 30; i++)
            {
                Matrix[i] = new char[30];
            }

            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    Matrix[i][j] = ' ';
                }
            }
            PositionX = 15;
            PositionY = 15;
            Matrix[PositionY][PositionX] = 'X';
        }
    }
}
