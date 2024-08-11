using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMoneySubtractText : MonoBehaviour
{
    private TMP_Text playerMoneySubtractText;

    private void Awake()
    {
        playerMoneySubtractText = GetComponent<TMP_Text>();
    }

    public void PlayMoneySubtractAnimation(int amount)
    {
        playerMoneySubtractText.text = $"-{amount} Caps";

        StartCoroutine(MoneySubractAnimationCoroutine());
    }

    private IEnumerator MoneySubractAnimationCoroutine()
    {
        playerMoneySubtractText.gameObject.SetActive(true);

        Vector3 startPos = playerMoneySubtractText.rectTransform.localPosition;
        Vector3 endPos = playerMoneySubtractText.rectTransform.localPosition + 50f * Vector3.down;

        Color startColor = playerMoneySubtractText.color;

        for (float t = 0; t < 1f; t += Time.unscaledDeltaTime)
        {
            playerMoneySubtractText.rectTransform.localPosition = Vector3.Lerp(startPos, endPos, t);
            playerMoneySubtractText.color = Color.Lerp(startColor, Color.clear, t);

            yield return null;
        }
        playerMoneySubtractText.rectTransform.localPosition = endPos;
        playerMoneySubtractText.color = Color.clear;

        Destroy(gameObject);
    }
}
