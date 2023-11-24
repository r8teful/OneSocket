using UnityEngine;

// By using the protected abstract combination, we are creating a contract that enforces the implementation
// of these methods in any class derived from Interactable,

public abstract class Interactable : MonoBehaviour {
    protected virtual void OnMouseEnter(){
        Debug.Log("Enter" + gameObject.name);
        CursorManager.Instance.SetCursorClickable();
    }

    protected virtual void OnMouseExit() {
        Debug.Log("Exit" + gameObject.name);
        CursorManager.Instance.SetCursorDefault();
    }

    protected abstract void OnMouseDown();
}
