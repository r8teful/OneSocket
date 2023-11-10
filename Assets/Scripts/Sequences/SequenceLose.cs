using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceLose : Sequencer {

    [SerializeField] private GameObject _blackScreen;
    protected override IEnumerator Sequence() {
        _blackScreen.SetActive(true);
        DialogueManager.Instance.DialogueSpeaker = DialogueManager.Speaker.DisplayText;
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[0]);
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);
    }
}
