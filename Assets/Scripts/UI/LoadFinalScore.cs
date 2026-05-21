using TMPro;
using UnityEngine;

public class LoadFinalScore : MonoBehaviour
{
    private TMP_Text ScoreText;

    void Start()
    {
        ScoreText = GetComponent<TMP_Text>();
        ScoreText.text = GameData.CurrentPoints.ToString();
    }
}
