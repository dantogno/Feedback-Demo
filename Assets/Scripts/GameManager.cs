using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static bool IsGameOver {get; private set;} = false;
    public static event Action<bool> GameEnded;
    public static void Win()
    {
        IsGameOver = true;
        GameEnded?.Invoke(true);
    }

    public static void Lose()
    {
        IsGameOver = true;
        GameEnded?.Invoke(false);
    }

    public static void ResetGame()
    {
        Settings.ResetSettings();
        IsGameOver = false;        
    }
}
