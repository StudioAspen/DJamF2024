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

        if (playerHealthPercent < 0.1f) playerHealthSliderFillImage.color = Color.Lerp(playerHealthSliderFillImage.color, Color.red, 5f * Time.deltaTime);
        else if (playerHealthPercent < 0.25f) playerHealthSliderFillImage.color = Color.Lerp(playerHealthSliderFillImage.color, Color.yellow, 5f * Time.deltaTime);
        else playerHealthSliderFillImage.color = Color.Lerp(playerHealthSliderFillImage.color, Color.green, 5f * Time.deltaTime);
    }

    private void HandlePlayerMoneyUI()
    {
        playerMoneyText.text = $"Money: {player.Money}";
    }
}
