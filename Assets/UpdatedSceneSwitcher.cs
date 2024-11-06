/*using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

// Separate static class for persistent data
public static class PlayerPositionStorage1 {
    public static float xPosition = 0f;
    public static float yPosition = 0f;
    public static bool isInPlatformer = true;
    public static string lastScene = "";

    public static void SavePosition(Vector3 position, bool inPlatformer) {
        if (inPlatformer) {
            xPosition = position.x;
            Debug.Log($"Saved platformer X position: {xPosition}");
        }
        else {
            yPosition = position.y;
            Debug.Log($"Saved top-down Y position: {yPosition}");
        }
        isInPlatformer = inPlatformer;
        lastScene = SceneManager.GetActiveScene().name;
        Debug.Log($"Saved state - IsInPlatformer: {isInPlatformer}, LastScene: {lastScene}");
    }
}

public class UpdatedSceneSwitcher : MonoBehaviour {
    public string TD;  // Name of the top-down scene
    public string Plat;  // Name of the platformer scene
    public CinemachineVirtualCamera platformerCamera;
    public CinemachineVirtualCamera topDownCamera;
    private bool isInTransitionZone = false;

    private void Awake() {
        // Make this object persistent across scene loads
        DontDestroyOnLoad(gameObject);
        Debug.Log($"SceneSwitcher Awake - Current Scene: {SceneManager.GetActiveScene().name}");
    }

    private void Start() {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log($"Starting in scene: {currentScene}");
        Debug.Log($"Previous scene was: {PlayerPositionStorage1.lastScene}");
        Debug.Log($"Stored xPosition: {PlayerPositionStorage1.xPosition}");
        Debug.Log($"Stored yPosition: {PlayerPositionStorage1.yPosition}");

        // Set initial state based on current scene
        PlayerPositionStorage.isInPlatformer = currentScene == Plat;

        // Position the player based on the scene and stored positions
        if (PlayerPositionStorage1.isInPlatformer) {
            float xPos = PlayerPositionStorage1.yPosition; // Use stored Y as X in platformer
            transform.position = new Vector3(xPos, 5f, 0f);
            platformerCamera.Priority = 1;
            topDownCamera.Priority = 0;
            Debug.Log($"Platformer Start: Setting X to {xPos} from stored Y position");
        }
        else {
            float yPos = PlayerPositionStorage1.xPosition; // Use stored X as Y in top-down
            transform.position = new Vector3(0f, yPos, 0f);
            platformerCamera.Priority = 0;
            topDownCamera.Priority = 1;
            Debug.Log($"Top-down Start: Setting Y to {yPos} from stored X position");
        }
    }

    private void Update() {
        // Debug key to print current state
        if (Input.GetKeyDown(KeyCode.P)) {
            PrintDebugInfo();
        }

        if (isInTransitionZone && Input.GetKeyDown(KeyCode.Tab)) {
            SwitchPerspective();
        }
    }

    private void PrintDebugInfo() {
        Debug.Log("=== Debug Info ===");
        Debug.Log($"Current Scene: {SceneManager.GetActiveScene().name}");
        Debug.Log($"IsInPlatformer: {PlayerPositionStorage1.isInPlatformer}");
        Debug.Log($"Current Position: {transform.position}");
        Debug.Log($"Stored X Position: {PlayerPositionStorage1.xPosition}");
        Debug.Log($"Stored Y Position: {PlayerPositionStorage1.yPosition}");
        Debug.Log($"In Transition Zone: {isInTransitionZone}");
    }

    private void SwitchPerspective() {
        // Save current position before switching
        PlayerPositionStorage1.SavePosition(transform.position, PlayerPositionStorage1.isInPlatformer);

        if (PlayerPositionStorage1.isInPlatformer) {
            Debug.Log($"Switching TO Top-down FROM Platformer");
            Debug.Log($"Current position before switch: {transform.position}");
            SceneManager.LoadScene(TD);
        }
        else {
            Debug.Log($"Switching TO Platformer FROM Top-down");
            Debug.Log($"Current position before switch: {transform.position}");
            SceneManager.LoadScene(Plat);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("TransitionZone")) {
            isInTransitionZone = true;
            Debug.Log("Entered transition zone");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("TransitionZone")) {
            isInTransitionZone = false;
            Debug.Log("Exited transition zone");
        }
    }
}*/