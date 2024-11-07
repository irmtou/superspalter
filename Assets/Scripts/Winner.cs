using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour {
    
    [SerializeField] public AudioClip yippeeSoundClip;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            SoundFXManager.instance.PlaySoundFXClip(yippeeSoundClip, transform, 0.5f);
            SceneManager.LoadScene(2);
        }
    }
}