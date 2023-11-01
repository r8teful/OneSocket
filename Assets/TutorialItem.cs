using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    private bool _isInsideTrigger;

    private void OnMouseEnter(Collider other) {
        _isInsideTrigger = true;
    }

    private void OnMouseExit(Collider other) {
        _isInsideTrigger = false;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && _isInsideTrigger) {
            Debug.Log("YAAAAA");
            TutorialHandler.Instance.NextStep();
        }
    }
}
