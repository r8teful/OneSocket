using System.Collections;
using UnityEngine;

public class Lamp : PlugDevice {

    private Light _light;
    [SerializeField] private PlugType _plugType;
    [SerializeField] private GameObject _connectedPrefab;
    [SerializeField] private GameObject _disconnectedPrefab;
    private GameObject _bulb;
    private GameObject _chiledPrefab;
    private Color _lightOnColor = new Color(1, 0.9192954f, 0.6556604f, 1.0f);

    private void Awake() {
        _light = GetComponentInChildren<Light>();
        _bulb = transform.GetChild(0).GetChild(0).gameObject;
        _chiledPrefab = Instantiate(_disconnectedPrefab,transform);
        Socket.OnOutOfCharge += OnOutOfCharge;
        LightOff();
    }

    private void OnDestroy() {
        Socket.OnOutOfCharge -= OnOutOfCharge;
    }

    private void OnOutOfCharge(PlugType type) {
        if(type == _plugType) {
            // I'm effected!
            LightOff();
            // Wait until the generator has charge again
            StartCoroutine(WaitForCharge());
        }
            
    }

    private IEnumerator WaitForCharge() {
        while (Generator.Instance.GeneratorCharge <= 0) {
            if(Socket.Instance.CurrentPlug == PlugType.Empty) {
               // exit out of whole function
               yield break;
            }
            yield return null;
        }
        // Maybe some slow buildup?
        LightOn();
    }

    public override void OnPlugConnected() {
        // Bad to directly reference here but its a game jam!
        // Only turn on if the generator has charge
        if(Generator.Instance.GeneratorCharge > 0) LightOn();
    }

    public override void OnPlugDisconnected() {
        LightOff();
    }

   // public override void OnCursorEnter() {
   //     base.OnCursorEnter();
   // }
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

    public override void OnPlugClicked() {
        //Debug.Log("OnCursorClick on" + gameObject.name);
        // tell Socket to connect
        if (Socket.Instance.CurrentPlug == _plugType) {
            // You are already connected, disconnect?
            Socket.Instance.CurrentPlug = PlugType.Empty;
            Destroy(_chiledPrefab);
            _chiledPrefab = Instantiate(_disconnectedPrefab, transform);
            OnPlugDisconnected();
        } else if (Socket.Instance.CurrentPlug == PlugType.Empty) {
            // Connect!
            Socket.Instance.CurrentPlug = _plugType;
            Destroy(_chiledPrefab);
            _chiledPrefab = Instantiate(_connectedPrefab,transform);
            
            OnPlugConnected();
        }
    }
}
