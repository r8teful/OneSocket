using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MenuBase {
    private Button _buttonMainMenu;

    protected override void Start() {
        base.Start();
        _buttonMainMenu = GameObject.Find("ButtonMainMenu").GetComponent<Button>();
        if (_buttonMainMenu != null) _buttonMainMenu.onClick.AddListener(OnButtonMainMenuClicked);
    }

    private void OnButtonMainMenuClicked() {
        if (_playButton != null) {
            _playButton.interactable = false;
        }
        StartCoroutine(SceneHandler.Instance.FadeOutAndLoadScene(this,0));
    }
}
