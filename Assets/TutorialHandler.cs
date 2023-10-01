using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour {
    public static TutorialHandler Instance;
    public GameObject[] tutorialPanels;
    private int currentStep = 0;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        ShowTutorialStep(currentStep);
    }

    public void NextStep() {
        currentStep++;

        if (currentStep < tutorialPanels.Length) {
            ShowTutorialStep(currentStep);
        } else {
            // All tutorials are completed, you can hide the tutorial canvas or perform other actions.
            // You may also want to save that the tutorials have been completed for this session or player.
            HideTutorial();
        }
    }

    private void ShowTutorialStep(int stepIndex) {
        for (int i = 0; i < tutorialPanels.Length; i++) {
            tutorialPanels[i].SetActive(i == stepIndex);
        }
    }

    private void HideTutorial() {
        // Hide the tutorial canvas or perform other actions as needed.
        gameObject.SetActive(false);
    }
}