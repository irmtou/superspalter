using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SceneSwitcher : MonoBehaviour {
    public string TD;  // Name of the top-down scene
    public string Plat;  // Name of the platformer scene
    public CinemachineVirtualCamera platformerCamera;
    public CinemachineVirtualCamera topDownCamera;

    private bool isInTransitionZone = false;

    private void Start() {
        // Check which scene was last active
        string lastScene = PlayerPrefs.GetString("LastScene", Plat);

        if (lastScene == Plat) {
            // Position player for platformer view: Platformer X, Y fixed at 0
            transform.position = new Vector3(PlayerPositionStorage.yPosition, 0, 0);
            platformerCamera.Priority = 1;
            topDownCamera.Priority = 0;
        }
        else {
            // Position player for top-down view: Y based on Platformer X, X fixed at 0
            transform.position = new Vector3(0, PlayerPositionStorage.xPosition, 0);
            platformerCamera.Priority = 0;
            topDownCamera.Priority = 1;
        }
    }

    private void Update() {
        if (isInTransitionZone && Input.GetKeyDown(KeyCode.Tab)) {
            SwitchPerspective();
        }
    }

    private void SwitchPerspective() {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == Plat) {
            // Store Platformer X to use as Top-down Y
            Debug.Log("PLAT -> TD");
            PlayerPositionStorage.yPosition = transform.position.x;

            // Switch to top-down
            PlayerPrefs.SetString("LastScene", TD);
            PlayerPrefs.Save();
            SceneManager.LoadScene(TD);

            platformerCamera.Priority = 0;
            topDownCamera.Priority = 1;
        }
        else {
            // Store Top-down Y to use as Platformer X
            Debug.Log("TD -> PLAT");
            PlayerPositionStorage.xPosition = transform.position.y;

            // Switch to platformer
            PlayerPrefs.SetString("LastScene", Plat);
            PlayerPrefs.Save();
            SceneManager.LoadScene(Plat);

            platformerCamera.Priority = 1;
            topDownCamera.Priority = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("TransitionZone")) {
            isInTransitionZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("TransitionZone")) {
            isInTransitionZone = false;
        }
    }
}