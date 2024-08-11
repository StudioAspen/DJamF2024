using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Upgrade : ScriptableObject
{
    [field: SerializeField] public UpgradeType UpgradeType {  get; private set; }
    [field: TextArea(3, 20)]
    [field: SerializeField] public string Description { get; private set; } = string.Empty;
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int Count { get; private set; }
}

public enum UpgradeType
{
    MaxHealth,
    MoneyGainMultiplier,
    FireRate,
    Damage,
    AutoFire,
    CheaperHealth,
    CheaperAmmo,
    BetterHeal,
    MoreAmmoReplenish
}
