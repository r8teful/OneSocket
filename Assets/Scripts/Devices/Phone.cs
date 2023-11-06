using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [SerializeField] private AudioClip[] _codeClips;


    protected override void OnMouseDown() {
        Debug.Log("Clicked on Phone");
        // Make sure the phone is ringing
        if(_ringSound.isPlaying) {
            // stop ringing
            _phoneCallState = PhoneCallState.Idle;
            _ringSound.Stop();
            
            if(_callSound == null) {
                //throw new ArgumentException("Parameter is null, need to define _callSound before calling Ring()", nameof(_callSound));
            } else {
                _callSound.Play();
                _callSound.volume *= 2f;
            }
        }
    }

    // Activate ringsound and also specify what will be played when answered
    public void Ring() {
        if(!_ringSound.isPlaying) {
            _phoneCallState = PhoneCallState.Ringing;
            _ringSound.Play();
        }
    }

    public void SetSoundClipCodeOrder(int i) {
        _callSound.clip = _codeClips[GameManager.Instance.Code[i]];
    }
}
