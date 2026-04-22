using UnityEngine;

public class GameplayTimer : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        GameData.CurrentTimeInSeconds -= Time.deltaTime;
    }
}
