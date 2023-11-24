using System.Xml.Serialization;
using UnityEngine;

public class GameManager : StaticInstance<GameManager> {

#if UNITY_EDITOR
    [SerializeField] private bool debugDontStartIntro = false;
    [SerializeField] private bool debugNoMonster = false;
    [SerializeField] private bool debugWinGame = false;
    [SerializeField] private bool debugLoseGame = false;
#endif
    [SerializeField] private GameObject _introSequencer;
    [SerializeField] private GameObject _winSequencer;
    [SerializeField] private GameObject _loseSequencer;
    [SerializeField] private GameObject _monster;

    [SerializeField] private Phone _phone;
    [SerializeField] private Clock _clock;
    [SerializeField] private Speaker _speaker;
    [SerializeField] private int[] _code;
    
 
    public int[] Code { get { return _code; } private set { _code = value; } }


    protected override void Awake() {
        base.Awake();
        if (_code == null || _code.Length != 3) {
            _code = new int[3];
            _code[0] = Random.Range(0, 10);
            _code[1] = Random.Range(0, 10);
            _code[2] = Random.Range(0, 10);
        }
    }
 

    private void Start() {
#if UNITY_EDITOR
        if (debugDontStartIntro) {
            return;
        }
#endif

        _introSequencer.SetActive(true);

#if UNITY_EDITOR
        if (debugNoMonster) {
            return;
        }
#endif
        _monster.SetActive(true);
    }

#if UNITY_EDITOR
    private void Update() {
        if (debugWinGame) {
            GameWin();
        }
        if (debugLoseGame) {
            GameLose();
        }
    }
#endif
    public void RingPhone() {
        _phone.Ring();
    }
    public void PhoneOnHolder() {
        _phone.PhoneOnHolder();
    }
    public Phone.PhoneCallState GetPhoneCallState() {
        return _phone.PhoneCallStateNow;
    }

    public void ResetClockTime() {
        _clock.ResetTimer();
    }

    public void SetSpeakerCharge(int n) {
        _speaker.Charges = n;
    }
    
    public void GameWin() {
        _winSequencer.SetActive(true);
    }

    public void GameLose() {
        _loseSequencer.SetActive(true);
    }
    public int GetCurrentCode() {
        return Code[_phone.CurrentCodePosition];
    }
    public void SetMonster(bool n) {
        _monster.SetActive(n);
    }
}