using System;
using System.Collections.Generic;
using System.Threading;

public enum Action
{
    None,
    MoveLeft,
    MoveRight,
    MoveUp,
    MoveDown
}

public class World
{
    private readonly View View;

    public const int Size = 30;
    private Entity[][] Matrix;
    private Player Player;

    private Timer Timer;

    public World(View view)
    {
        View = view;
        Init();
    }

    private void Init()
    {
        Matrix = new Entity[Size][];
        for (int i = 0; i < Size; i++)
        {
            Matrix[i] = new Entity[Size];
        }
        Player = new Player(Size/2, Size/2);
        AddEntityToCell(Player);
        Timer = new Timer(OnTimerElapsed, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1));
    }

    public void ProcessInput(InputAction action)
    {
        switch (action)
        {
            case InputAction.MovePlayerLeft:
            case InputAction.MovePlayerRight:
            case InputAction.MovePlayerUp:
            case InputAction.MovePlayerDown:
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
        if (action == InputAction.MovePlayerDown && Player.Row < (Size-1)) deltaY = 1;
        if (action == InputAction.MovePlayerUp && Player.Row > 0) deltaY = -1;
        if (action == InputAction.MovePlayerRight && Player.Column < (Size-1)) deltaX = 1;
        if (action == InputAction.MovePlayerLeft && Player.Column > 0) deltaX = -1;
        return deltaX != 0 || deltaY != 0;
    }

    private void OnTimerElapsed(object arg)
    {
        // Update entities
    }

    private void AddEntityToCell(Entity entity)
    {
        Matrix[entity.Row][entity.Column] = entity;
        View.DisplayCell(new Tuple<int, int, char>(entity.Column, entity.Row, entity.Char));
    }

    private void RemoveEntityFromCell(Entity entity)
    {
        Matrix[entity.Row][entity.Column] = null;
        View.DisplayCell(new Tuple<int, int, char>(entity.Column, entity.Row, ' '));
    }
}