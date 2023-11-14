using System.Collections;
using UnityEngine;

public class SequenceWin : Sequencer {

    [SerializeField] private GameObject _blackScreen;

    protected override IEnumerator Sequence() {
        _blackScreen.SetActive(true);
        yield return StartCoroutine(_blackScreen.GetComponent<AlphaLerp>().Fade(false));
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[0]);
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);
        SceneHandler.Instance.LoadWinMenu();
    }
}
