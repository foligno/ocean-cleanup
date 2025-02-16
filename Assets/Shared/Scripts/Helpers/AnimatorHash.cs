using UnityEngine;

namespace OceanCleanup.Shared.Helpers
{
    public struct AnimatorHash
    {
        public static readonly int Idle = Animator.StringToHash("idle");
        public static readonly int Float = Animator.StringToHash("float");
        public static readonly int Shaking = Animator.StringToHash("shaking");
    }
}