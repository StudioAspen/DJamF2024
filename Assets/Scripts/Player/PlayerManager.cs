using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float CurrentHealth;
    public float MaxHealth = 100f;
    public int Money;
    public float MoneyGainMultiplier = 1;
    public float HealAmount = 50f;
    public int AmmoReplenishAmount = 30;
    public int HealCost = 50;
    public int AmmoReplenishCost = 50;
    public int UpgradeCost = 50;

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
    }
}
