using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneHandler : MonoBehaviour {
    [SerializeField] private Camera _cameraMain;
    //[SerializeField] private Transform _lookAtWin;
    //[SerializeField] private Transform _lookAtLose;
    [SerializeField] private Camera _cameraDed;
    [SerializeField] private Camera _cameraAlif;
    [SerializeField] private RenderTexture _rendureTexture;
    public static SceneHandler Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
    }

    private void Start() {
        _cameraMain = GameObject.Find("Main Camera").GetComponent<Camera>();
        _cameraDed = GameObject.Find("CameraDed").GetComponent<Camera>();
        _cameraAlif = GameObject.Find("CameraAlif").GetComponent<Camera>();
    }

    private void Update() {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            if (Input.GetKeyDown(KeyCode.R)) {
                RestartGame();
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 0) {
            // In tutorial 
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                SceneManager.LoadScene(1);
            }
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
        _cameraMain = GameObject.Find("Main Camera").GetComponent<Camera>();
        _cameraMain.targetTexture = null;

        _cameraAlif = GameObject.Find("CameraAlif").GetComponent<Camera>();
        _cameraAlif.targetTexture = _rendureTexture;
        CameraMovement.Instance.LookState = CameraMovement.CameraState.GameEnd;
       // _camera.transform.position = new Vector3(-0.18f, 1.136f, -31.287f);
       // _camera.transform.LookAt(_lookAtWin);
        //SceneManager.LoadScene(2);
    }
    public void GameLose() {
        _cameraMain = GameObject.Find("Main Camera").GetComponent<Camera>();
        _cameraMain.targetTexture = null;
        _cameraDed = GameObject.Find("CameraDed").GetComponent<Camera>();
        _cameraDed.targetTexture = _rendureTexture;
        CameraMovement.Instance.LookState = CameraMovement.CameraState.GameEnd;
        //_camera.transform.position = new Vector3(-0.18f, 1.136f, -31.287f);
        //_camera.transform.LookAt(_lookAtLose);
        //todo
    }

}
