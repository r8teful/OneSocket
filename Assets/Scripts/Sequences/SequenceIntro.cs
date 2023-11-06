using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceIntro : Sequencer {

    [SerializeField] private GameObject _socketLight;
    [SerializeField] private GameObject _phoneLight;

    protected override IEnumerator Sequence() {
        Debug.Log("Starded intro sequence");

        //  set interactions off for plugs
        ClickArea[] clickAreas = FindObjectsOfType<ClickArea>();

        foreach (ClickArea clickArea in clickAreas) {
            clickArea.SetInteraction(false);
        }

        _socketLight.SetActive(false);
        _phoneLight.SetActive(true);
        GameManager.Instance.RingPhone();
        yield return new WaitUntil(() => GameManager.Instance.GetPhoneCallState().Equals(Phone.PhoneCallState.Idle));

        Debug.Log("Phone Idle");
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[0]);
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);

    }
}