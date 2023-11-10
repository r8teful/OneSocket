using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : PersistentSingleton<SceneHandler> {

    public void LoadPlayScene() {
        SceneManager.LoadScene(0);
    }

    public void PlayClicked() {
        StartCoroutine(PlayClickedRoutine());
    }

    private IEnumerator PlayClickedRoutine() {
        AudioController.Instance.FadeOutLoop(4);
        MainMenu.Instance.FadeOut();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    public void LoadScene(int sceneBuildIndex) {
        SceneManager.LoadScene(sceneBuildIndex);
    }
}