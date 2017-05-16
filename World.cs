using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class World
{
    private readonly View View;
    private double Tick = 0.2;
    private readonly Map Map;
    private Player Player;
    private const int numberOfGlobs = 10;
    private List<Entity> Entities = new List<Entity>();
    private Timer Timer;
    private ActionProcessor ActionProcessor;

    public int Size { get{ return Map.Size;} }

    public World(View view)
    {
        View = view;
        Map = new Map(view);
        Map.TrySpawnAt(Map.Size / 2, Map.Size / 2, out Player);
        for (int i = 0; i < numberOfGlobs; i++)
        {
            Entities.Add(Map.Spawn<Glob>());
        }
        ActionProcessor = new ActionProcessor(this, Map);
    }

    public void Init()
    {
        Timer = new Timer(OnTimerElapsed, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(Tick));
        View.DisplayTimer(Tick);
    }

    public void ProcessInput(InputAction inputAction)
    {
        switch (inputAction)
        {
            case InputAction.SlowDown:
                {
                    Tick += 0.1;
                    Timer.Change(TimeSpan.FromSeconds(Tick), TimeSpan.FromSeconds(Tick));
                    View.DisplayTimer(Tick);
                    break;
                }
            case InputAction.SpeedUp:
                {
                    Tick = Math.Max(0, Tick - 0.1);
                    Timer.Change(TimeSpan.FromSeconds(Tick), TimeSpan.FromSeconds(Tick));
                    View.DisplayTimer(Tick);
                    break;
                }
            default:
                {
                    ActionProcessor.ProcessAction(Player, inputAction.ToAction());
                    break;
                }
        }
    }

    public void Remove(Entity entity)
    {
        Entities.Remove(entity);
    }

    private void OnTimerElapsed(object arg)
    {
        Entities.ForEach(e => ActionProcessor.ProcessAction(e, e.GetNextIntendedAction()));
    }
}