using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SceneSwitcher : MonoBehaviour {
    public string TD; // Use string for scene name
    public string Plat; // Use string for scene name
    public CinemachineVirtualCamera platformerCamera;
    public CinemachineVirtualCamera topDownCamera;

    private bool isInTransitionZone = false;
    private bool isInPlatformer = true;


    private void Start() {
        
        // Set initial camera
        platformerCamera.Priority = 1; // Active camera
        topDownCamera.Priority = 0; // Inactive camera

        // Set initial camera
        platformerCamera.Priority = 1;  // Active camera
        topDownCamera.Priority = 0;     // Inactive camera

        // Check if we're switching into the top-down scene
        if (!isInPlatformer) {
            // Use the stored X position when entering the top-down scene
            Vector3 newPosition = transform.position;
            newPosition.x = PlayerPositionStorage.xPosition;
            transform.position = newPosition;
        }
    }

    private void Update() {
        if (isInTransitionZone && Input.GetKeyDown(KeyCode.Tab)) {
            SwitchPerspective();
        }
    }

    private void SwitchPerspective() {
        if (isInPlatformer) {
            // Store the X position before switching to top-down
            PlayerPositionStorage.xPosition = transform.position.x;
            SceneManager.LoadScene(TD); // Use the string reference

            // Switch to top-down camera
            platformerCamera.Priority = 0;
            topDownCamera.Priority = 1;
        }
        else {
            // Retrieve the X position when returning to the platformer
            Vector3 platformerPosition = new Vector3(PlayerPositionStorage.xPosition, transform.position.y, transform.position.z);
            transform.position = platformerPosition;
            SceneManager.LoadScene(Plat); // Use the string reference

            // Switch to platformer camera
            platformerCamera.Priority = 1;
            topDownCamera.Priority = 0;
        }

        isInPlatformer = !isInPlatformer;
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
