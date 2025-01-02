using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarfaceAnimator : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateRoaring(int value)
    {
        string tag = "";

        tag = value switch
        {
            0 => "Left",
            1 => "Forward",
            2 => "Right",
            _ => "Forward",
        };

        animator.SetTrigger("isRoaring" + tag);
    }

    public void UpdateVelocity(Vector3 velocity)
    {
        animator.SetFloat("velocityY", velocity.y);
        animator.SetFloat("velocityZ", velocity.z);
    }
}