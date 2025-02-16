using OceanCleanup.Shared.Helpers;
using OceanCleanup.Shared.Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace OceanCleanup.Smasher
{
	public class SmasherControls : MonoBehaviour
	{
		[SerializeField] private float _smashForce = 40.0f;
		[SerializeField] [FormerlySerializedAs("HoldTime")] public float _holdTime = 2.0F;
		
		private float _targetTime;
		private bool _hovering;
		private GameObject _currentObject;
		private Camera _camera;
	
		private void Awake()
		{
			_camera = Camera.main;
		}

		private void FixedUpdate ()
		{
			if (_hovering)
			{
				if (Input.GetMouseButton(0))
				{
					_currentObject.GetComponent<Animator>().SetBool(AnimatorHash.Shaking, true);
					_currentObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * (_smashForce * Time.fixedDeltaTime));

					if (Time.time >= _targetTime)
					{
						_currentObject.GetComponent<TrashBehaviour>().DestroyTrash();
					}
				}
			}

			if (IsCircleOver())
			{
				if (!_hovering)
				{
					_targetTime = Time.time + _holdTime;
					_hovering = true;
				}
			}
			else
			{
				_targetTime = 0.0F;
				_hovering = false;

				if (_currentObject)
				{
					_currentObject.GetComponent<Animator>().SetBool(AnimatorHash.Shaking, false);
				}
			}
		}
	
		private bool IsCircleOver()
		{
			Vector3 convertedMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
		
			RaycastHit2D hit = Physics2D.CircleCast(new Vector2(convertedMousePosition.x, convertedMousePosition.y), 0.5f, Vector2.zero, 0.0f);

			if (hit)
			{
				if (hit.collider.CompareTag("Trash"))
				{
					_currentObject = hit.collider.gameObject;
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private void OnDrawGizmos()
		{
			if (Input.GetMouseButton(0))
			{
				Gizmos.color = Color.red;
			
				Vector3 convertedMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
			
				Gizmos.DrawWireSphere(new Vector3(convertedMousePosition.x, convertedMousePosition.y, 0), 0.5f);
			}
		}
	}
}
