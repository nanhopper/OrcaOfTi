using System;
using System.Collections.Generic;
using System.Linq;

public class View
{
    public World World { get; private set;}

    public View()
    {
        InitDisplay();
        World = new World(this);
    }

    public void Run()
    {
        while (true)
        {
            var key = Console.ReadKey(true);
            var action = InputManager.GetInputAction(key);
            if (action == InputAction.Exit) break;
            World.ProcessInput(action);
        }
        Console.SetCursorPosition(0, World.Size + 2);
        Console.WriteLine("Stopped");
    }

    public void DisplayCell(Tuple<int, int, char> cell)
    {
        DisplayCharAtPos(cell.Item3, cell.Item1 + 1, cell.Item2 + 1);            
    }

    private void InitDisplay()
    {
        Console.CursorVisible = false;
        Console.WindowHeight = 40;
        Console.WindowWidth = 40;
        Console.BufferHeight = 40;
        Console.BufferWidth = 40;
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
    
        Console.Clear();
        for (int x = 0; x < World.Size; x++)
        {
            DisplayCharAtPos('-', x+1, 0);
            DisplayCharAtPos('-', x+1, World.Size + 1);
            DisplayCharAtPos('|', 0, x + 1);
            DisplayCharAtPos('|', World.Size + 2, x + 1);
        }
    }

    private void DisplayCharAtPos(char c, int left, int top)
    {
        Console.SetCursorPosition(left, top);
        Console.Write(c);
    }
}