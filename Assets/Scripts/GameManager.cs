using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    // Variables to store player position
    public float yTDPosition;
    public float xPlatPosition;

    public static float currX;
    public static float currY;

    public static int currShards;

    public static int prevScene;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject); // Prevent duplicates
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager across scenes
        }
    }

    // Method to update position data
    public void SavePlayerPosition(Vector3 position) {
        currX = position.x;
        currY = position.y;
        Debug.Log("Saved position in GameManager: " + currX + ", " + currY);
    }

    // Method to retrieve position as Vector3
    public float GetPlayerPositionX() {
        return currX;
    }
    public int GetLastScene() {
        return prevScene;
    }
    public void SetSceneFromBefore(int sceneb4) {
        prevScene = sceneb4;
    }
    public float GetPlayerPositionY() {
        return currY;
    }

    public int GetShardCount() {
        return currShards;     
    }

    public void SetShardCount(int shardCount) {
        currShards = shardCount;
    }
    public void PlaySound(AudioClip clip) {
        SoundFXManager.instance.PlaySoundFXClip(clip, transform, 0.5f);
        Debug.Log("Played sound from GameManager");
    }
}
