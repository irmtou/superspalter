/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private AudioClip fallSoundClip;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            SoundFXManager.instance.PlaySoundFXClip(fallSoundClip, transform, 0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
*/