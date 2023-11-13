using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class AlphaLerp : MonoBehaviour {
    [SerializeField] private float duration = 10f;
    private RawImage rawImage;

    void Start() {
        rawImage = GetComponent<RawImage>();
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 0f);
    }
    private void OnEnable() {
        rawImage = GetComponent<RawImage>();
    }

    public IEnumerator Fade() {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            float lerpValue = Mathf.Clamp01(elapsedTime / duration);
            float alpha = Mathf.Lerp(0f, 1f, lerpValue);
            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, alpha);
            yield return null;
        }
    }
}