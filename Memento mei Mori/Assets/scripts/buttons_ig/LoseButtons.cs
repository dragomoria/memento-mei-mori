using UnityEngine.SceneManagement;
using UnityEngine;

public class LoseButtons : MonoBehaviour
{
        public void OnRetryClick()
        {
            SceneManager.LoadScene("Game");
        }

        public void OnExitClick()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
        }
}
