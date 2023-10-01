using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : Interactable {

    public static Socket Instance;
    [SerializeField] private float _dischargeRate = 1.0f;
    public static event Action<PlugType> OnOutOfCharge;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _plugIn;
    [SerializeField] private AudioClip _plugOut;
    private void Awake() {
        Instance = this;
        CurrentPlug = PlugType.Empty;
    }
    // need a private PlugType currentPlug;
    //public PlugType CurrentPlug { get { return CurrentPlug; }
    //    set { 
    //        CurrentPlug = value;
    //        // other shit
    //    }
    //}
    private PlugType _currentPlug;
    public PlugType CurrentPlug { get { return _currentPlug; } set { _currentPlug = value;
            Debug.Log("Setting plug to: " + _currentPlug);
            if (_currentPlug.Equals(PlugType.Empty)) {
                _audioSource.clip = _plugOut;
                _audioSource.Play();
            } else {
                _audioSource.clip = _plugIn; 
                _audioSource.Play();
            }

        } }
    // public int[] Code { get { return _code; } private set { _code = value; }}

    private void FixedUpdate() {
        // Take charge off the generator aslong as there is something connected to the socket
        if(CurrentPlug != PlugType.Empty && Generator.Instance.GeneratorCharge > 0) {
            Generator.Instance.GeneratorCharge -= _dischargeRate * 0.02f;
        } else if (Generator.Instance.GeneratorCharge <= 0) {
            //Debug.Log("No more charge!");
            OnOutOfCharge?.Invoke(CurrentPlug);

        }
    }

}
