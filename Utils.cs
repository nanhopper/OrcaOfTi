using System;

public static class Extentions
{
    public static Action ToAction(this InputAction inputAction)
    {
        switch (inputAction)
        {
            case InputAction.None: return Action.None;
            case InputAction.MovePlayerLeft: return Action.MoveLeft;
            case InputAction.MovePlayerRight: return Action.MoveRight;
            case InputAction.MovePlayerUp: return Action.MoveUp;
            case InputAction.MovePlayerDown: return Action.MoveDown;
            case InputAction.EatGlob: return Action.EatGlob;
            default:
                throw new NotSupportedException();
        }
    }

    public static Entity LeftNeighbor(this Entity[][] matrix, Entity entity)
    {
        return (entity.Column == 0) ? null : matrix[entity.Row][entity.Column -1];
    }

    public static Entity RightNeighbor(this Entity[][] matrix, Entity entity)
    {
        return (entity.Column == matrix[0].Length-1) ? null : matrix[entity.Row][entity.Column +1];
    }

    public static Entity TopNeighbor(this Entity[][] matrix, Entity entity)
    {
        return (entity.Row == 0) ? null : matrix[entity.Row-1][entity.Column];
    }

    public static Entity BottomNeighbor(this Entity[][] matrix, Entity entity)
    {
        return (entity.Column == matrix.Length -1) ? null : matrix[entity.Row+1][entity.Column];
    }
}