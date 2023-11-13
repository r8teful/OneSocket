using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : StaticInstance<PauseMenu> {
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;

    private bool _isPaused = false;


    private void Start() {
        _restartButton.onClick.AddListener(OnRestartClick);
        _mainMenuButton.onClick.AddListener(OnMainMenuClick);
    }

    private void OnRestartClick() {
        ResumeGame();
        SceneHandler.Instance.RestartGame();
    }
    private void OnMainMenuClick() {
        ResumeGame();
        SceneHandler.Instance.LoadMainMenu();
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
        _pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        if (_isPaused) _isPaused = !_isPaused;
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
