using UnityEngine;

namespace OceanCleanup.Shared.UI
{
    public class ModalUI : MonoBehaviour
    {
        private void OnEnable()
        {
            ShowModal();
        }

        private void OnDisable()
        {
            CloseModal();
        }

        public void ShowModal()
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }

        public void CloseModal()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
