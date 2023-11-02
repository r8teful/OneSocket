using System.Collections;
using TMPro;
using UnityEngine;

public class Clock : PlugDevice {
    // Will only show and count time when generator has power

    [SerializeField] private TextMeshProUGUI _clockTime;

    public float[] _phoneTimes = new float[3];

    [SerializeField] private AudioSource _tikTok;
    private string _previousFormattedTime = "";


    public float currentTime = 58f;
    private bool _hasPower = false;

    protected override void Awake() {
        base.Awake();
        CreatePhoneTimes();
    }
  
    private void Update() {
        if(_hasPower) {
            _clockTime.enabled = true;
            currentTime += Time.deltaTime;
            string formattedTime = string.Format("{0:00}:{1:00}", Mathf.Floor(currentTime / 60), Mathf.Floor(currentTime % 60));
            if(formattedTime != _previousFormattedTime) {
                _tikTok.Play();
                _previousFormattedTime = formattedTime;
            }
            _clockTime.text = formattedTime;
            // Notify phone to call if time is right
            if (currentTime >= _phoneTimes[0] && Phone.PhoneState.Equals(Phone.PhoneStatedCode.None)) {
                Phone.PhoneState = Phone.PhoneStatedCode.First;
                Phone.Instance.Ring();
                
            } if(currentTime >= _phoneTimes[1] && Phone.PhoneState.Equals(Phone.PhoneStatedCode.First)) {
                Phone.PhoneState = Phone.PhoneStatedCode.Second;
                Phone.Instance.Ring();
            } if(currentTime >= _phoneTimes[2] && Phone.PhoneState.Equals(Phone.PhoneStatedCode.Second)) {
                Phone.PhoneState = Phone.PhoneStatedCode.Third;
                Phone.Instance.Ring();
            }
        } else {
            // Turn of the clock
            _clockTime.enabled = false;
        }
        
    }

    protected override void OnActivate() {
        _hasPower = true;
        base.OnActivate();
    }

    protected override void OnDeactivate() {
        _hasPower = false;
        Phone.Instance.OnPlugDisconnectedPhone();
        base.OnDeactivate();
    }



    private void CreatePhoneTimes() {
        // Total game length is 5 minutes
        // First phone call is after 1 minute
        _phoneTimes[0] = UnityEngine.Random.Range(60.0f, 90.0f);
        // Second phone call is after 2.5 minutes
        _phoneTimes[1] = UnityEngine.Random.Range(120.0f, 200.0f);
        // Third phone call is after 4 minutes
        _phoneTimes[2] = UnityEngine.Random.Range(240.0f, 280.0f);
    }

    public bool GetHasPower() => _hasPower;
}