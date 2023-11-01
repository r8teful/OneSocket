using System.Collections;
using UnityEngine;

public class Lamp : PlugDevice {

    private Light _light;
    private GameObject _bulb;
    private GameObject _childPrefab;
    private Color _lightOnColor = new Color(1, 0.9192954f, 0.6556604f, 1.0f);


    protected override void Awake() {
        base.Awake();
        _light = GetComponentInChildren<Light>();
        _bulb = transform.GetChild(0).GetChild(0).gameObject;
        LightOff();
    }

    protected override void OnActivate() {
        LightOn();
        base.OnActivate();
    }

    protected override void OnDeactivate() {
        LightOff();
        base.OnDeactivate();
    }

    private void LightOff() {
        _light.enabled = false;
        _bulb.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        _bulb.GetComponentInChildren<Light>().enabled = false; 
    }
    private void LightOn() {
        _light.enabled = true;
        _bulb.GetComponent<Renderer>().material.SetColor("_Color", _lightOnColor);
        _bulb.GetComponentInChildren<Light>().enabled = true;
    }
}
