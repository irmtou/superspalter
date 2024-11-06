using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Shrooms : MonoBehaviour {

    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioClip shroomSoundClip;

    // FSM

    private enum State { idle, bounce };
    private State state = State.idle;


    private void Start() {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() {

        anim.SetInteger("state", (int)state);
        

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == ("Player") && state != State.bounce) {
            state = State.bounce;
            SoundFXManager.instance.PlaySoundFXClip(shroomSoundClip, transform, 0.5f);

        }
       
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") ) {
            state = State.idle; // Reset state to idle when player leaves the collider
        }
    }
}