using System;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour {
    public static Generator Instance;
    [SerializeField] private Slider _chargeSlider;
    [SerializeField] private AudioSource _turnOff;
    [SerializeField] private AudioSource _turnOn;
    [SerializeField] private float _dischargeRate = 1.0f;
    public AudioSource Rotating;
    private GameObject _lampLight;
    private GameObject _lampBulb;

    public static event Action<bool> GeneratorStatus;

    private float generatorCharge = 10;
    private bool _turnOffPlayed;
    private bool _turnOnPlayed;
    private bool _previousPower = true;
    private bool _firstTime =true;

    public float GeneratorCharge {
        get { return generatorCharge; }
        set {
            if (generatorCharge != value) {
                generatorCharge = value;
                _chargeSlider.value = generatorCharge/100;
            }
        }
    }

    private void Awake() {
        Instance = this;
        GeneratorCharge = generatorCharge;

        _lampLight = transform.GetChild(0).gameObject;
        _lampBulb = transform.GetChild(0).GetChild(0).gameObject;

    }

    private void FixedUpdate() {
        if(generatorCharge<=0) {
            // Out of charge! Turn off the lamp
            _lampBulb.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            _lampLight.GetComponent<Light>().enabled = false;
            _turnOnPlayed = false;
            if (!_turnOffPlayed && !_turnOff.isPlaying) {
                _turnOff.Play();
                _turnOffPlayed = true;
            }
            // Ensure we only invoke once
            if(_previousPower) {
                GeneratorStatus?.Invoke(false);
                _previousPower = false;
            }   
        } else {
            // On
            _lampBulb.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            _lampLight.GetComponent<Light>().enabled = true;
            _turnOffPlayed = false;
            if (!_turnOnPlayed && !_turnOn.isPlaying) {
                _turnOnPlayed = true;
                if (_firstTime) { 
                    _firstTime = false;
                    return; 
                }
                _turnOn.Play();
            }

            // Ensure we only invoke once
            if (!_previousPower) {
                GeneratorStatus?.Invoke(true);
                _previousPower = true;
            }
            // Drain power when there is a socket connection
            if(Socket.Instance.CurrentPlug != PlugType.Empty) GeneratorCharge -= _dischargeRate * 0.02f;
        }
    }

}
