using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private Volume volume;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;
    private FilmGrain filmGrain;

    public Gun Gun { get; private set; }

    public float CurrentHealth;
    public float MaxHealth = 100f;
    public int Money;
    public float MoneyGainMultiplier = 1;
    public float HealAmount = 50f;
    public int AmmoReplenishAmount = 30;
    public int HealCost = 50;
    public int AmmoReplenishCost = 50;
    public int UpgradeCost = 50;
    public float UpgradeCostMultiplier = 1.05f;

    public string endSceneName;

    private void Awake()
    {
        Gun = FindObjectOfType<Gun>();
        volume = FindObjectOfType<Volume>();

        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out filmGrain);
    }


    [SerializeField] AudioSource whispering;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        HandleHealth();
    }

    private void HandleHealth()
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        float targetCAIntensity = 2f * (1 - CurrentHealth / MaxHealth);
        chromaticAberration.intensity.Override(Mathf.Lerp(chromaticAberration.intensity.value, targetCAIntensity, Time.unscaledDeltaTime));

        float targetFGIntensity = 1 - (CurrentHealth / MaxHealth);
        filmGrain.intensity.value = Mathf.Lerp(filmGrain.intensity.value, targetFGIntensity, Time.unscaledDeltaTime);

        float targetVIntensity = 0.5f - 0.5f * (CurrentHealth / MaxHealth);
        vignette.intensity.value =  Mathf.Lerp(vignette.intensity.value, targetVIntensity, Time.unscaledDeltaTime);

        Vector2 newVignetteCenter = new Vector2(Gun.CrosshairPos.x / Camera.main.pixelRect.width, Gun.CrosshairPos.y / Camera.main.pixelRect.height);
        vignette.center.value = Vector2.Lerp(vignette.center.value, newVignetteCenter, 10f * Time.unscaledDeltaTime);
        if(CurrentHealth/MaxHealth <= 0.5f) {
            float percent = CurrentHealth / MaxHealth;
            whispering.volume = Mathf.Pow(1-percent,3);
        }
        else {
            whispering.volume = 0;
        }

        if(CurrentHealth <= 0) {
            SceneManager.LoadScene(endSceneName);
        }
    }
}
