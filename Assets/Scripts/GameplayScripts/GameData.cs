using UnityEngine;

public class GameData
{
    public static MedicineSelect SelectedTile;
    public static bool IsAnimating;
    public static GameObject SelectedLevelButton;
    public static LevelData CurrentLevel;

    // Curent level states
    public static int CurrentRound;
    public static float CurrentTimeInSeconds;
    public static int CurrentPoints;
    public static int CurrentMoves;
    public static int CurrentComboCount;
    public static bool IsComboActive;

    // Score per medicine type
    public static int[] ScorePerType = new int[5];
}
