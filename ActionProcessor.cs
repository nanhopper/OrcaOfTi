

public class ActionProcessor
{
    private readonly World World;
    private readonly Map Map;

    public ActionProcessor(World world, Map map)
    {
        Map = map;
        World = world;
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
                Move(entity, action);
                break;
            }
            case Action.EatGlob:
            {
                Glob glob;
                if (Map.TryGetNeighbor(entity, out glob))
                {
                    Map.Remove(glob);
                    World.Remove(glob);
                }
                break;
            }
          
            case Action.None: 
            default:
                return;        
        }
    }

    private void Move(Entity entity, Action action)
    {
        var deltaX = 0;
        var deltaY = 0;
        if (action == Action.MoveDown && entity.Row < (Map.Size-1)) deltaY = 1;
        if (action == Action.MoveUp && entity.Row > 0) deltaY = -1;
        if (action == Action.MoveRight && entity.Column < (Map.Size-1)) deltaX = 1;
        if (action == Action.MoveLeft && entity.Column > 0) deltaX = -1;
        
        if(Map.IsEmpty(entity.Row + deltaY, entity.Column + deltaX) && (deltaX != 0 || deltaY != 0))
        {
            Move(entity, deltaX, deltaY);
        }
    }

    private void Move(Entity entity, int deltaX, int deltaY)
    {
        Map.Remove(entity);
        entity.Move(deltaX, deltaY);
        Map.Add(entity);
    }
}