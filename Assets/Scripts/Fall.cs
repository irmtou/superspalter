using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour {
    [SerializeField] private string sceneToLoad;
    [SerializeField] public AudioClip fallSoundClip;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            SoundFXManager.instance.PlaySoundFXClip(fallSoundClip, transform, 0.5f);

            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            GameManager.Instance.SetSceneFromBefore(sceneIndex);

            float positionPlatX = GameManager.Instance.GetPlayerPositionX();
            float positionPlatY = GameManager.Instance.GetPlayerPositionY();
            /*SceneManager.LoadScene(SceneManager.GetActiveScene().name);*/
            if (sceneIndex == 0) {
                collision.gameObject.transform.position = new Vector3(positionPlatX, positionPlatY, 0);
            }
        }
    }
}
