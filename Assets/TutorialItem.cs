using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItem : MonoBehaviour
{
    private bool _isInsideTrigger;

    private void OnTriggerEnter(Collider other) {
        _isInsideTrigger = true;
    }

    private void OnTriggerExit(Collider other) {
        _isInsideTrigger = false;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && _isInsideTrigger) {
            Debug.Log("YAAAAA");
            TutorialHandler.Instance.NextStep();
        }
    }
}
