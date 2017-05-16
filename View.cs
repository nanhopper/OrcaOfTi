using System;
using System.Collections.Generic;
using System.Linq;

public class View
{
    private readonly World World;
    private readonly object Sync = new object();

    public View()
    {
        World = new World(this);
        InitDisplay();
    }

    public void Run()
    {
        World.Init();
         while (true)
        {
            var key = Console.ReadKey(true);
            var action = InputManager.GetInputAction(key);
            if (action == InputAction.Exit) break;
            World.ProcessInput(action);
        }
        // todo stop timer
        Console.SetCursorPosition(0, World.Size + 2);
        Console.WriteLine("Stopped");
    }

    public void DisplayEntity(Entity entity, bool remove = false)
    {
        DisplayCharAtPos(remove ? ' ' : entity.Char, entity.Column + 1, entity.Row + 1);            
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
        lock (Sync)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(c);
        }
    }

    internal void DisplayTimer(double tick)
    {
        lock (Sync)
        {
            Console.SetCursorPosition(0, World.Size + 2);
            Console.Write("{0:f2}", tick);
        }
    }
}