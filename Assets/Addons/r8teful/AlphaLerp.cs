using UnityEngine;
using UnityEngine.UI;

public class AlphaLerp : MonoBehaviour {
    [SerializeField] private float duration = 2f;
    private float elapsedTime = 0f;
    private bool isFading = false;

    private RawImage rawImage;

    void Start() {
        rawImage = GetComponent<RawImage>();
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 0f);
    }


    void Update() {
        if (isFading) {
            if (elapsedTime < duration) {
                elapsedTime += Time.deltaTime;
                float lerpValue = Mathf.Clamp01(elapsedTime / duration);
                float alpha = Mathf.Lerp(0f, 1f, lerpValue);
                rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, alpha);
            } else {
                // Reset the flag and any other necessary state after fading is complete
                isFading = false;
            }
        }
    }

    public void StartFade() {
        // Reset elapsed time and set the flag to start fading
        elapsedTime = 0f;
        isFading = true;
    }
}