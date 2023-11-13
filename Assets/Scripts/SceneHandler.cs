using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : PersistentSingleton<SceneHandler> {

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
    public void PlayClicked() {
        StartCoroutine(PlayClickedRoutine());
    }

    private IEnumerator PlayClickedRoutine() {
        AsyncOperation async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        AudioController.Instance.FadeOutLoop(4);    
        yield return StartCoroutine(MainMenu.Instance.FadeOut());
        async.allowSceneActivation = true;
        yield return async;
    }

    public void LoadScene(int sceneBuildIndex) {
        SceneManager.LoadScene(sceneBuildIndex);
    }
}