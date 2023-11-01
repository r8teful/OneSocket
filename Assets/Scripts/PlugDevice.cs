using UnityEngine;

public abstract class PlugDevice : MonoBehaviour {
    public virtual void OnPlugDisconnected() { }
    public virtual void OnPlugConnected() { }
    public abstract void OnPlugClicked();
}
