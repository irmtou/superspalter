using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Ghost : MonoBehaviour {

    // Start variables
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;

    // FSM
    private enum State { idle, moving };
    private State state = State.moving;
    private bool facingLeft = true;

    // Inspector variables
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask ground;

    private int idleLoops = 0;
    private bool isWaiting = false; // To prevent multiple coroutine calls

    private void Start() {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (!isWaiting) {
            Movement();
        }
        anim.SetInteger("state", (int)state); // Sets animation based on Enumerator state
    }

    private void Movement() {
        if (facingLeft) {
            if (transform.position.x > leftCap) {
                if (transform.localScale.x != -1 && coll.IsTouchingLayers(ground)) {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground) && !isWaiting) {
                    StartCoroutine(StallState());
                    state = State.moving;
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                }
            }
            else {
                facingLeft = false;
            }
        }
        else {
            if (transform.position.x < rightCap) {
                if (transform.localScale.x != 1 && coll.IsTouchingLayers(ground)) {
                    transform.localScale = new Vector3(1, 1);
                }
                if (coll.IsTouchingLayers(ground) && !isWaiting) {
                    StartCoroutine(StallState());
                    state = State.moving;
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                }
            }
            else {
                facingLeft = true;
            }
        }
    }

    private IEnumerator StallState() {
        isWaiting = true;

        idleLoops = UnityEngine.Random.Range(0, 3); // Choose 0, 1, or 2 idle loops

        for (int i = 0; i < idleLoops; i++) {
            state = State.idle;
            yield return new WaitForSeconds(1f); // Adjust duration to match idle animation time
        }

        state = State.moving;
        isWaiting = false;
    }
}
