using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueBadEnd : MonoBehaviour
{
    public void ProgressStoryClick()
    {
        SceneManager.LoadScene("Lose");
    }
}
