using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesGiver : MonoBehaviour
{
    private PlayerManager player;

    [SerializeField] private float maxHealthAddAmount = 10f;
    [SerializeField] private float moneyGainMultiplierAddAmount = 0.1f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    public void ApplyUpgrade(Upgrade upgrade)
    {
        switch (upgrade)
        {
            case Upgrade.None:
                Debug.LogError("Can't apply an upgrade of None");
                break;
            case Upgrade.MaxHealth:
                player.AddMaxHealth(maxHealthAddAmount);
                break;
            case Upgrade.MoneyGainMultiplier:
                player.SetMoneyGainMultiplier(player.MoneyGainMultiplier + moneyGainMultiplierAddAmount);
                break;
            case Upgrade.FireRate:
                break;
            case Upgrade.Damage:
                break;
            case Upgrade.AutoFire:
                break;
            default:
                break;
        }
    }
}

public enum Upgrade
{
    None,
    MaxHealth,
    MoneyGainMultiplier,
    FireRate,
    Damage,
    AutoFire
}
