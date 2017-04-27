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
            default:
                throw new NotSupportedException();
        }
    }
}