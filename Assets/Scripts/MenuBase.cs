using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class MenuBase : MonoBehaviour {
    private AlphaLerp _fadeOut;
    protected Button _playButton;
    public IEnumerator FadeOut() {
        _fadeOut = GameObject.FindWithTag("FadeOut").GetComponent<AlphaLerp>();
        if (_fadeOut == null) yield break;
        _fadeOut.gameObject.SetActive(true);
        yield return StartCoroutine(_fadeOut.GetComponent<AlphaLerp>().Fade(false));
    }

    protected virtual void Start() {
        _fadeOut = GameObject.FindWithTag("FadeOut").GetComponent<AlphaLerp>();
        _playButton = GameObject.FindWithTag("ButtonPlay").GetComponent<Button>();
        if (_playButton != null) _playButton.onClick.AddListener(OnButtonPlayClick);
        if (_fadeOut != null) _fadeOut.gameObject.SetActive(true);
        SceneHandler.Instance.PlayMainMenuLoop();
        // Fade from black to menu
        StartCoroutine(_fadeOut.Fade(true));
    }

    protected virtual void OnButtonPlayClick() {
        if (_playButton != null) {
            _playButton.interactable = false;
        }
        StartCoroutine(SceneHandler.Instance.FadeOutAndLoadScene(this,1));
    }
}