using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class SceneSwitcher : MonoBehaviour {
    public string TD = "TD"; // Name of the top-down scene
    public string Plat = "Plat"; // Name of the platformer scene
    public CinemachineVirtualCamera platformerCamera;
    public CinemachineVirtualCamera topDownCamera;
    [SerializeField] private AudioClip spalterSoundClip;
    [SerializeField] private AudioClip chimes;

    private bool isInTransitionZone = false;

    public const int PLAT_INDEX = 0;
    public const int TD_INDEX = 1;

    public GameObject currGameObject;

    public int sceneIndex = -1;

    private void Start() {
        // Clear default positions in GameManager
        /*GameManager.Instance.xPlatPosition = 0;
        GameManager.Instance.yTDPosition = 0;*/

        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == PLAT_INDEX) {
            // PLATFORMER
            // Position player for platformer view
          
            platformerCamera.Priority = 1;
            topDownCamera.Priority = 0;
        }
        else {
            // TOPDOWN
            // Position player for top-down view

            platformerCamera.Priority = 0;
            topDownCamera.Priority = 1;
        }
    }

    private void Update() {
        if (isInTransitionZone) {
            if (Input.GetKeyDown(KeyCode.Tab)) {


                /*if (sceneIndex == PLAT_INDEX) {
                    currGameObject = GameObject.Find("Player-TD");
                }
                else {
                    currGameObject = GameObject.Find("Player");
                }*/
                SavePosition();
                Destroy(gameObject); // addition
                SwitchPerspective(gameObject);



            }
        }
    }

    private void SavePosition() {
        if (GameManager.Instance != null) {
            GameManager.Instance.SavePlayerPosition(transform.position);
        }
    }

    private void SwitchPerspective(GameObject other) {
        
        if (sceneIndex == PLAT_INDEX) {
            Debug.Log("PLAT -> TD");

            // Store Platformer X for Top-down Y
            /*GameManager.Instance.yTDPosition = transform.position.x;*/

            GameManager.Instance.SavePlayerPosition(transform.position);
// Switch to top-down
            SceneManager.LoadScene(TD_INDEX);
            

            // Update player position in new scene based on stored value
            other.gameObject.transform.position = new Vector3(0, GameManager.Instance.GetPlayerPositionX(), 0);

            platformerCamera.Priority = 0;
            topDownCamera.Priority = 1;
            //SoundFXManager.instance.PlaySoundFXClip(spalterSoundClip, transform, 0.5f);
        }
        else {
            Debug.Log("TD -> PLAT");

            // Store Top-down Y for Platformer X
            /*GameManager.Instance.xPlatPosition = transform.position.y;*/
            GameManager.Instance.SavePlayerPosition(transform.position);
 // Switch to platformer
            SceneManager.LoadScene(PLAT_INDEX);
            


            // Update player position in new scene based on stored value
            other.gameObject.transform.position = new Vector3(GameManager.Instance.GetPlayerPositionY(), 5, 0);

            platformerCamera.Priority = 1;
            topDownCamera.Priority = 0;
            //SoundFXManager.instance.PlaySoundFXClip(spalterSoundClip, transform, 0.5f);
        }

        /*Debug.Log("After transition STATS: ");
        Debug.Log("TDY (from Transform): " + transform.position.y);
        Debug.Log("PlatX: " + GameManager.Instance.xPlatPosition);
        Debug.Log("PlatX (from Transform): " + transform.position.x);
        Debug.Log("TDY: " + GameManager.Instance.yTDPosition);*/
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("TransitionZone")) {
            isInTransitionZone = true;
            GameManager.Instance.PlaySound(chimes);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("TransitionZone")) {
            isInTransitionZone = false;
            
        }
    }
}