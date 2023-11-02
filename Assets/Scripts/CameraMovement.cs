using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public static CameraMovement Instance;

    public CameraState LookState = CameraState.Room;
    [SerializeField] private Transform generatorPosition;
    [SerializeField] private Transform codePosition;
    [SerializeField] private Transform leftWallPosition;
    [SerializeField] private Transform roomPosition;
    [SerializeField] private float _speed;
    private Quaternion _targetRotation;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        MoveCamera();
    }


    private void FixedUpdate() {
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _speed * Time.deltaTime);
    }


    public void MoveCamera() {
        switch (LookState) {
            case CameraState.Room:
                // Set camera transform to look at left wall
                _targetRotation = Quaternion.LookRotation(roomPosition.position - transform.position);
                break;
            case CameraState.Generator:
                _targetRotation = Quaternion.LookRotation(generatorPosition.position - transform.position);
                break;
            case CameraState.Code:
                _targetRotation = Quaternion.LookRotation(codePosition.position - transform.position);
                break;
        }
    }


    public void MoveCameraLeft() {
        switch (LookState) {
            case CameraState.Room:
                LookState = CameraState.Code;
                MoveCamera();
                break;
            case CameraState.Generator:
                LookState = CameraState.Room;
                 MoveCamera();
                break;
            case CameraState.Code:
                LookState = CameraState.Generator;
                 MoveCamera();
                break;
            case CameraState.GameEnd:
                break;
        }
    }

    public void MoveCameraRight() {
        switch (LookState) {
            case CameraState.Room:
                LookState = CameraState.Generator;
                MoveCamera();
                break;
            case CameraState.Generator:
                LookState = CameraState.Code;
                MoveCamera();
                break;
            case CameraState.Code:
                LookState = CameraState.Room;
                MoveCamera();
                break;
            case CameraState.GameEnd:
                break;
        }
    }


    public enum CameraState {
        Room,
        Generator,
        Code,
        GameEnd
    }
}
