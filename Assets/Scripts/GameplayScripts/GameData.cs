public class GameData
{
    public static MedicineSelect SelectedTile;
    public static bool IsAnimating;

    // Level Data
    public static float CurrentTimeInSeconds;
    public static int CurrentMoves;
    public static int MaxMoves;
    public static int CurrentPoints;
    public static int CurrentComboCount;
    public static bool IsComboActive;

    // Score per medicine type
    public static int[] ScorePerType = new int[5];
}