using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
public class LoadLevelData : MonoBehaviour
{
    private TMP_Text _description;
    private void FindDataToLoad()
    {
        GameData.CurrentLevel = GameData.SelectedLevelButton.GetComponent<AssignedLevelData>().AssignedData;
        Debug.Log(GameData.CurrentLevel);
    }
    public void LoadData()
    {
        FindDataToLoad();
        GameData.CurrentRound = GameData.CurrentLevel.Rounds;
        GameData.CurrentMoves = GameData.CurrentLevel.MovesPerRound;
        GameData.CurrentTimeInSeconds = GameData.CurrentLevel.MaxTimeInSeconds;
        GameData.CurrentPoints = 0;
        Debug.Log("Data loaded!");
    }

    public void ResetMoves()
    {
        GameData.CurrentMoves = GameData.CurrentLevel.MovesPerRound;
    }

}
