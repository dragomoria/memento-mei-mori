using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueStory : MonoBehaviour
{
    public void ProgressStoryClick()
    {
        SceneManager.LoadScene("Game");
    }
}
