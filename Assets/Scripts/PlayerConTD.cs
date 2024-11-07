using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private int shards = 0;
    [SerializeField] private TMP_Text shardsText;
    [SerializeField] private AudioClip spalterSoundClip;
    // List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // coll = GetComponent<Collider2D>();
        transform.position = new Vector3(0, GameManager.Instance.GetPlayerPositionX(), 0);

        shards = GameManager.Instance.GetShardCount();
        shardsText.text = shards.ToString();

        GameManager.Instance.PlaySound(spalterSoundClip);

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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Collectable") {
            Destroy(collision.gameObject);
            shards = GameManager.Instance.GetShardCount() + 1;
            GameManager.Instance.SetShardCount(shards);
            shardsText.text = shards.ToString();
        }
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
