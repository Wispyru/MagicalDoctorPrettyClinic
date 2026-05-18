using System.Collections;
using UnityEngine;

public class WinLoseCondition : MonoBehaviour
{
    private float WaitForScreenSec = 3f;

    public void CheckForCurrentTime()
    {
        if (GameData.CurrentTimeInSeconds > 0) return;

        CheckForPoints();


    }

    /// <summary>
    /// Checks the points that you have garnered at the end of the level when the time runs out.
    /// </summary>
    public void CheckForPoints()
    {
        if (GameData.CurrentPoints < GameData.CurrentLevel.RequiredPoints)
        {
            StartCoroutine(PlayLoseAnimation());
        }

        if (GameData.CurrentPoints >= GameData.CurrentLevel.RequiredPoints)
        {
            StartCoroutine(PlayWinAnimation());
        }
    }

    private IEnumerator PlayWinAnimation()
    {
        //TODO: Play animations
        yield return new WaitForSeconds(WaitForScreenSec);
        //TODO: Swap to WinScreen
    }

    private IEnumerator PlayLoseAnimation()
    {
        //TODO: Play animations
        yield return new WaitForSeconds(WaitForScreenSec);
        //TODO: Swap to losingScreen

    }
}
