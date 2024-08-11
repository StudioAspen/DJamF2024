using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuzzleFlash : MonoBehaviour
{

    [SerializeField] RectTransform image;
    [SerializeField] AnimationCurve scale;
    [SerializeField] float fadeDuration;
    [SerializeField] float scaleMag;
    float timer;
    bool transitioning = false;


    private void Update() {
        FadeUpdate();
    }

    public void FlashMuzzle() {
        if (!transitioning) {
            transitioning = true;
            timer = 0;

            gameObject.SetActive(true);
        }
    }


    private void FadeUpdate() {
        if (transitioning) {
            timer += Time.deltaTime;

            // Update color
            float value = scale.Evaluate((timer * 2) / fadeDuration);
            image.localScale = scaleMag*new Vector3(value, value, value);

            // ending value
            if (timer >= fadeDuration) {
                transitioning = false;
                gameObject.SetActive(false);
            }
        }
    }
}
