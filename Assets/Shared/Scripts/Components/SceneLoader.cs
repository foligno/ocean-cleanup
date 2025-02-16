using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OceanCleanup.Shared.Components
{
    public class SceneLoader : MonoBehaviour
    {
        /// <summary>
        /// Optional loading bar visual to use for the progress of the scene load.
        /// </summary>
        public Slider loadingBar;

        /// <summary>
        /// Denotes if a loading bar should be used.
        /// </summary>
        private bool _useLoadingBar;
    
        /// <summary>
        /// Displays an overlay while loading a scene.
        /// </summary>
        /// <param name="sceneName">Scene name to load.</param>
        public void LoadScene(string sceneName)
        {
            // Enable the current UI object.
            gameObject.SetActive(true);
        
            // Enable or disable the loading bar.
            _useLoadingBar = loadingBar != null;
            
            // Start loading the level.
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        /// <summary>
        /// Loads a scene asynchronously while updating the progress on a loading bar, if present.
        /// </summary>
        /// <param name="sceneName">Scene name to load.</param>
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        
            while (!loadOperation.isDone)
            {
                if (_useLoadingBar)
                {
                    float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);

                    loadingBar.value = progress;
                }
            
                yield return null;
            }
        }
    }
}
