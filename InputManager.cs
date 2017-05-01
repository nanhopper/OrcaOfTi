using System;
using System.Collections.Generic;

public enum InputAction
{
    None,
    MovePlayerLeft,
    MovePlayerRight,
    MovePlayerUp,
    MovePlayerDown,
    SpeedUp,
    SlowDown,
    EatGlob,
    Exit
}

public static class InputManager
{
    private static readonly Dictionary<ConsoleKey, InputAction> Actions = new Dictionary<ConsoleKey, InputAction>()
    {
        {ConsoleKey.LeftArrow , InputAction.MovePlayerLeft},
        {ConsoleKey.RightArrow , InputAction.MovePlayerRight},
        {ConsoleKey.UpArrow , InputAction.MovePlayerUp},
        {ConsoleKey.DownArrow , InputAction.MovePlayerDown},
        {ConsoleKey.OemPlus , InputAction.SpeedUp},
        {ConsoleKey.Oem4 , InputAction.SlowDown},
        {ConsoleKey.Spacebar , InputAction.EatGlob},
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