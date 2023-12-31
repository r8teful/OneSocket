using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : Interactable {
    // Does not need charge, just calls lol when its wants, its a magic phone.
    public enum PhoneCallState {
        Idle,
        Ringing,
        Calling
    }

    private PhoneCallState _phoneCallState = PhoneCallState.Idle;
    public PhoneCallState PhoneCallStateNow { get { return _phoneCallState; } private set { _phoneCallState = value; } }


    [SerializeField] private AudioSource _ringSound;
    [SerializeField] private List<DialogueEventSO> _codeText = default;
    [SerializeField] private GameObject _phoneOn;
    [SerializeField] private GameObject _phoneOff;
    public int CurrentCodePosition { get; private set; }
    private bool _firstTime = false;
    private void Start() {
        _phoneOff.SetActive(false);
        _phoneOn.SetActive(true);
    }

    protected override void OnMouseDown() {
        // Make sure the phone is ringing
        if(_ringSound.isPlaying) {
            // stop ringing
            StopRing();
            PhoneOffHolder();
            if (_firstTime) {
                DialogueManager.Instance.AddDialogueEventToStack(_codeText[CurrentCodePosition]);
                StartCoroutine(WaitForDialogueEnd());
            }
        }
    }

    private IEnumerator WaitForDialogueEnd() {
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);
        PhoneOnHolder();
    }

    // Activate ringsound and also specify what will be played when answered
    public void Ring() {
        if(!_ringSound.isPlaying) {
            _phoneCallState = PhoneCallState.Ringing;
            _ringSound.Play();
            _phoneOn.GetComponent<Vibration>().StartVibration();
        }
    }
    private void StopRing() {
        _phoneCallState = PhoneCallState.Idle;
        _ringSound.Stop();
    }

    public void PhoneOnHolder() {
        _phoneOff.SetActive(false);
        _phoneOn.SetActive(true);
    }
    public void PhoneOffHolder() {
        _phoneOff.SetActive(true);
        _phoneOn.SetActive(false);
    }
    public void SetSoundClipCodeOrder(int i) {
        _firstTime = true;
        CurrentCodePosition = i;
    }
}
