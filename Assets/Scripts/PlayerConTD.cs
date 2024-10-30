using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Tilemaps;
// using UnityEngine.InputSystem;

public class PlayerConTD : MonoBehaviour {

    // public float moveSpeed = 3f;
    // public float collisionOffset = 0.05f;
    // public ContactFilter2D movementFilter;

    private Vector2 movement;
    Rigidbody2D rb;
    Animator anim;

    public float speed = 0.5f;

    private Vector2 lastMoveDirection;
    private bool facingLeft = false;
    // List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // coll = GetComponent<Collider2D>();
    }


    void Update()
    {
        ProcessInputs();
        Animate();
        if(movement.x < 0 && !facingLeft || movement.x > 0 && facingLeft) {
            Flip();
        }

    }

    private void FixedUpdate() {
        rb.velocity = movement * speed;
        // rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.deltaTime);
    }

    void ProcessInputs() {

        // Store last move direction when we stop moving
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if((moveX == 0 && moveY ==0) && (movement.x != 0 || movement.y != 0)) {
           lastMoveDirection = movement;

        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
    }

    void Animate() {
        anim.SetFloat("MoveX", movement.x);
        anim.SetFloat("MoveY", movement.y);
        anim.SetFloat("MoveMagnitude", movement.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    void Flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Flips the sprite
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }
}
