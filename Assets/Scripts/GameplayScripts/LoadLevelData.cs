using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;
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

        GameData.CurrentStartingAnimation = GameData.CurrentLevel.StartingAnimation;
        GameData.CurrentIdleAnimation = GameData.CurrentLevel.IdleAnimation; 
        GameData.CurrentAttackAnimation = GameData.CurrentLevel.AttackAnimation;
        GameData.CurrentWinAnimation = GameData.CurrentLevel.WinAnimation;
        GameData.CurrentLoseAnimation = GameData.CurrentLevel.LoseAnimation;
    Debug.Log("Data loaded!");
    }

    public void ResetMoves()
    {
        GameData.CurrentMoves = GameData.CurrentLevel.MovesPerRound;
    }

}
