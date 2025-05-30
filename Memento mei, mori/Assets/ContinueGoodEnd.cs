using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGoodEnd : MonoBehaviour
{
    public void ProgressStoryClick()
    {
        SceneManager.LoadScene("Win");
    }
}