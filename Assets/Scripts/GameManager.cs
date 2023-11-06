using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Phone;

public class GameManager : StaticInstance<GameManager> {


    [SerializeField] private bool debugDontStartIntro = false;
    [SerializeField] private GameObject firstSequencer;

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

        firstSequencer.SetActive(true);
        
    }

    public void RingPhone() {
        _phone.Ring();
    }

    public PhoneCallState GetPhoneCallState() {
        return _phone.PhoneCallStateNow;
    }


}
