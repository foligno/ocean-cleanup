using OceanCleanup.Shared.Logic;
using UnityEngine;

namespace OceanCleanup.Shared
{
    public class GameManager : MonoBehaviour
    {
        public delegate void GameOverEvent();
        public static event GameOverEvent OnGameOver;
    
        private int _fishAlive;
    
        private void Awake()
        {
            _fishAlive = FindObjectsOfType<FishBehaviour>().Length;
        }

        private void OnEnable()
        {
            FishBehaviour.OnKilled += DecreaseFishCount;
        }

        private void OnDisable()
        {
            FishBehaviour.OnKilled -= DecreaseFishCount;
        }

        private void DecreaseFishCount()
        {
            _fishAlive -= 1;

            CheckFishCount();
        }

        private void CheckFishCount()
        {
            if (_fishAlive <= 0)
            {
                // GameOver
                OnGameOver?.Invoke();
            }
        }
    }
}
