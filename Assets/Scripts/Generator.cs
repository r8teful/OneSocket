using UnityEngine;
using UnityEngine.UI;

public class Generator : Interactable {


    public static Generator Instance;
    [SerializeField] private Slider _chargeSlider;
    [SerializeField] private AudioSource _turnOff;
    [SerializeField] private AudioSource _turnOn;
    public AudioSource Rotating;
    private GameObject _lampLight;
    private GameObject _lampBulb;

    private float generatorCharge = 50;
    private bool _turnOffPlayed;
    private bool _turnOnPlayed;

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

    private void Update() {
        if(generatorCharge<=0) {
            // Out of charge! Turn off the lamp
            _lampBulb.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            _lampLight.GetComponent<Light>().enabled = false;
            _turnOnPlayed = false;
            if (!_turnOffPlayed && !_turnOff.isPlaying) {
                _turnOff.Play();
                _turnOffPlayed = true;
            }
        } else {
            // On
            _lampBulb.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            _lampLight.GetComponent<Light>().enabled = true;
            _turnOffPlayed = false;
            if (!_turnOnPlayed && !_turnOn.isPlaying) {
                _turnOnPlayed = true;
                _turnOn.Play();
            }
        }
    }
}
