
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Speaker : PlugDevice {
    [SerializeField] private PlugType _plugType;
    [SerializeField] private GameObject _connectedPrefab;
    [SerializeField] private GameObject _disconnectedPrefab;
    [SerializeField] private TextMeshProUGUI _chargesText;
    [SerializeField] private AudioSource _audioSource;
    private int _charges = 3;

    private GameObject _childPrefab;
    private void Awake() {
        _childPrefab = Instantiate(_disconnectedPrefab, transform);
        Socket.OnOutOfCharge += OnOutOfCharge;
    }

    private void OnOutOfCharge(PlugType type) {
        if (type == _plugType) {
            // I'm effected!
            _audioSource.Stop();
            // Wait until the generator has charge again
            StartCoroutine(WaitForCharge());
        }

    }

    private IEnumerator WaitForCharge() {

        while (Generator.Instance.GeneratorCharge <= 0) {
            if (Socket.Instance.CurrentPlug == PlugType.Empty) {
                // exit out of whole function
                yield break;
            }
            yield return null;
        }
        // Maybe some slow buildup?

        _audioSource.Play();
    }

    public override void OnPlugConnected() {

        if (_charges <= 0) return;
        _audioSource.Play();
        Monster.Instance.Scare();
        _charges--;
        _chargesText.text=_charges.ToString();
    }

    public override void OnPlugDisconnected() {
        _audioSource.Stop();
    }


    public override void OnCursorClick() {
        //Debug.Log("OnCursorClick on" + gameObject.name);
        // tell Socket to connect
        if (Socket.Instance.CurrentPlug == PlugType.Empty && _charges <= 0) return;
        if (Socket.Instance.CurrentPlug == _plugType) {
            // You are already connected, disconnect?
            Socket.Instance.CurrentPlug = PlugType.Empty;
            Destroy(_childPrefab);
            _childPrefab = Instantiate(_disconnectedPrefab, transform);
            OnPlugDisconnected();
        } else if (Socket.Instance.CurrentPlug == PlugType.Empty) {
            // Connect!
            Socket.Instance.CurrentPlug = _plugType;
            Destroy(_childPrefab);
            _childPrefab = Instantiate(_connectedPrefab, transform);

            OnPlugConnected();
        }
    }
}
