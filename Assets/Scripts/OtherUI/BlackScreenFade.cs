using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BlackScreenFade : MonoBehaviour
{
    [SerializeField] Image panel;
    [SerializeField] AnimationCurve fadeCurve;
    [SerializeField] float fadeDuration;
    float timer;
    bool transitioning = false;

    UnityAction midTransitionAction;
    UnityAction endTransitionAction;


    private void Update() {
        FadeUpdate();

    }

    public void FadeInOut(UnityAction midTransitionAction=null, UnityAction endTransitionAction=null) {
        if(!transitioning) {
            transitioning = true;
            timer = 0;
            this.midTransitionAction = midTransitionAction;
            this.endTransitionAction = endTransitionAction;

            gameObject.SetActive(true);
        }
    }

    public void FadeIn(UnityAction midTransitionAction = null, UnityAction endTransitionAction = null) {
        if (!transitioning) {
            transitioning = true;
            timer = fadeDuration/2;
            this.midTransitionAction = midTransitionAction;
            this.endTransitionAction = endTransitionAction;

            gameObject.SetActive(true);
        }
    }

    private void FadeUpdate() {
        if (transitioning) {
            timer += Time.deltaTime;

            // Update color
            float alphaValue = fadeCurve.Evaluate((timer * 2) / fadeDuration);
            panel.color = new Color(0, 0, 0, alphaValue);

            // Triggering actions
            if (midTransitionAction != null && timer/fadeDuration > 0.5f) {
                midTransitionAction.Invoke();
                midTransitionAction = null;
            }

            // ending value
            if (timer >= fadeDuration) {
                if (endTransitionAction != null) {
                    endTransitionAction.Invoke();
                    endTransitionAction = null;
                }
                transitioning = false;
                gameObject.SetActive(false);
            }
        }
    }
}
