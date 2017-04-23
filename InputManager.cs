using System;
using System.Collections.Generic;

public enum InputAction
{
    None,
    MoveLeft,
    MoveRight,
    MoveUp,
    MoveDown,
    Exit
}

public static class InputManager
{
    private static readonly Dictionary<ConsoleKey, InputAction> Actions = new Dictionary<ConsoleKey, InputAction>()
    {
        {ConsoleKey.LeftArrow , InputAction.MoveLeft},
        {ConsoleKey.RightArrow , InputAction.MoveRight},
        {ConsoleKey.UpArrow , InputAction.MoveUp},
        {ConsoleKey.DownArrow , InputAction.MoveDown},
        {ConsoleKey.Escape , InputAction.Exit}
    };

    public static InputAction GetInputAction(ConsoleKeyInfo consoleKeyInfo)
    {
        if(Actions.ContainsKey(consoleKeyInfo.Key))
        {
            return Actions[consoleKeyInfo.Key];
        }
        return InputAction.None;
    }
}   