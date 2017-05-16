using System;

public class Glob : Entity
{
    private static readonly Random Random = new Random();
    private static Action[] Actions = new []{
        Action.MoveLeft,
        Action.MoveRight,
        Action.MoveUp,
        Action.MoveDown
    };

    public override char Char { get { return 'O';}}

    public override Action GetNextIntendedAction()
    {
        return Actions[Random.Next(4)];
    }
}