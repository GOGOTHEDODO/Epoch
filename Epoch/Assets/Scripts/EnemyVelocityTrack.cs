using UnityEngine;
using Pathfinding;

public class EnemyVelocityTrack : MonoBehaviour
{
    public Rigidbody rb;
    public AIPath aiPath;
    public Animator animator;

    bool isFacingRight = false;
    private Vector3 currentVelocity;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (aiPath == null) aiPath = GetComponent<AIPath>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Use AIPath velocity if available, otherwise Rigidbody
        if (aiPath != null)
        {
            currentVelocity = aiPath.velocity;
        }
        else if (rb != null)
        {
            currentVelocity = rb.velocity;
        }
        else
        {
            currentVelocity = Vector3.zero;
        }

        float moveX = currentVelocity.x;

        // Flip direction based on X movement
        if ((isFacingRight && moveX < -0.1f) || (!isFacingRight && moveX > 0.1f))
        {
            isFacingRight = !isFacingRight;
            Debug.Log($"Direction flipped. Now facing right: {isFacingRight}");
        }

        // Calculate velocity magnitude and pass to Animator
        float velocityMagnitude = currentVelocity.magnitude;

        if (animator != null)
        {
            animator.SetFloat("xVelocity", velocityMagnitude);
            animator.SetBool("IsFacingRight", isFacingRight);

            Debug.Log($"A* Velocity: {currentVelocity} | Magnitude (xVelocity): {velocityMagnitude} | FacingRight: {isFacingRight}");
        }
    }

    public Vector3 GetCurrentVelocity()
    {
        return currentVelocity;
    }
}
