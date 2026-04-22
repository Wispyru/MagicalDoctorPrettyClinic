using UnityEngine;

public class LoadLevelData : MonoBehaviour
{
    public LevelData LevelDataObject;


    public void LoadLevel()
    {
        GameData.CurrentTimeInSeconds = LevelDataObject.MaxTimeInSeconds;
        GameData.CurrentMoves = LevelDataObject.MaximumMoves;
    }
}
