using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour {
    public string sceneToLoad;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}