using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScare : MonoBehaviour {
    public static JumpScare Instance;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _aaa;
    [SerializeField] private GameObject _Hand1;
    [SerializeField] private GameObject _Hand2;
    // Start is called before the first frame update
    private void Awake() {
        Instance = this;
        _Hand1.SetActive(false);
        _Hand2.SetActive(false);
    }

    // Update is called once per frame
    public void Scare() {
        _Hand1.SetActive(true);
        _Hand2.SetActive(true);
        _animator.SetTrigger("JumpScareEvent");
        _aaa.Play();
    }
}
