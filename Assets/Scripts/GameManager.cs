using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static bool IsGameOver {get; private set;} = false;
    public static void Win()
    {
        IsGameOver = true;
    }

    public static void Lose()
    {
        IsGameOver = true;
    }

    public static void Reset()
    {
        IsGameOver = false;
    }
}
