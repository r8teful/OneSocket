using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour {
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;

    // The target position to move towards
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;

    // Start is called before the first frame update
    void Start() {
        // Set the target position to the current position
        _targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;
        float ratioX = (float)renderTexture.width / Screen.width;
        float ratioY = (float)renderTexture.height / Screen.height;

        mousePosition.x *= ratioX;
        mousePosition.y *= ratioY;
        // Get the ray that goes from the camera through the mouse position
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        // Cast the ray and get the hit information
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            // Set the target position to the hit point
            _targetPosition = hit.point;
        }
        _targetRotation = Quaternion.Euler(0,mainCamera.transform.eulerAngles.y,0);

        transform.position = Vector3.Lerp(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
    }
}
