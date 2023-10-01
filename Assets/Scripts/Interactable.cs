using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour {

    private Color _defaultColor = new Color(1.0f, 0.72f, 0.87f, 1.0f); // 

    public virtual void OnCursorEnter() { 
        //var hand = FindObjectOfType(typeof(FollowCursor));
        //hand.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }
    public virtual void OnCursorExit() {

       // var hand = FindObjectOfType(typeof(FollowCursor));
      //  hand.GetComponent<Renderer>().material.SetColor("_Color", _defaultColor);
    }
    public virtual void OnCursorClick() { }
}
