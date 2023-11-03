using System;
using UnityEngine;

public class Socket : MonoBehaviour {

    public static Socket Instance;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _plugIn;
    [SerializeField] private AudioClip _plugOut;
    private bool _firstTime = true;

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
                if (_firstTime) { 
                    _firstTime = false; 
                    return;
                }
                _audioSource.clip = _plugOut;
                _audioSource.Play();
            } else {
                _audioSource.clip = _plugIn; 
                _audioSource.Play();
            }

        } }
    // public int[] Code { get { return _code; } private set { _code = value; }}
}
