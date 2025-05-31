using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseButtons : MonoBehaviour
{
        public void OnRetryClick()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public void OnExitClick()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
        }
}
