using UnityEngine;

public class GameplayTimer : MonoBehaviour
{
    public void Timer()
    {
        while(GameData.CurrentTimeInSeconds > 0)
        {
            GameData.CurrentTimeInSeconds -= Time.deltaTime;
            return;
        }
    }
}
