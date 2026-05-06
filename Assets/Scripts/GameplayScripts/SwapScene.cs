using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScene : MonoBehaviour
{
    public int SceneIndex;

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
