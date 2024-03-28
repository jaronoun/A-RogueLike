using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator animator;

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    // General method for setting bools to reduce redundancy
    private void SetBool(string name, bool state)
    {
        animator.SetBool(name, state);
    }

    // General method to directly set triggers
    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }

    // General method for setting floats for movement
    public void SetMovement(float directionX, float directionY)
    {
        animator.SetFloat("moveX", directionX);
        animator.SetFloat("moveY", directionY);
    }

    // Movement States
    public void SetWalking(bool state)
    {
        SetBool("isWalking", state);
    }

    public void SetRunning(bool state)
    {
        SetBool("isRunning", state);
    }

    // Jump States
    public void StartJump()
    {
        SetTrigger("StartJump");
    }

    public void SetFalling(bool state)
    {
        SetBool("isFalling", state);
    }

    public void SetGrounded(bool state)
    {
        SetBool("isGrounded", state);
    }
}
