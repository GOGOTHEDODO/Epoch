using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;


public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rb;
    public Vector2 moveDirection;
    public Animator animator;
    bool isFacingRight = false;
<<<<<<< HEAD

=======
>>>>>>> RyansBranch
    public bool canMove = true;

    //Dash Vars
    public bool isInvincible = false;
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;

    private bool isDashing = false;
    private float dashTimeRemaining;
    private float dashCooldownTimer;
    private Vector2 lastMoveDirection;
    public int dashQuantity;
    public int currentDashes;
    public float dashRechargeTime = 1f;
    private float dashRechargeTimer = 0f;

    void Start() {
        animator = GetComponent<Animator>();
        moveSpeed = GameManager.instance.playerSpeed;
        dashQuantity = GameManager.instance.currentDashQuantity;
        currentDashes = dashQuantity;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!canMove) return;

        ProcessInputs();

        if (moveDirection != Vector2.zero)
        {
            lastMoveDirection = moveDirection;
        }

        dashCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && currentDashes > 0)
        {
            animator.SetBool("Dashing", true);
            isDashing = true;
            dashTimeRemaining = dashDuration;
            dashCooldownTimer = dashCooldown;
            currentDashes--;

             if (currentDashes < dashQuantity && dashRechargeTimer <= 0f)
            {
                dashRechargeTimer = dashRechargeTime;
            }
        }

        if (currentDashes < dashQuantity)
        {
            dashRechargeTimer -= Time.deltaTime;

            if (dashRechargeTimer <= 0f)
            {
                currentDashes++;
                if(currentDashes < dashQuantity)
                {
                dashRechargeTimer = dashRechargeTime;
                }
            }
        }

    }

    void FixedUpdate() {

        if (isDashing)
        {
            isInvincible = true;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
            rb.velocity = lastMoveDirection * dashSpeed;
            dashTimeRemaining -= Time.fixedDeltaTime;

            if (dashTimeRemaining <= 0f)
            {
                animator.SetBool("Dashing", false);
                isDashing = false;
                isInvincible = false;
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
            }

            return; // Skip regular movement while dashing
        }


        Move();

        animator.SetBool("isFacingRight", isFacingRight);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
<<<<<<< HEAD
=======

    }

    public void PlayerDeathEvents()
    {
        rb.velocity = new Vector2(0, 0);
        
        canMove = false;

        rb.constraints = RigidbodyConstraints2D.FreezeAll; // Lock position & rotation

>>>>>>> RyansBranch
    }



    void ProcessInputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Flip is facing right when we changed directions
        // This decides how velocity is interpretted
        if(isFacingRight && moveX < 0f || !isFacingRight && moveX > 0f)
        {
            isFacingRight = !isFacingRight;
        }

        moveDirection = new Vector2(moveX, moveY).normalized;

    }

    void Move() {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public void IncreaseMovementSpeed(float percentage) {
        moveSpeed += moveSpeed * (percentage / 100);
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }

    public void enableMovement(bool enable)
    {
        canMove = enable;
        Debug.Log("Called enablemovement");
        if(!enable)
        { 
            rb.velocity = Vector2.zero;   
            rb.constraints = RigidbodyConstraints2D.FreezePosition; 
        }
    }
}
