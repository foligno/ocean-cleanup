using UnityEngine;

namespace OceanCleanup.Shared.Logic
{
    public static class ParticleController {

        public static void SpawnParticleSystem(GameObject particleToCreate, Transform newLocation)
        {
            Object newParticleSystem =
                Object.Instantiate(particleToCreate, newLocation.position, newLocation.localRotation);

            Object.Destroy(newParticleSystem, particleToCreate.GetComponent<ParticleSystem>().main.duration);
        }
    }
}
