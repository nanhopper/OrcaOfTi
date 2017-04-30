using System;

public abstract class Entity
{
    public int Column { get; protected set;}
    public int Row { get; protected set;}
    public abstract char Char{get;} 

    public Entity(int column, int row)
    {
        Column = column;
        Row = row;
    }

    public void Move(int deltaX, int deltaY)
    {
        Column += deltaX;
        Row += deltaY;
    }

    public abstract Action GetNextIntendedAction();
}