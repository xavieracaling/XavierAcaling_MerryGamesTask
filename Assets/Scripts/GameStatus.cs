using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStatus 
{
    public static int MaxLevel = 3;
    private static int levelGame;
    public static int LevelGame { get => levelGame; set => levelGame = value;}
    private static bool lastStatusWin;
    public static bool LastStatus { get => lastStatusWin; set => lastStatusWin = value;}

    public static void SetLastStatus(bool status) => lastStatusWin = status;
    public static void SetLevelGame() => LevelGame++;
}
