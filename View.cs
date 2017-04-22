using System;
using System.Collections.Generic;
using System.Linq;

public class View
{
    static ConsoleKey[] Arrows = new[] { ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.UpArrow };

    public World World { get; private set;}

    public View()
    {
        Console.CursorVisible = false;
        Console.WindowHeight = 40;
        Console.WindowWidth = 40;
        Console.BufferHeight = 40;
        Console.BufferWidth = 40;
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.DarkBlue;

        World = new World();
        DisplayMatrix(World.Matrix);
    }

    public void Run()
    {
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape) break;
            if (Arrows.Contains(key.Key))
            {       
                DisplayUpdates(World.UpdateMatrix(key));
            }
        }
        Console.SetCursorPosition(0, World.Size + 2);
        Console.WriteLine("Stopped");
        Console.ReadLine();
    }

    private void DisplayMatrix(char[][] matrix)
    {
        Console.Clear();
        Console.Write(' ');
        for (short x = 0; x < matrix.Length; x++)
        {
            Console.Write('-');
        }
        Console.Write('\n');
        for (short i = 0; i < matrix.Length; i++)
        {
            Console.Write('|');
            for (short j = 0; j < matrix.Length; j++)
            {
                Console.Write(matrix[i][j]);
            }
            Console.Write("|\n");
        }
        Console.Write(' ');
        for (short x = 0; x < matrix.Length; x++)
        {
            Console.Write('-');
        }
        Console.Write('\n');
    }

    private void DisplayUpdates(IList<Tuple<short, short, char>> updates)
    {
        foreach (var update in updates)
        {
            DisplayCharOfCell(update.Item3, update.Item1, update.Item2);            
        }
    }

    private void DisplayCharOfCell(char c, short column, short row)
    {
        Console.SetCursorPosition(column + 1, row + 1);
        Console.Write(c);
    }
}