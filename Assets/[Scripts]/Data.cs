using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static Difficulty difficulty;

    // 0 win, 1 lose
    public static int gameEndState = 0;
    public static void ResetData()
    {
        difficulty = Difficulty.EASY;
        gameEndState = 0;
    }
}
