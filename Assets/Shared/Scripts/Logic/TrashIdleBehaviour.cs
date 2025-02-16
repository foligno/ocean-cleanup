using OceanCleanup.Shared.Helpers;
using UnityEngine;

namespace OceanCleanup.Shared.Logic
{
	public class TrashIdleBehaviour : StateMachineBehaviour
	{
		public float checkInterval = 1.0f;
		public float velocityThreshold = 0.3f;
		
		private Rigidbody2D _trashRigidbody;
		private float _nextCheckTime;
	
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			_trashRigidbody = animator.gameObject.GetComponent<Rigidbody2D>();
			SetNextCheckTime();
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (Time.time > _nextCheckTime)
			{
				animator.SetBool(AnimatorHash.Idle, _trashRigidbody.velocity.magnitude < velocityThreshold);
				SetNextCheckTime();
			}
		}

		private void SetNextCheckTime()
		{
			_nextCheckTime = Time.time + checkInterval;
		}
	}
}
