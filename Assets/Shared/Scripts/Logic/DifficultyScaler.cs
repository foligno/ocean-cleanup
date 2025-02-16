using UnityEngine;

namespace OceanCleanup.Shared.Logic
{
    public class DifficultyScaler : MonoBehaviour
    {
        public void ScaleGravity(float amount)
        {
            Physics2D.gravity *= amount;
        }
    }
}
