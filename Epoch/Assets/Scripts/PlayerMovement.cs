using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate() {
        Move();
    }

    void ProcessInputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

    }

    void Move() {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        SetAnimatorMovement(moveDirection);
    }

    private void SetAnimatorMovement(Vector2 dir) {
        animator.SetFloat("xDir", moveDirection.x);
        animator.SetFloat("yDir", moveDirection.y);
        

    }
}
