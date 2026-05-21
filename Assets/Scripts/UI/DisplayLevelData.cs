using UnityEngine;
using TMPro;
public class DisplayLevelData : MonoBehaviour
{
    public TMP_Text TimeText;
    public TMP_Text RoundText;
    public TMP_Text MovesText;
    public TMP_Text PointsText;

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

    }

    public void UpdateUIText()
    {
        if(GameData.CurrentMoves == 0) updateRound();
        RoundText.text = "Rounds left: " + GameData.CurrentRound.ToString();
        MovesText.text = "Moves: " + GameData.CurrentMoves.ToString();
        PointsText.text = "Points: " + GameData.CurrentPoints.ToString();
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
