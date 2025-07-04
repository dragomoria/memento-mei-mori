using UnityEngine.SceneManagement;
using UnityEngine;

public class ContinueStory : MonoBehaviour
{
    public void ProgressStoryClick()
    {
        SceneManager.LoadScene("Game");
    }
}
