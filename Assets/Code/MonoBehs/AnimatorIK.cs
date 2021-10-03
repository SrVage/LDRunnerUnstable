using UnityEngine;

namespace Client.MonoBehs
{
    public class AnimatorIK:MonoBehaviour
    {
        public Transform GunPosition;
        public Animator Animator;
        public Transform Aim;
        private void OnAnimatorIK(int layerIndex)
        {
            Animator.SetLookAtWeight(1);
            Animator.SetLookAtPosition(Aim.position);
            Debug.Log(AvatarIKGoal.RightHand);
            Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            Animator.SetIKPosition(AvatarIKGoal.RightHand, GunPosition.position);
        }
    }
}