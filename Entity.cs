public abstract class Entity
{
    public int Column { get; protected set;}
    public int Row { get; protected set;}
    public abstract char Char{get;} 

    public void MoveTo(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public void Move(int deltaX, int deltaY)
    {
        Column += deltaX;
        Row += deltaY;
    }

    public abstract Action GetNextIntendedAction();
}