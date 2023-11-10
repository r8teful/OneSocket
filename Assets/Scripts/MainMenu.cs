using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : StaticInstance<MainMenu> {

    [SerializeField] private GameObject _fadeOut;

    private void Start() {
        AudioController.Instance.SetLoopAndPlay("mainMenu",0);
        _fadeOut.SetActive(false);
    }
    public void FadeOut() {
        _fadeOut.SetActive(true);
        _fadeOut.GetComponent<AlphaLerp>().StartFade();
    }
}