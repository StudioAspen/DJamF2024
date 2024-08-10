using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [field: SerializeField] public float CurrentHealth { get; private set; }
    [field: SerializeField] public float MaxHealth { get; private set; } = 100f;
    [field: SerializeField] public int Money { get; private set; }
    [field: SerializeField] public float MoneyGainMultiplier { get; private set; } = 1;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void SetMaxHealth(float amount)
    {
        MaxHealth = amount;
    }

    public void AddMaxHealth(float amount)
    {
        MaxHealth += amount;
    }

    public void SetCurrentHealth(float amount)
    {
        CurrentHealth = amount;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
    }

    public void SetMoney(int amount)
    {
        Money = amount;
    }

    public void AddMoney(int amount)
    {
        Money += (int)Mathf.Floor(amount * MoneyGainMultiplier);
    }

    public void SetMoneyGainMultiplier(float multiplier)
    {
        MoneyGainMultiplier = multiplier;
    }
}
