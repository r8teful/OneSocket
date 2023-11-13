using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : StaticInstance<MainMenu> {

    [SerializeField] private AlphaLerp _fadeOut;
    [SerializeField] private Button _playButton;

    private void Start() {
        _playButton = GameObject.Find("ButtonPlay").GetComponent<Button>();
        _fadeOut = GameObject.Find("FadeOut").GetComponent<AlphaLerp>();
        if (_playButton!=null) _playButton.onClick.AddListener(OnPlayClick);
        AudioController.Instance.SetLoopAndPlay("mainMenu",0);
        _fadeOut.gameObject.SetActive(false);
    }
    public IEnumerator FadeOut() {
        _fadeOut.gameObject.SetActive(true);
        yield return StartCoroutine(_fadeOut.GetComponent<AlphaLerp>().Fade());
    }

    private void OnPlayClick() {
        SceneHandler.Instance.PlayClicked();
    }
}