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

    public bool canMove = true;

    void Start() {
        animator = GetComponent<Animator>();
        moveSpeed = GameManager.instance.playerSpeed;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(!canMove) return;

        ProcessInputs();
    }

    void FixedUpdate() {
        Move();

        animator.SetBool("isFacingRight", isFacingRight);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y));
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
