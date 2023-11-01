using System;
using System.Collections;
using UnityEngine;

public abstract class PlugDevice : MonoBehaviour {

    [SerializeField] protected PlugType plugType;
    [SerializeField] protected GameObject connectedPrefab;
    [SerializeField] protected GameObject disconnectedPrefab;
    protected GameObject childPrefab;

    private bool pluggedIn;
    private bool HasPower { get { return Generator.Instance.GeneratorCharge > 0; } }

    protected virtual void Awake() {
        childPrefab = Instantiate(disconnectedPrefab, transform);
        Generator.GeneratorStatus += OnGeneratorStatus;
    }
    protected virtual void OnDestroy() {
        Generator.GeneratorStatus -= OnGeneratorStatus;
    }

    protected virtual void OnGeneratorStatus(bool b) {
        UpdateActivationStatus(b, pluggedIn);
    }

    protected virtual void OnDeactivate() {
        //StopAllCoroutines();
        //StartCoroutine(WaitForPower());
    }
    protected virtual void OnActivate() {
       // StopAllCoroutines();
       // StartCoroutine(WaitForDisruption());
    }
    public virtual void OnPlugClicked() {
        if (Socket.Instance.CurrentPlug == plugType) {
            // Already connected, disconnect
            Socket.Instance.CurrentPlug = PlugType.Empty;
            Destroy(childPrefab);
            childPrefab = Instantiate(disconnectedPrefab, transform);
            pluggedIn = false;
            UpdateActivationStatus(HasPower, pluggedIn);

            //OnDeactivate();
        } else if (Socket.Instance.CurrentPlug == PlugType.Empty) {
            // Connect!
            Socket.Instance.CurrentPlug = plugType;
            Destroy(childPrefab);
            childPrefab = Instantiate(connectedPrefab, transform);
            pluggedIn = true;
            UpdateActivationStatus(HasPower, pluggedIn);
        }
    }

    protected virtual void UpdateActivationStatus(bool hasPower, bool isConnected) {
        if (hasPower && isConnected) {
            OnActivate();
        } else {
            OnDeactivate();
        }
    }

    /*
    protected IEnumerator WaitForDisruption() {
        yield return new WaitUntil(() => !HasPower);
        OnDeactivate();
    }
    private IEnumerator WaitForPower() {
        while (!HasPower) {
            if (Socket.Instance.CurrentPlug == PlugType.Empty) {
                // exit out of whole function
                yield break;
            }
            yield return null;
        }
        OnActivate();
    }*/

}
