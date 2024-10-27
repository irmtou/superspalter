using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerConTD : MonoBehaviour {

    public float moveSpeed = 3f;
    // public float collisionOffset = 0.05f;
    // public ContactFilter2D movementFilter;

    Vector2 movement;
    private Rigidbody2D rb;
    // List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        // coll = GetComponent<Collider2D>();
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.deltaTime);
    }
}
