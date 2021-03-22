using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Vector3 axis = Vector3.zero;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Movement(Vector3 axis)
    {
        if (axis != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("horizontal", axis.x);
            animator.SetFloat("vertical", axis.y);
        }
        else animator.SetBool("isWalking", false);
    }
}
