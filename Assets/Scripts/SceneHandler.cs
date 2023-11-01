using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour {
    //[SerializeField] private Transform _lookAtWin;
    //[SerializeField] private Transform _lookAtLose;
    public static SceneHandler Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
    }

    private void Start() {
    }

    private void Update() {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            if (Input.GetKeyDown(KeyCode.R)) {
                RestartGame();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SceneManager.LoadScene(1); 
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            GameWin();
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            GameLose();
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(1);
    }
    public void GameWin() {
        SceneManager.LoadScene(3);
    }
    public void GameLose() {
        SceneManager.LoadScene(2);
        //_camera.transform.position = new Vector3(-0.18f, 1.136f, -31.287f);
        //_camera.transform.LookAt(_lookAtLose);
        //todo
    }

}
