using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    // Start variables
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D feet;
    private CapsuleCollider2D body;
    // private Collider2D body;

    [SerializeField] private AudioClip jumpSoundClip;
    [SerializeField] private AudioClip shardSoundClip;
    [SerializeField] private AudioClip enemyPoofSoundClip;
    [SerializeField] private AudioClip fallSoundClip;

    // FSM
    private enum State { idle, walking, jumping, falling, hurt, running };
    private State state = State.idle;

    

    // Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int shards = 0;
    [SerializeField] private TMP_Text shardsText;
    [SerializeField] private float hurtForce = 10f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        feet = GetComponent<BoxCollider2D>();
        body = GetComponent<CapsuleCollider2D>();

        // Load saved position from GameManager if available
        transform.position = new Vector3(GameManager.Instance.GetPlayerPositionY(), 2, 0);

        shards = GameManager.Instance.GetShardCount();
        shardsText.text = shards.ToString();

        // uitext = GetComponent<TextMeshProUGUI>();
    }


    private void Update() {

        if (state != State.hurt) {
            Movement(); 
        }
        AnimationState();
        anim.SetInteger("state", (int)state); // Sets animation based on Enumerator state
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            if (state == State.falling) {
                Destroy(other.gameObject);
                SoundFXManager.instance.PlaySoundFXClip(enemyPoofSoundClip, transform, 0.5f);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                state = State.jumping;
            }
            else {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x) {
                    // Enemy is on the right, player will be damaged and moved left
                    // .this is implied
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else {
                    // Enemy is on the left, player will be damaged and moved right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
        else if (other.gameObject.tag == "Bouncer") {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce*1.5f);
            state = State.jumping;

        }
        /*else if (other.gameObject.tag == "Fallen") {
                SoundFXManager.instance.PlaySoundFXClip(fallSoundClip, transform, 0.5f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Collectable") {
            Destroy(collision.gameObject);
            SoundFXManager.instance.PlaySoundFXClip(shardSoundClip, transform, 0.5f);
            shards = GameManager.Instance.GetShardCount() + 1;
            GameManager.Instance.SetShardCount(shards);
            shardsText.text = shards.ToString();
        }
        if (collision.tag == "Fallen") {
            SoundFXManager.instance.PlaySoundFXClip(fallSoundClip, transform, 0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }

    private void Movement() {
        float hDirection = Input.GetAxis("Horizontal");
        float vDirection = Input.GetAxis("Vertical");
        // Moving left
        if (hDirection < 0) {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);

        }
        // Moving right
        else if (hDirection > 0) {

            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);

        }
        // Jumping
        if (Input.GetButtonDown("Jump") && feet.IsTouchingLayers(ground)) {
            Jump();
        }
    }

    private void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
        SoundFXManager.instance.PlaySoundFXClip(jumpSoundClip, transform, 0.5f);
    }

    private void AnimationState() {

        if (state == State.jumping) {
            if (rb.velocity.y < .1f) {
                state = State.falling;
            }
        }
        else if (state == State.falling) {
            if (feet.IsTouchingLayers(ground)) {
                state = State.idle;
            }
        }
        else if (state == State.hurt) {
            if (Mathf.Abs(rb.velocity.x) < .1f) {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f) {
            // Go right
            state = State.walking;
        }
        else {
            state = State.idle;
        }
    }
 
}

