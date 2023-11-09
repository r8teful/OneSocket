using System;
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
    [SerializeField] private AudioSource _callSound;
    [SerializeField] private List<DialogueEventSO> _codeText = default;
    [SerializeField] private GameObject _phoneOn;
    [SerializeField] private GameObject _phoneOff;
    private int _codePosition;
    //private int _currentCode;
    public int CurrentCode { get; private set; }

    private void Start() {
        _phoneOff.SetActive(false);
        _phoneOn.SetActive(true);
    }

    protected override void OnMouseDown() {
        Debug.Log("Clicked on Phone");
        // Make sure the phone is ringing
        if(_ringSound.isPlaying) {
            // stop ringing
            StopRing();
            PhoneOffHolder();
            if (_callSound == null) {
                //throw new ArgumentException("Parameter is null, need to define _callSound before calling Ring()", nameof(_callSound));
            } else {
                // _callSound.Play();
                // _callSound.volume *= 2f;
               // DialogueManager.Instance.AddDialogueEventToStack(_codeText[CurrentCode]);
                DialogueManager.Instance.AddDialogueEventToStack(_codeText[_codePosition]);
            }
        }
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
        _codePosition = i;
    }
}
