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
    private const double Tick = 0.5;
    private Entity[][] Matrix;
    private Player Player;
    private const int numberOfGlobs = 10;
    private List<Glob> Globs = new List<Glob>();

    private Timer Timer;
    private static Random Random = new Random();

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
        for (int i = 0; i < numberOfGlobs; i++)
        {
            Globs.Add(AddNewGlob());
        }
        Timer = new Timer(OnTimerElapsed, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(Tick));
    }

    private Glob AddNewGlob()
    {
        Glob glob = null;
        while(glob == null)
        {
            var column = Random.Next(Size);
            var row = Random.Next(Size);
            if(Matrix[row][column] == null)
            {
                glob = new Glob(column, row);
            }
        }
        AddEntityToCell(glob);
        return glob;
    }

    public void ProcessInput(InputAction inputAction)
    {
        switch (inputAction)
        {
            case InputAction.MovePlayerLeft:
            case InputAction.MovePlayerRight:
            case InputAction.MovePlayerUp:
            case InputAction.MovePlayerDown:
            {
                ProcessAction(Player, inputAction.ToAction());
                break;
            }
            case InputAction.None:         
            default:
                return;
        }
    }

    public void ProcessAction(Entity entity, Action action)
    {
        switch (action)
        {
            case Action.MoveLeft:
            case Action.MoveRight:
            case Action.MoveUp:
            case Action.MoveDown:
            {
                int deltaX, deltaY;
                if (TryMove(entity, action, out deltaX, out deltaY))
                {
                    RemoveEntityFromCell(entity);
                    entity.Move(deltaX, deltaY);
                    AddEntityToCell(entity);
                }
                break;
            }
            case Action.None:         
            default:
                return;
        }
    }

    private bool TryMove(Entity entity, Action action, out int deltaX, out int deltaY)
    {
        deltaX = 0;
        deltaY = 0;
        if (action == Action.MoveDown && entity.Row < (Size-1)) deltaY = 1;
        if (action == Action.MoveUp && entity.Row > 0) deltaY = -1;
        if (action == Action.MoveRight && entity.Column < (Size-1)) deltaX = 1;
        if (action == Action.MoveLeft && entity.Column > 0) deltaX = -1;
        return deltaX != 0 || deltaY != 0;
    }

    private void OnTimerElapsed(object arg)
    {
        foreach (var glob in Globs)
        {
            ProcessAction(glob, glob.GetNextAction());
        }
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