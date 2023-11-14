using System.Collections;
using UnityEngine;

public class Vibration : MonoBehaviour {
    private float vibrationIntensity = 0.015f; // Adjust the intensity of the vibration
    private float vibrationSpeed = 500f; // Adjust the speed of the vibration
    private float vibrationDuration = 1f; // Adjust the duration of each vibration in seconds
    private float pauseDuration = 1f; // Adjust the duration of the pause between vibrations in seconds

    private Vector3 initialPosition;
    private bool isVibrating = false;

    private void Start() {
        initialPosition = transform.position;
    }

   // private void Update() {
   //     if (isVibrating) {
   //         float vibrationOffset = Mathf.Sin(Time.time * vibrationSpeed) * vibrationIntensity;
   //         transform.position = initialPosition + new Vector3(vibrationOffset,0f , 0f);
   //     }
   // }

    public void StartVibration() {
        if (!isVibrating) {
            isVibrating = true;
            StartCoroutine(VibrationLoop());
        }
    }

    public void StopVibration() {
        if (isVibrating) {
            isVibrating = false;
            StopAllCoroutines();
            transform.position = initialPosition;
        }
    }

    private IEnumerator VibrationLoop() {
        while (isVibrating) {
            float startTime = Time.time;

            // Continue vibrating for the specified duration
            while (Time.time - startTime < vibrationDuration) {
                // Simulate vibration by moving the object back and forth along the Y-axis
                float vibrationOffset = Mathf.Sin(Time.time * vibrationSpeed) * vibrationIntensity;
                transform.position = initialPosition + new Vector3(0f, vibrationOffset, 0f);

                yield return null;
            }

            // Reset position after each vibration duration
            transform.position = initialPosition;

            // Check if isVibrating is still true (it might have been set to false during the vibration)
            if (isVibrating) {
                // Wait for the specified pause duration
                yield return new WaitForSeconds(pauseDuration);
            }
        }
    }
}
