using System;
using System.Collections.Generic;
using System.Threading;

public class World
{
    private readonly View View;

    public const int Size = 30;
    private char[][] Matrix;
    private Player Player;

    private Timer Timer;

    public World(View view)
    {
        View = view;
        Init();
    }

    private void Init()
    {
        Matrix = new char[Size][];
        for (int i = 0; i < Size; i++)
        {
            Matrix[i] = new char[Size];
            for (int j = 0; j < Size; j++)
            {
                Matrix[i][j] = ' ';
            }
        }
        Player = new Player(Size/2, Size/2);
        AddEntityToCell(Player);
        Timer = new Timer(OnTimerElapsed, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1));
    }

    public void ProcessInput(InputAction action)
    {
        switch (action)
        {
            case InputAction.MoveLeft:
            case InputAction.MoveRight:
            case InputAction.MoveUp:
            case InputAction.MoveDown:
            {
                int deltaX, deltaY;
                if (TryMove(action, out deltaX, out deltaY))
                {
                    RemoveEntityFromCell(Player);
                    Player.Move(deltaX, deltaY);
                    AddEntityToCell(Player);
                }
                break;
            }
            case InputAction.None:         
            default:
                return;
        }
    }

    private bool TryMove(InputAction action, out int deltaX, out int deltaY)
    {
        deltaX = 0;
        deltaY = 0;
        if (action == InputAction.MoveDown && Player.Row < (Size-1)) deltaY = 1;
        if (action == InputAction.MoveUp && Player.Row > 0) deltaY = -1;
        if (action == InputAction.MoveRight && Player.Column < (Size-1)) deltaX = 1;
        if (action == InputAction.MoveLeft && Player.Column > 0) deltaX = -1;
        return deltaX != 0 || deltaY != 0;
    }

    private void OnTimerElapsed(object arg)
    {
        // Update entities
    }

    private void AddEntityToCell(Entity entity)
    {
        UpdateCell(entity.Column, entity.Row, entity.Char);
    }

    private void RemoveEntityFromCell(Entity entity)
    {
        UpdateCell(entity.Column, entity.Row, ' ');
    }

    private void UpdateCell(int column, int row, char c)
    {
        Matrix[row][column] = c;
        View.DisplayCell(new Tuple<int, int, char>(column, row, c));
    }
}