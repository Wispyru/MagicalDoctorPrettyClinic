using UnityEngine;

public class GameplayTimer : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public void Timer()
    {
        while (GameData.CurrentTimeInSeconds > 0)
        {
            GameData.CurrentTimeInSeconds -= Time.deltaTime;
            //TODO: In an if-statement, when timer reaches 0: Call function to trigger game over screen.
        }
    }
}
