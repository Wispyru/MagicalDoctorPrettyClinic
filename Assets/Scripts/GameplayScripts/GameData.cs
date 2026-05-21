using UnityEngine;
using UnityEngine.Video;

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

    public static VideoClip CurrentStartingAnimation;
    public static VideoClip CurrentIdleAnimation;
    public static VideoClip CurrentAttackAnimation;
    public static VideoClip CurrentWinAnimation;
    public static VideoClip CurrentLoseAnimation;

    // Score per medicine type
    public static int[] ScorePerType = new int[5];
}
