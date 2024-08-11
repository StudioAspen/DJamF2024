using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    private PlayerManager player;

    [Header("Health")]
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Image playerHealthSliderFillImage;
    [SerializeField] private TMP_Text playerHealthText;

    [Header("Money")]
    [SerializeField] private TMP_Text playerMoneyText;
    [SerializeField] private PlayerMoneySubtractText playerMoneySubtractText;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerManager>();
    }

    private void Update()
    {
        HandlePlayerHealthUI();
        HandlePlayerMoneyUI();
    }

    private void HandlePlayerHealthUI()
    {
        float playerHealthPercent = player.CurrentHealth / player.MaxHealth;

        playerHealthSlider.value = Mathf.Lerp(playerHealthSlider.value, playerHealthPercent, 10f * Time.unscaledDeltaTime);

        playerHealthText.text = $"{Mathf.Round(player.CurrentHealth * 100)/100}/{player.MaxHealth}";
    }

    private void HandlePlayerMoneyUI()
    {
        playerMoneyText.text = $"Mushroom Caps: {player.Money}";
    }

    public void PlayMoneySubtractAnimation(int amount)
    {
        PlayerMoneySubtractText text = Instantiate(playerMoneySubtractText, playerMoneyText.rectTransform.position + 50f * Vector3.down, Quaternion.identity, transform);
        text.PlayMoneySubtractAnimation(amount);
    }
}
