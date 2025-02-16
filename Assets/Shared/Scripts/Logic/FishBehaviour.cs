using UnityEngine;
using Random = UnityEngine.Random;

namespace OceanCleanup.Shared.Logic
{
	public class FishBehaviour : MonoBehaviour
	{
		public delegate void FishDiedEvent();
		public static event FishDiedEvent OnKilled;

		public BoxCollider2D SwimRegion;
		public float SwimSpeed = 0.2F;
		public GameObject DeathParticles;
	
		private Vector3 _currentTarget;
		private bool _isMoving;
		private readonly Vector2 _roamStep = new(1.0F, 0.2F);
		
		private void Start () {
			if (SwimRegion == null)
			{
				enabled = false;
			}	
		}
		
		private void FixedUpdate () {
			if (_isMoving)
			{
				if (transform.position == _currentTarget)
				{
					_isMoving = false;
				}
				else
				{
					transform.position = Vector3.MoveTowards(transform.position, _currentTarget, SwimSpeed * Time.fixedDeltaTime);
				}
			}
			else
			{
				_currentTarget = GenerateTargetPosition();
				_isMoving = true;
			}
		}

		private Vector3 GenerateTargetPosition()
		{
			Vector3 newLocation;

			do
			{
				newLocation = new Vector3(
					Random.Range(transform.position.x - _roamStep.x, transform.position.x + _roamStep.x),
					Random.Range(transform.position.y - _roamStep.y, transform.position.y + _roamStep.y),
					0.0F);
			} while (!SwimRegion.bounds.Contains(newLocation));
		
			return newLocation;
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Trash") ||
			    other.gameObject.CompareTag("Turbine"))
			{
				ParticleController.SpawnParticleSystem(DeathParticles, transform);

				OnKilled?.Invoke();

				Destroy(gameObject);
			}
		}
	}
}
