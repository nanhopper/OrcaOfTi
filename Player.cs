using System;

public class Player : Entity
{
    public override char Char { get { return 'X';}}

    public override Action GetNextIntendedAction()
    {
        return Action.None;
    }
}