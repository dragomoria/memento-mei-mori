using UnityEngine.SceneManagement;
using UnityEngine;

public class ContinueGoodEnd : MonoBehaviour
{
    public void ProgressStoryClick()
    {
        SceneManager.LoadScene("Win");
    }
}