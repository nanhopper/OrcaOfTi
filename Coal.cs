using System;

public class Coal : Entity
{
    public Coal(int column, int row) : base(column, row)
    {
    }

    public override char Char { get { return '*';}}

    public override Action GetNextIntendedAction()
    {
        return Action.None;
    }
}