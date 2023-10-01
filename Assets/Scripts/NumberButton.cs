using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberButton : MonoBehaviour {
    [SerializeField] private int number;
    private bool _isInsideTrigger;


    private void OnTriggerEnter(Collider other) {
        _isInsideTrigger = true;
        // Todo say that I'm hovering
    }

    private void OnTriggerExit(Collider other) {
        _isInsideTrigger = false;
        // Todo say that I'm not hovering anymore
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && _isInsideTrigger) {
            //Debug.Log("SendMessage: " + _parentScript);
            GameEndHandler.Instance.OnNumberButtonClick(number);
            //_parentScript.SendMessage("OnCursorClick");
        }
    }
}
