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
    private Vector3 platformerPosition;
    private Vector3 topDownPosition;

    private void Start() {
        platformerPosition = transform.position;
        topDownPosition = new Vector3(transform.position.x, 0, transform.position.y);

        // Set initial camera
        platformerCamera.Priority = 1; // Active camera
        topDownCamera.Priority = 0; // Inactive camera
    }

    private void Update() {
        if (isInTransitionZone && Input.GetKeyDown(KeyCode.Tab)) {
            SwitchPerspective();
        }
    }

    private void SwitchPerspective() {
        if (isInPlatformer) {
            platformerPosition = transform.position;
            topDownPosition = new Vector3(platformerPosition.x, 0, platformerPosition.y);
            SceneManager.LoadScene(TD); // Use the string reference
            transform.position = topDownPosition;

            // Switch to top-down camera
            platformerCamera.Priority = 0;
            topDownCamera.Priority = 1;
        }
        else {
            topDownPosition = transform.position;
            platformerPosition = new Vector3(topDownPosition.x, topDownPosition.z, 0);
            SceneManager.LoadScene(Plat); // Use the string reference
            transform.position = platformerPosition;

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
