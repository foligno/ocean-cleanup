using UnityEngine;
using UnityEngine.SceneManagement;

namespace OceanCleanup.Shared.Input
{
    public class SceneControls : MonoBehaviour
    {
        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                RestartScene();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                GoToMenu();
            }
        }

        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void GoToMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}