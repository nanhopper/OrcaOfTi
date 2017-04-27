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

    public Glob(int column, int row) : base(column, row)
    {
    }

    public override char Char { get { return 'O';}}

    public override Action GetNextAction()
    {
        return Actions[Random.Next(4)];
    }
}