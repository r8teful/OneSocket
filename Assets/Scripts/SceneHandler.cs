using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : PersistentSingleton<SceneHandler> {

    public int PreviousLoadedSceneIndex { get; private set; }

    public void LoadPlayScene() {
        SceneManager.LoadScene(1);
    }
    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }
    public void RestartGame() {
        SceneManager.LoadScene(1);
        // Do other restart things? Such as, dont play intro, tutorial etc
    }
    
    public void LoadWinMenu() {
        PreviousLoadedSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(3);
    }
    public void LoadLoseMenu() {
        PreviousLoadedSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(2);
    }

    // Main Menu -> Game, fade
    // Pause Menu -> Main Menu, play loop
    // Lose Screen, play -> Main Menu, dont fade
    // Win Screen,  play -> Main Menu, dont fade
    // Lose Screen, play -> Game, fade
    // Win Screen,  play -> Game, fade
    public IEnumerator FadeOutAndLoadScene(MenuBase m,int sceneBuildIndex,float fadeDuration = 4) {
        PreviousLoadedSceneIndex = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneBuildIndex);
        async.allowSceneActivation = false;
        string transitionType = FadeCheck(PreviousLoadedSceneIndex, sceneBuildIndex);
        switch (transitionType) {
            case "fade out":
                AudioController.Instance.FadeOutLoop(fadeDuration);
                break;
            case "play loop":
                PlayMainMenuLoop();
                break;
            case "no fade":
                break;
            default:
                // Will happen with same scene transitions, for example, restarting a game
                break;
        }
        yield return StartCoroutine(m.FadeOut());
        async.allowSceneActivation = true;
        yield return async;
    }

    private string FadeCheck(int previous, int next) {
        if(previous == 0 && next == 1) 
            return "fade out"; // Main Menu -> Game, fade
        if ((previous == 2 || previous == 3) && next == 1)
            return "fade out";  // Lose Screen/Win Screen, play -> Game, fade
        if (previous == 1 && next == 0) 
            return "play loop";  // Pause Menu -> Main Menu, play loop
        if ((previous == 2 || previous == 3) && next == 0) 
            return "no fade";  // Lose Screen/Win Screen, play -> Main Menu, dont fade
        return "";
    }

    public void LoadScene(int sceneBuildIndex) {
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void PlayMainMenuLoop() {
        if (!AudioController.Instance.IsLoopPlaying("mainMenu")) {
            Debug.Log("PlayingLoop");
            AudioController.Instance.SetLoopAndPlay("mainMenu", 0);
        } else {
            Debug.Log("Loop already playing!");
            // Assume we already started the loop, so just resume
            AudioController.Instance.FadeInLoop(4,1);
        }
    }

    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.W)) {
            GameManager.Instance.GameWin();
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L)) {
            GameManager.Instance.GameLose();
        }

        ClickArea[] clickAreas = FindObjectsOfType<ClickArea>();

        foreach (ClickArea clickArea in clickAreas) {
            if (clickArea.gameObject.GetComponent<DrawBoxcollider>() != null) return;
            clickArea.gameObject.AddComponent<DrawBoxcollider>();
        }
    }
}