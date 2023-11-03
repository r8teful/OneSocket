using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceIntro : Sequencer {

    protected override IEnumerator Sequence() {
        Debug.Log("Starded intro sequence");
        yield return new WaitUntil(() => Input.anyKeyDown);

        Debug.Log("anyKeyDown");
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[0]);
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);

    }
}