using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseMenu : Singleton<LoseMenu> {
    [SerializeField] private AlphaLerp _fadeOut;
    private Button _buttonRetry;
    private Button _buttonMainMenu;
    private void Start() {
        _buttonRetry = GameObject.Find("ButtonRetry").GetComponent<Button>();
        _buttonMainMenu = GameObject.Find("ButtonMainMenu").GetComponent<Button>();


        if (_buttonRetry != null) _buttonRetry.onClick.AddListener(OnButtonRetryClick);
        if (_buttonMainMenu != null) _buttonMainMenu.onClick.AddListener(OnButtonMainMenuClicked);

        _fadeOut.gameObject.SetActive(true);
        AudioController.Instance.SetLoopAndPlay("mainMenu", 0);
        StartCoroutine(_fadeOut.GetComponent<AlphaLerp>().Fade(true));
        //_fadeOut = GameObject.Find("FadeOut").GetComponent<AlphaLerp>();
    }

    public IEnumerator FadeOut() {
        _fadeOut.gameObject.SetActive(true);
        yield return StartCoroutine(_fadeOut.GetComponent<AlphaLerp>().Fade(false));
    }

    private void OnButtonRetryClick() {
        SceneHandler.Instance.PlayClickedLoseMenu();
    }
    private void OnButtonMainMenuClicked() {
        SceneHandler.Instance.LoadMainMenu();
    }
}
