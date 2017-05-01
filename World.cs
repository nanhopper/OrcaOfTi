using System;
using System.Collections.Generic;
using System.Threading;

public class World
{
    private readonly View View;
    public const int Size = 30;
    private double Tick = 0.2;
    private Entity[][] Matrix;
    private Player Player;
    private const int numberOfGlobs = 10;
    private List<Entity> Entities = new List<Entity>();
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
            Entities.Add(AddNewGlob());
        }
        Timer = new Timer(OnTimerElapsed, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(Tick));
        View.DisplayTimer(Tick);
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
            case InputAction.EatGlob:
            {
                TryProcessAction(Player, inputAction.ToAction());
                break;
            }
            case InputAction.SlowDown: 
            {
                Tick += 0.1;
                Timer.Change(TimeSpan.FromSeconds(Tick), TimeSpan.FromSeconds(Tick));
                View.DisplayTimer(Tick);
                break;
            }
            case InputAction.SpeedUp: 
            {
                Tick = Math.Max(0, Tick-0.1);
                Timer.Change(TimeSpan.FromSeconds(Tick), TimeSpan.FromSeconds(Tick));
                View.DisplayTimer(Tick);
                break;
            }
            case InputAction.None:         
            default:
                return;
        }
    }

    public bool TryProcessAction(Entity entity, Action action)
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
                    return true;
                }
                return false;
            }
            case Action.EatGlob:
            {
                Glob glob;
                if (TryFindGlob(out glob))
                {
                    RemoveEntityFromCell(glob);
                    Entities.Remove(glob);
                    return true;
                }
                return false;
            }
          
            case Action.None:         
            default:
                return true;
        }
    }

    private bool TryFindGlob(out Glob glob)
    {
        glob = Matrix.LeftNeighbor(Player) as Glob
            ?? Matrix.RightNeighbor(Player) as Glob
            ?? Matrix.TopNeighbor(Player) as Glob
            ?? Matrix.BottomNeighbor(Player) as Glob;
        return glob != null;
    }

    private bool TryMove(Entity entity, Action action, out int deltaX, out int deltaY)
    {
        deltaX = 0;
        deltaY = 0;
        if (action == Action.MoveDown && entity.Row < (Size-1)) deltaY = 1;
        if (action == Action.MoveUp && entity.Row > 0) deltaY = -1;
        if (action == Action.MoveRight && entity.Column < (Size-1)) deltaX = 1;
        if (action == Action.MoveLeft && entity.Column > 0) deltaX = -1;
        return Matrix[entity.Row + deltaY][entity.Column + deltaX] == null && (deltaX != 0 || deltaY != 0);
    }

    private void OnTimerElapsed(object arg)
    {
        Entities.ForEach(e => TryProcessAction(e, e.GetNextIntendedAction()));
    }

    private void AddEntityToCell(Entity entity)
    {
        Matrix[entity.Row][entity.Column] = entity;
        View.DisplayEntity(entity);
    }

    private void RemoveEntityFromCell(Entity entity)
    {
        Matrix[entity.Row][entity.Column] = null;
        View.DisplayEntity(entity, true);
    }
}