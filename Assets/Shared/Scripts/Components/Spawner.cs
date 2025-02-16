using UnityEngine;

namespace OceanCleanup.Shared.Components
{
	public class Spawner : MonoBehaviour
	{
		[Header("Settings")]
		[Tooltip("Prefab of game object that will be spawned.")]
		public GameObject[] PrefabsToSpawn;
	
		[Tooltip("Amount of game objects to spawn.")]
		public int Count = 1;

		[Tooltip("Toggle for immediate spawning of game objects.")]
		public bool SpawnOnWake = true;

		[Tooltip("Toggle for randomization of Y orientation.")]
		public bool RandomOrientation;
	
		[Tooltip("Spawn region for randomization of location.")]
		public bool RandomLocation;
		[HideInInspector]
		public Bounds SpawnRegion;

		private int _previousPrefabIndex = -1;

		private void Start ()
		{
			if (SpawnRegion == new Bounds())
			{
				RandomLocation = false;
			}

			if (PrefabsToSpawn.Length == 0)
			{
				enabled = false;
			}
			else
			{
				if (SpawnOnWake)
				{
					SpawnPrefabs();
				}
			}
		}
	
		public void SpawnPrefabs()
		{
			Vector3 newLocation = transform.position;
			Vector3 newRotation = transform.rotation.eulerAngles;
		
			for (int i = 0; i < Count; i++)
			{
				GameObject prefabToSpawn;

				if (PrefabsToSpawn.Length > 0)
				{
					int prefabIndex;

					do
					{
						prefabIndex = Random.Range(0, PrefabsToSpawn.Length - 1);
					} while (_previousPrefabIndex == prefabIndex);
				
					prefabToSpawn = PrefabsToSpawn[prefabIndex];
				
					_previousPrefabIndex = prefabIndex;
				}
				else
				{
					prefabToSpawn = PrefabsToSpawn[0];
				}
			
				if (RandomLocation)
				{
					Bounds prefabBounds = new Bounds();
					BoxCollider prefabCollider = prefabToSpawn.GetComponent<BoxCollider>();
			
					if(prefabCollider != null)
					{
						prefabBounds = prefabCollider.bounds;
					}

					Vector3 randomSpawnLocation = new Vector3(
						Random.Range(-SpawnRegion.extents.x + prefabBounds.extents.x, SpawnRegion.extents.x - prefabBounds.extents.x),
						Random.Range(-SpawnRegion.extents.y + prefabBounds.extents.y, SpawnRegion.extents.y - prefabBounds.extents.y),
						Random.Range(-SpawnRegion.extents.z + prefabBounds.extents.z, SpawnRegion.extents.z - prefabBounds.extents.z)
					);
				
					newLocation = transform.position + SpawnRegion.center + randomSpawnLocation;
				}
		
				if (RandomOrientation)
				{			
					newRotation = new Vector3(0.0F, Random.Range(0.0F, 359.0F), 0.0F);
				}

				Instantiate(prefabToSpawn, newLocation, Quaternion.Euler(newRotation), transform);
			}
		}

		public void IncreaseCount(int amount)
		{
			Count += amount;
		}
	}
}