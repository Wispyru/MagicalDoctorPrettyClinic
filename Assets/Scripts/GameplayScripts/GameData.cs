using UnityEngine;

public class GameData
{
    public static MedicineSelect SelectedTile;
    public static bool IsAnimating;
    public static GameObject SelectedLevelButton;
    public static LevelData CurrentLevel;

    // Curent level states
    public static int CurrentRound;
    public static int CurrentMoveAmount;
    public static float CurrentTimeInSeconds;
    public static int CurrentPoints;
}
