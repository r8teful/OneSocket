using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Phone;

public class GameManager : StaticInstance<GameManager> {


    [SerializeField] private bool debugDontStartIntro = false;
    [SerializeField] private bool debugNoMonster = false;
    [SerializeField] private GameObject _introSequencer;
    [SerializeField] private GameObject _winSequencer;
    [SerializeField] private GameObject _loseSequencer;
    [SerializeField] private GameObject _monster;

    [SerializeField] private Phone _phone;
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

    public void RingPhone() {
        _phone.Ring();
    }
    public void PhoneOnHolder() {
        _phone.PhoneOnHolder();
    }
    public PhoneCallState GetPhoneCallState() {
        return _phone.PhoneCallStateNow;
    }

    public void GameWin() {
        _winSequencer.SetActive(true);
    }

    public void GameLose() {
        _loseSequencer.SetActive(true);
    }
    public int GetCurrentCode() {
        return _phone.CurrentCode;
    }
}
