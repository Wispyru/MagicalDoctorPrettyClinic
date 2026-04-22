using UnityEngine;

public class LoadLevelData : MonoBehaviour
{
    public LevelData LevelDataObject;


    public void LoadLevel()
    {
        GameData.CurrentTimeInSeconds = LevelDataObject.MaxTime;
        GameData.CurrentMoves = LevelDataObject.MaxMoves;
        
        Debug.Log(GameData.CurrentMoves);
    }
}
