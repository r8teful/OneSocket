using UnityEngine;

public class DebugOnClick : MonoBehaviour {
    public void OnClickDebugLog() {
        Debug.Log("Button Clicked");
    }

    public void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger Enter");
    }


}
