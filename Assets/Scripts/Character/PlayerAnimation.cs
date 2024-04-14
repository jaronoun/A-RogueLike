using UnityEngine;

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
        animator.CrossFade("Idle", 0.2f);
    }

    public void StartWalking()
    {
        animator.CrossFade("Movement", 0.2f);
    }

    public void StartRunning()
    {
        animator.CrossFade("Running", 0.2f);
    }

    public void StartJump()
    {
        animator.Play("StartJump");
    }

    public void StartMidJump()
    {
        animator.CrossFade("MidJump", 0.2f);
    }

    public void StartEndJump()
    {
        animator.CrossFade("EndJump", 0.2f);
    }
}
