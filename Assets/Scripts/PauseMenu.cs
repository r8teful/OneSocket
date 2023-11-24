using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MenuBase {

    private GameObject _pauseMenuPanel;
    private Button _mainMenuButton;
    private Button _resumeMenuButton;
    private bool _isPaused = false;


    protected override void Start() {
        _pauseMenuPanel = transform.Find("PauseMenuPanel").gameObject;
        _playButton = transform.Find("PauseMenuPanel/buttonGroup/ButtonPlay").GetComponent<Button>();
        _mainMenuButton = transform.Find("PauseMenuPanel/buttonGroup/MainMenu").GetComponent<Button>();
        _resumeMenuButton = transform.Find("PauseMenuPanel/Resume").GetComponent<Button>();
        _playButton.onClick.AddListener(OnButtonPlayClick);
        _mainMenuButton.onClick.AddListener(OnMainMenuClick);
        _resumeMenuButton.onClick.AddListener(ResumeGame);
    }

    protected override void OnButtonPlayClick() {
        ResumeGame();
        base.OnButtonPlayClick();
    }

    private void OnMainMenuClick() {
        ResumeGame();
        if (_playButton != null) {
            _playButton.interactable = false;
        }
        StartCoroutine(SceneHandler.Instance.FadeOutAndLoadScene(this,0));
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _isPaused = !_isPaused;
            if (_isPaused) {
                PauseGame();
            } else {
                ResumeGame();
            }
        }
    }

    public void PauseGame() {
        if (!_isPaused) _isPaused = !_isPaused;
        _pauseMenuPanel.transform.SetAsLastSibling();
        _pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        ClickArea[] clickAreas = FindObjectsOfType<ClickArea>();
        foreach (ClickArea clickArea in clickAreas) {
            clickArea.SetInteraction(false);
        }
    }

    public void ResumeGame() {
        if (_isPaused) _isPaused = !_isPaused;
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        ClickArea[] clickAreas = FindObjectsOfType<ClickArea>();
        foreach (ClickArea clickArea in clickAreas) {
            clickArea.SetInteraction(true);
        }
    }
}