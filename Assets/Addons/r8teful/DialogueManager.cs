using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager> {
    public bool NoDialoguePlaying => eventStack.Count == 0;
    public DialogueEventSO CurrentEvent => NoDialoguePlaying ? null : eventStack.Peek();

    [SerializeField]
    private DialogueText dialogueText = default;

    [SerializeField]
    private DialogueEventSO resumeLines = default;

    private Stack<DialogueEventSO> eventStack = new Stack<DialogueEventSO>();
    private List<DialogueEventSO> playedEvents = new List<DialogueEventSO>();

    private void Start() {
        dialogueText.DisplayCharacter += OnDisplayCharacter;
        //dialogueText.CompletedLine += OnMessageEnded;
        //dialogueText.EmotionChange += OnEmotionChange;
    }

    public void AddDialogueEventToStack(DialogueEventSO dialogueEvent) {
        if (!playedEvents.Contains(dialogueEvent)) {
            if (eventStack.Count == 0 || eventStack.Peek().interruptBehaviour != DialogueEventSO.InterruptBehaviour.Uninterruptable) {
                StartCoroutine(PlayDialogueEvent(dialogueEvent));
            }
        }
    }

    private IEnumerator PlayDialogueEvent(DialogueEventSO dialogueEvent) {
        playedEvents.Add(dialogueEvent);

        eventStack.Push(dialogueEvent);
        dialogueText.Clear();

        bool continueToNextLine = true;
        for (int i = 0; i < dialogueEvent.lines.Count; i++) {
            if (eventStack.Peek() != dialogueEvent) {
                yield return new WaitWhile(() => eventStack.Peek() != dialogueEvent);

                switch (dialogueEvent.interruptBehaviour) {
                    case DialogueEventSO.InterruptBehaviour.Skip:
                        continueToNextLine = false;
                        break;
                    case DialogueEventSO.InterruptBehaviour.ResumeAfter:
                        i = Mathf.Max(i - 1, 0);
                        dialogueText.PlayMessage(resumeLines.lines[Random.Range(0, resumeLines.lines.Count)]);
                        yield return new WaitUntil(() => !dialogueText.PlayingMessage);
                        break;
                }
            }

            var line = dialogueEvent.lines[i];
            if (continueToNextLine) {
                dialogueText.PlayMessage(line);
                yield return new WaitUntil(() => !dialogueText.PlayingMessage || eventStack.Peek() != dialogueEvent);
                yield return new WaitForSeconds(0.15f);
            }
        }

        yield return new WaitWhile(() => eventStack.Peek() != dialogueEvent);
        eventStack.Pop();
        if (eventStack.Count == 0) {
            yield return new WaitForSeconds(0.75f);
            if (eventStack.Count == 0) {
                dialogueText.Clear();
            }
        }
    }
    private void OnDisplayCharacter(string message, int index) {
        //mouth.ToggleOpen();
        AudioController.Instance.PlaySound2D("buddy_voice_1", pitch: new AudioParams.Pitch(AudioParams.Pitch.Variation.VerySmall),
            repetition: new AudioParams.Repetition(0.075f));
    }

    /*
    private void OnEmotionChange(Emotion emotion) {
        eyes.SetEmotion(emotion);
        mouth.SetEmotion(emotion);
    }

    
    private void OnMessageEnded() {
        mouth.SetOpen(false);
    }*/
}
