using UnityEngine;

namespace Character
{
    public class PlayerAnimation : MonoBehaviour
    {
        [Header("Animator")]
        [SerializeField] private Animator animator;

        public AnimatorStateInfo GetAnimationStateInfo()
        {
            return animator.GetCurrentAnimatorStateInfo(0);
        }

        public void SetMovement(float directionX, float directionY)
        {
            animator.SetFloat("moveX", directionX);
            animator.SetFloat("moveY", directionY);
        }

        public void StartIdle()
        {
            animator.CrossFade("Idle", 0.1f);
        }

        public void StartWalking()
        {
            animator.CrossFade("Move", 0.1f);
        }

        public void StartRunning()
        {
            animator.CrossFade("Run", 0.1f);
        }

        public void StartJump()
        {
            animator.Play("Start Jump");
        }

        public void StartMidJump()
        {
            animator.CrossFade("Mid Jump", 0.2f);
        }

        public void StartEndJump()
        {
            animator.CrossFade("End Jump", 0.2f);
        }
    }
}
