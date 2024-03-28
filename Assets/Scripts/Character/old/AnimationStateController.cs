using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    public Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public void isWalking() {
        animator.SetBool("isWalking", true);
    }

    public void isNotWalking() {
        animator.SetBool("isWalking", false);
    }

    public void StartJump() {
        animator.SetTrigger("StartJump");
    }

    public void isFalling() {
        animator.SetBool("isFalling", true);
    }

    public void isNotFalling() {
        animator.SetBool("isFalling", false);
    }

    public void isGrounded() {
        animator.SetBool("isGrounded", true);
    }

    public void isNotGrounded() {
        animator.SetBool("isGrounded", false);
    }

    public void setWalkingAnim(float directionX, float directionY) {
        animator.SetFloat("moveX", directionX);
        animator.SetFloat("moveY", directionY);
    }

    public void isRunning() {
        animator.SetBool("isRunning", true);
    }

    public void isNotRunning() {
        animator.SetBool("isRunning", false);
    }

}
