using System;

public class Player : Entity
{
    public Player(int column, int row) : base(column, row)
    {
    }

    public override char Char { get { return 'X';}}
}