using UnityEngine;

namespace OceanCleanup.Shared.Logic
{
    public class TrashBehaviour : MonoBehaviour
    {
        public delegate void TrashDestroyedEvent();
        public static event TrashDestroyedEvent OnDestroyed;
    
        public GameObject DeathParticles;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Turbine"))
            {
                DestroyTrash();
            }
        }

        public void DestroyTrash()
        {
            ParticleController.SpawnParticleSystem(DeathParticles, transform);

            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}