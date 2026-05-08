using UnityEngine;

public class LoadLevelData : MonoBehaviour
{

    private void FindDataToLoad()
    {

        GameData.CurrentLevel = GameData.SelectedLevelButton.GetComponent<AssignedLevelData>().AssignedData;
    }

    public void LoadData()
    {
        FindDataToLoad();
        GameData.CurrentRound = GameData.CurrentLevel.Rounds;
        GameData.CurrentMoveAmount = GameData.CurrentLevel.MovesPerRound;
        GameData.CurrentTimeInSeconds = GameData.CurrentLevel.MaxTimeInSeconds;
        GameData.CurrentPoints = 0;
        Debug.Log("Data loaded!");
    }

    public void ResetMoves()
    {
        GameData.CurrentMoveAmount = GameData.CurrentLevel.MovesPerRound;
    }

}
