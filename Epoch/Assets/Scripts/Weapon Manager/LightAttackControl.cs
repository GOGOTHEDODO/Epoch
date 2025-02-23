using UnityEngine;

public class LightAttackControl : MonoBehaviour
{
    public SpriteRenderer attackRenderer;
    private Animator animator;

    void Start()
    {
        attackRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Calculate direction from player to mouse
        Vector2 direction = (mousePosition - transform.position).normalized;
        transform.right = direction;

        // If the attack is going to occur on the left then we need to flip the sprite
        if (direction.x < 0)
        {
            attackRenderer.flipY = true;
        }
        else
        {
            attackRenderer.flipY = false;
        }


        // Handle attack input
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");

            // animator.Play("MeleeIdle");
        }

    }
}
