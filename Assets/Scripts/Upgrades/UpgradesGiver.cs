using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UpgradesGiver : MonoBehaviour
{
    private PlayerManager player;

    [field: SerializeField] public Upgrade[] AllUpgrades { get; private set; }
    private Dictionary<UpgradeType, int> upgradesCounts = new Dictionary<UpgradeType, int>();

    [SerializeField] private float maxHealthAddAmount = 10f;
    [SerializeField] private float moneyGainMultiplierAddAmount = 0.1f;
    [SerializeField] private float fireRateMultiplierAddAmount = 0.1f;
    [SerializeField] private int damageAddAmount = 1;
    [SerializeField] private int ammoCostSubtractAmount = 5;
    [SerializeField] private int healCostSubtractAmount = 5;
    [SerializeField] private float healAddAmount = 10f;
    [SerializeField] private int ammoReplenishAddAmount = 2;

    [HideInInspector] public UnityEvent OnUpgradeApplied = new UnityEvent();

    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    private void Start()
    {
        PopulateSessionUpgradesCount();
    }

    public void ApplyUpgrade(UpgradeType upgradeType)
    {
        Upgrade upgrade = GetUpgradeByUpgradeType(upgradeType);
        upgradesCounts[upgradeType]--;

        switch (upgradeType)
        {
            case UpgradeType.MaxHealth:
                player.MaxHealth += maxHealthAddAmount;
                break;
            case UpgradeType.MoneyGainMultiplier:
                player.MoneyGainMultiplier = player.MoneyGainMultiplier + moneyGainMultiplierAddAmount;
                break;
            case UpgradeType.FireRate:
                break;
            case UpgradeType.Damage:
                break;
            case UpgradeType.AutoFire:
                break;
            case UpgradeType.CheaperHealth:
                player.HealCost -= healCostSubtractAmount;
                break;
            case UpgradeType.CheaperAmmo:
                player.AmmoReplenishCost -= ammoCostSubtractAmount;
                break;
            case UpgradeType.BetterHeal:
                player.HealAmount += healAddAmount;
                break;
            case UpgradeType.MoreAmmoReplenish:
                player.AmmoReplenishAmount += ammoReplenishAddAmount;
                break;
            default:
                break;
        }

        OnUpgradeApplied?.Invoke();
    }

    private void PopulateSessionUpgradesCount()
    {
        foreach(Upgrade upgrade in AllUpgrades)
        {
            upgradesCounts[upgrade.UpgradeType] = upgrade.Count;

            if (upgrade.UpgradeType == UpgradeType.CheaperHealth) upgradesCounts[upgrade.UpgradeType] = (int)Mathf.Ceil(player.HealCost / (float)healAddAmount);
            if (upgrade.UpgradeType == UpgradeType.CheaperAmmo) upgradesCounts[upgrade.UpgradeType] = (int)Mathf.Ceil(player.AmmoReplenishCost / (float)ammoCostSubtractAmount);
        }
    }

    public List<Upgrade> GetPossibleUpgrades()
    {
        List<Upgrade> possibleUpgrades = new List<Upgrade>();

        foreach (Upgrade upgrade in AllUpgrades)
        {
            if (upgradesCounts[upgrade.UpgradeType] > 0) possibleUpgrades.Add(upgrade);
        }

        return possibleUpgrades;
    }

    public Upgrade GetUpgradeByUpgradeType(UpgradeType type)
    {
        foreach (Upgrade upgrade in AllUpgrades)
        {
            if(upgrade.UpgradeType == type) return upgrade;
        }

        return null;
    }
}
