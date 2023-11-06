using System;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour {
    public static Generator Instance;
    [SerializeField] private Slider _chargeSlider;
    [SerializeField] private AudioSource _turnOff;
    [SerializeField] private AudioSource _turnOn;
    [SerializeField] private float _dischargeRate = 0.1f;
     private float _previousdischargeRate;
    public AudioSource Rotating;
    private GameObject _lampLight;
    private GameObject _lampBulb;

    public static event Action<bool> GeneratorStatus;

    private float _generatorCharge = 20f;
    private float _generatorChargeMax = 100;
    private bool _turnOffPlayed;
    private bool _turnOnPlayed;
    private bool _previousPower = true;
    private bool _firstTime =true;

    public float GeneratorCharge {
        get { return _generatorCharge; }
        set {
            if (_generatorCharge != value) {
                _generatorCharge = value;
                _chargeSlider.value = _generatorCharge/100;
            }
        }
    }

    public bool IsMaxCharge { get { return _generatorCharge >= _generatorChargeMax; }}

    private void Awake() {
        Instance = this;
        GeneratorCharge = _generatorCharge;
        _lampLight = transform.GetChild(0).gameObject;
        _lampBulb = transform.GetChild(0).GetChild(0).gameObject;
    }
    private void Start() {
        _chargeSlider.value = _generatorCharge / 100;
    }
    private void FixedUpdate() {
        if(_generatorCharge<=0) {
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
    public void DissableDischarge() {
        _previousdischargeRate = _dischargeRate;
        _dischargeRate = 0;
    }
    public void EnableDischarge() {
        _dischargeRate = _previousdischargeRate; 
    }
}