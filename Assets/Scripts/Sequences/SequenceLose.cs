using System.Collections;
using UnityEngine;

public class SequenceLose : Sequencer {

    [SerializeField] private GameObject _blackScreen;
    protected override IEnumerator Sequence() {
        JumpScare.Instance.Scare();
        yield return new WaitForSeconds(2f);
        _blackScreen.SetActive(true);
        yield return StartCoroutine(_blackScreen.GetComponent<AlphaLerp>().Fade(false));
        DialogueManager.Instance.DialogueSpeaker = DialogueManager.Speaker.DisplayText;
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[0]);
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);
        SceneHandler.Instance.LoadLoseMenu();
    }
}
