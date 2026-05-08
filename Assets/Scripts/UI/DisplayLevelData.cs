using UnityEngine;
using TMPro;
public class DisplayLevelData : MonoBehaviour
{
    public TMP_Text TimeText;
    public TMP_Text RoundText;
    public TMP_Text MovesText;
    private GameplayTimer _timer;
    private LoadLevelData _levelData;
    private void Start()
    {
        _timer = GetComponent<GameplayTimer>();
        _levelData = GetComponent<LoadLevelData>();
        UpdateUIText();
    }

    private void Update()
    {
        if (GameData.CurrentTimeInSeconds > 0)
        {
            _timer.Timer();
            updateTimerText();

        }

        if (GameData.CurrentMoveAmount == 0)
        {
            updateRound();
            UpdateUIText();
        }
    }

    public void UpdateUIText()
    {
        RoundText.text = "Rounds left: " + GameData.CurrentRound.ToString();
        MovesText.text = "Moves: " + GameData.CurrentMoveAmount.ToString();
    }

    private void updateTimerText()
    {
        int minutes = Mathf.FloorToInt(GameData.CurrentTimeInSeconds / 60);
        int seconds = Mathf.FloorToInt(GameData.CurrentTimeInSeconds % 60);

        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void updateRound()
    {
        GameData.CurrentRound--;
        _levelData.ResetMoves();    
    }
}
