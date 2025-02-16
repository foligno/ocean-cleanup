using UnityEngine;
using UnityEngine.Events;

namespace OceanCleanup.Shared.Components
{
	public class TimedCallback : MonoBehaviour
	{	
		public float TimeToActivation;
		public bool RepeatTimer;
	
		private float _startTime;
		private float _activationTime;

		[SerializeField] private UnityEvent _onCompleteEvent;
	
		void Start ()
		{
			// Disable script if no component present.
			if (_onCompleteEvent == null)
			{
				Debug.Log(name + " " + GetType() + " does not contain an event, disabling script.");
			
				enabled = false;
				return;
			}

			SetupTimer();
		}
	
		private void Update()
		{
			CheckTimer();
		}

		private void CheckTimer()
		{
			if (Time.time > _activationTime)
			{
				// Activate
				_onCompleteEvent.Invoke();
			
				if (RepeatTimer)
				{
					SetupTimer();
				}
				else
				{
					// Disable timer
					enabled = false;
				}
			}
		}
	
		private void SetupTimer()
		{
			_startTime = Time.time;
			_activationTime = _startTime + TimeToActivation;
		}
	}
}
