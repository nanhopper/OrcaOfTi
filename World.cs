using System;
using System.Collections.Generic;

public class World
{
    private short PositionX;
    private short PositionY;

    public World()
    {
        InitMatrix();
    }

    public const short Size = 30;

    public char[][] Matrix { get; private set;}

    private void InitMatrix()
    {     
        Matrix = new char[Size][];
        for (short i = 0; i < Size; i++)
        {
            Matrix[i] = new char[Size];
        }

        for (short i = 0; i < Size; i++)
        {
            for (short j = 0; j < Size; j++)
            {
                Matrix[i][j] = ' ';
            }
        }
        PositionX = 15;
        PositionY = 15;
        Matrix[PositionY][PositionX] = 'X';
    }

     public IList<Tuple<short, short, char>> UpdateMatrix(ConsoleKeyInfo key)
        {
            var updates = new List<Tuple<short, short, char>>();
            short deltaX, deltaY;
            if (TryMove(key, out deltaX, out deltaY))
            {
                updates.Add(new Tuple<short, short, char>(PositionX, PositionY, ' '));
                Matrix[PositionY][PositionX] = ' ';
                PositionX += deltaX;
                PositionY += deltaY;
                Matrix[PositionY][PositionX] = 'X';
                updates.Add(new Tuple<short, short, char>(PositionX, PositionY, 'X'));
            }
            return updates;
        }

        private bool TryMove(ConsoleKeyInfo key, out short deltaX, out short deltaY)
        {
            deltaX = 0;
            deltaY = 0;
            if (key.Key == ConsoleKey.DownArrow && PositionY < 29) deltaY = 1;
            if (key.Key == ConsoleKey.UpArrow && PositionY > 0) deltaY = -1;
            if (key.Key == ConsoleKey.RightArrow && PositionX < 29) deltaX = 1;
            if (key.Key == ConsoleKey.LeftArrow && PositionX > 0) deltaX = -1;
            return deltaX != 0 || deltaY != 0;
        }

}