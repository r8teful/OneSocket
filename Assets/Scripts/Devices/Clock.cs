using TMPro;
using UnityEngine;

public class Clock : PlugDevice {
    // Will only show and count time when generator has power

    [SerializeField] private TextMeshProUGUI _clockTime;
    [SerializeField] private Phone _connectedPhone;
    
    public float[] _phoneTimes = new float[3];
    private bool[] _phoneNotified = new bool[3];

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
            NotifyPhoneCall(currentTime);
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
        base.OnDeactivate();
    }

    private void CreatePhoneTimes() {
        // Total game length is 5 minutes
        // First phone call is after 1 minute
        _phoneTimes[0] = Random.Range(60.0f, 90.0f);
        // Second phone call is after 2.5 minutes
        _phoneTimes[1] = Random.Range(120.0f, 200.0f);
        // Third phone call is after 4 minutes
        _phoneTimes[2] = Random.Range(240.0f, 280.0f);
    }

    private void NotifyPhoneCall(float time) {
        if (ProgressionManager.Instance.CompletedTutorial==0) return;
        for (int i = 0; i < _phoneTimes.Length; i++) {
            if (time >= _phoneTimes[i] && !_phoneNotified[i]) {
                _connectedPhone.SetSoundClipCodeOrder(i);
                _connectedPhone.Ring();
                _phoneNotified[i] = true;
            }
        }
    }

    public void ResetTimer() {
        currentTime = 0.0f;
    }
}