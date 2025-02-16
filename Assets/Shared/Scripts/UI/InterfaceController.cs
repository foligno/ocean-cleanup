using OceanCleanup.Shared.Logic;
using UnityEngine;
using UnityEngine.UI;

namespace OceanCleanup.Shared.UI
{
    public class InterfaceController : MonoBehaviour
    {
        [SerializeField] private ModalUI _instructionsModal;
        [SerializeField] private ModalUI _gameOverModal;
        [SerializeField] private Text _scoreText;
        
        private int _score;
    
        private void OnEnable()
        {
            GameManager.OnGameOver += ShowGameOverModal;
            TrashBehaviour.OnDestroyed += IncreaseScore;
        }

        private void OnDisable()
        {
            GameManager.OnGameOver -= ShowGameOverModal;
            TrashBehaviour.OnDestroyed -= IncreaseScore;
        }

        private void Awake()
        {
            _instructionsModal.ShowModal();
            
            UpdateScoreText();
        }

        private void ShowGameOverModal()
        {
            if (_gameOverModal != null)
            {
                _gameOverModal.ShowModal();
            }
        }

        private void IncreaseScore()
        {
            _score += 1;
        
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            _scoreText.text = "Score: " + _score;
        }
    }
}
