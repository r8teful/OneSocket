using System.Collections;
using UnityEngine;

public class Phone : Interactable {
    // Has charge as long as clock is connected
    public enum PhoneStatedCode {
        None,
        First,
        Second, 
        Third
    }

    private enum PhoneCallState {
        Idle,
        Ringing,
        Calling
    }

    public static Phone Instance;
    public static PhoneStatedCode PhoneState = PhoneStatedCode.None;
    private PhoneCallState _phoneCallState = PhoneCallState.Idle;
    [SerializeField] private AudioSource _ringSound;
    [SerializeField] private AudioSource _callSound;
    [SerializeField] private AudioClip[] _codeClips;
    private bool _canInteract = false;
    private bool _saidFirstCode = false;
    private bool _saidSecondCode = false;
    private bool _saidThirdCode = false;

    [SerializeField] private int[] _code;
    public int[] Code { get { return _code; } private set { _code = value; }}

    private void Awake() {
        Instance = this;
      //  Generator.GeneratorStatus += OnOutOfCharge;

        if (_code == null || _code.Length != 3) {
            _code = new int[3];

            _code[0] = UnityEngine.Random.Range(0, 10);
            _code[1] = UnityEngine.Random.Range(0, 10);
            _code[2] = UnityEngine.Random.Range(0, 10);
        }
    }

   //TODO Will be broken
   // private void OnDestroy() {
   //     Socket.GeneratorStatus -= OnOutOfCharge;
   // }

   // private void OnOutOfCharge(bool b) {
   //     if (type == PlugType.Clock && _phoneCallState.Equals(PhoneCallState.Ringing)) {
   //        // I'm effected!
   //         StopRing();
   //         // Wait until the generator has charge again
   //         StartCoroutine(WaitForCharge());
   //     }

   // }

    private IEnumerator WaitForCharge() {
        while (Generator.Instance.GeneratorCharge <= 0) {
            if (Socket.Instance.CurrentPlug == PlugType.Empty) {
                // exit out of whole function
                yield break;
            }
            yield return null;
        }
        // Maybe some slow buildup?
        Ring();
    }
    private void Update() {
       // if (_phoneCallState.Equals(PhoneCallState.Ringing)) {
       //     // Power goes out or 
       //     Clock.Instance.GetHasPower()
       // }
       //     
    }

    public void OnPlugDisconnectedPhone() {
        if (_phoneCallState.Equals(PhoneCallState.Ringing)) {
            StopRing();
            StartCoroutine(WaitForOutlet());
        }
    }
    private IEnumerator WaitForOutlet() {
        while (Socket.Instance.CurrentPlug != PlugType.Clock) {
            yield return null;
        }
        // Maybe some slow buildup?
        Ring();
    }
    protected override void OnMouseDown() {
        Debug.Log("Clicked on Phone");
        if(_canInteract) {
            // pickup phone don't care about sound
            // decide which clip we need to play this is so so bad but who cares?
            if (PhoneState.Equals(PhoneStatedCode.First) && !_saidFirstCode) {
                StopRing();
                _callSound.clip = _codeClips[Code[0]];
                _callSound.volume *= 2f;
                _callSound.Play();
                _saidFirstCode = true;
            } else if (PhoneState.Equals(PhoneStatedCode.Second) && !_saidSecondCode) {
                StopRing();
                _callSound.clip = _codeClips[Code[1]];
                _callSound.volume *= 2f;
                _callSound.Play();
                _saidSecondCode = true;
            } else if (PhoneState.Equals(PhoneStatedCode.Third) && !_saidThirdCode) {
                StopRing();
                _callSound.clip = _codeClips[Code[2]];
                _callSound.volume *= 2f;
                _callSound.Play();
                _saidThirdCode = true;
            }
        }
    }

    public void Ring() {
        if(!_ringSound.isPlaying) {
            _ringSound.Play();
            _phoneCallState = PhoneCallState.Ringing;
        }
        _canInteract = true;

    }

    public void StopRing() {
        Debug.Log("Stop ring!");
        _ringSound.Stop();

        _phoneCallState = PhoneCallState.Idle;
        _canInteract = false;
    }
}
