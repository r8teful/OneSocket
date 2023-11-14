using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class AlphaLerp : MonoBehaviour {
    private RawImage rawImage;
    [SerializeField] private bool _isFadeIn;

    void Start() {
        rawImage = GetComponent<RawImage>();
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, _isFadeIn ? 1f : 0f);
    }
    private void OnEnable() {
        rawImage = GetComponent<RawImage>();
    }

    public IEnumerator Fade(bool fadeIn, float duration=2.5f) {
        float startAlpha = fadeIn ? 1f : 0f;
        float endAlpha = fadeIn ? 0f : 1f;
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, startAlpha);
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float lerpValue = Mathf.Clamp01(elapsedTime / duration);
            float alpha = Mathf.Lerp(startAlpha, endAlpha, lerpValue);

            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, alpha);
            yield return null;
        }
    }
}