using System;

public class Coal : Entity
{
    public override char Char { get { return '*';}}

    public override Action GetNextIntendedAction()
    {
        return Action.None;
    }
}