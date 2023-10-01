using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickArea : Interactable {

    private PlugDevice _parentScript;
    private bool _isInsideTrigger = false;

    private void Awake() {
        _parentScript = GetComponentInParent<PlugDevice>();
    }

    private void OnTriggerEnter(Collider other) {
        _isInsideTrigger = true;
        //Debug.Log("Inside: "+  _parentScript + gameObject);
       // Cursor.SetCursor
        _parentScript.OnCursorEnter();
        // Todo say that I'm hovering
    }

    private void OnTriggerExit(Collider other) {
        _isInsideTrigger = false;
        _parentScript.OnCursorExit();
        // Todo say that I'm not hovering anymore
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && _isInsideTrigger) {
            //Debug.Log("SendMessage: " + _parentScript);
            _parentScript.OnCursorClick();
            //_parentScript.SendMessage("OnCursorClick");
        }
    }

}
