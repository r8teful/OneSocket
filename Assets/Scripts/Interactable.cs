using System;
using UnityEngine;

// By using the protected abstract combination, we are creating a contract that enforces the implementation
// of these methods in any class derived from Interactable,

public abstract class Interactable : MonoBehaviour {
    protected virtual void OnMouseEnter(){
        CursorManager.Instance.SetCursorClickable();
    }

    protected virtual void OnMouseExit() {
        CursorManager.Instance.SetCursorDefault();
    }

    protected abstract void OnMouseDown();
}
