using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceTutorial : Sequencer {

    [SerializeField] private GameObject _socketLight;
    [SerializeField] private GameObject _phoneLight;
    private ClickArea[] _clickAreas;
    protected override IEnumerator Sequence() {
        _clickAreas = FindObjectsOfType<ClickArea>();
        foreach (ClickArea clickArea in _clickAreas) {
            clickArea.SetInteraction(false);
        }
        GameManager.Instance.DisableMonster();
        _socketLight.SetActive(false);
        _phoneLight.SetActive(true);

        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[0]);
        yield return new WaitUntil(() => CameraMovement.Instance.LookState != CameraMovement.CameraState.Room);
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[1]);
        yield return new WaitUntil(() => CameraMovement.Instance.LookState == CameraMovement.CameraState.Generator);
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[2]);
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);
        // Charge battery
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[3]);
        yield return new WaitUntil(() => Generator.Instance.IsMaxCharge);
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[4]);
        //yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);
        yield return new WaitUntil(() => CameraMovement.Instance.LookState == CameraMovement.CameraState.Room);
        
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[5]);

        yield return new WaitForSeconds(1);
        _socketLight.SetActive(true);
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);
        // Click on light
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[6]);


        _clickAreas = FindObjectsOfType<ClickArea>();
        foreach (ClickArea clickArea in _clickAreas) {
            if (clickArea.CompareTag("Light")) {
                clickArea.SetInteraction(true);
            }
        }
        Generator.Instance.DissableDischarge();

        yield return new WaitUntil(() => (Socket.Instance.CurrentPlug == PlugType.LampClose) || Socket.Instance.CurrentPlug == PlugType.LampFar);

        if(Socket.Instance.CurrentPlug == PlugType.LampFar) {
            // Good, now plug in the close one
            DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[7]);
            yield return new WaitUntil(() => (Socket.Instance.CurrentPlug == PlugType.LampClose));
        } else {
            // Continue 
            DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[8]);

            yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);
        }
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[9]);

        // Code wall 

        _clickAreas = FindObjectsOfType<ClickArea>();
        foreach (ClickArea clickArea in _clickAreas) {
            if (clickArea.CompareTag("Light")) {
                clickArea.SetInteraction(false);
            }
        }

        yield return new WaitUntil(() => CameraMovement.Instance.LookState == CameraMovement.CameraState.Code);
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[10]);

        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);

        _clickAreas = FindObjectsOfType<ClickArea>();
        foreach (ClickArea clickArea in _clickAreas) {
            if (clickArea.CompareTag("Clock") || clickArea.CompareTag("Light")) {
                clickArea.SetInteraction(true);
            }
        }
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[11]);
        yield return new WaitUntil(() => Socket.Instance.CurrentPlug == PlugType.Clock);

        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[12]);
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);

        _clickAreas = FindObjectsOfType<ClickArea>();
        foreach (ClickArea clickArea in _clickAreas) {
            if (clickArea.CompareTag("Speaker")) {
                clickArea.SetInteraction(true);
            }
        }
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[13]);
        yield return new WaitUntil(() => Socket.Instance.CurrentPlug == PlugType.Speaker);
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[14]);

        // Should check if speaker is out of charge, maybe give more charge?
        yield return new WaitUntil(() => Socket.Instance.CurrentPlug == PlugType.Empty);
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[15]);
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);
        yield return new WaitUntil(() => Socket.Instance.CurrentPlug == PlugType.LampClose);
        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[16]);
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);

        DialogueManager.Instance.AddDialogueEventToStack(dialogueEvents[17]);
        yield return new WaitUntil(() => DialogueManager.Instance.NoDialoguePlaying);
        // Setup Ready for game
        _clickAreas = FindObjectsOfType<ClickArea>();
        foreach (ClickArea clickArea in _clickAreas) {
            clickArea.SetInteraction(true);
        }
        GameManager.Instance.PhoneOnHolder();
    }
}
