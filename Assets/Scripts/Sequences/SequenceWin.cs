using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceWin : Sequencer {

    [SerializeField] private GameObject _blackScreen;

    // Enable black screen, Another narrative speaks? Not phone guy.
    protected override IEnumerator Sequence() {
        _blackScreen.SetActive(true);
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[0]);
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);
        // todo back to main menu
    }
}
