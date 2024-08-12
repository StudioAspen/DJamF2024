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

    [field: Header("Upgrade Stats")]
    [field: SerializeField] public float ChanceToGetRareUpgrade { get; private set; } = 0.01f;

    [Header("Upgrade Add/Multipliers")]
    [SerializeField] private float maxHealthAddAmount = 10f;
    [SerializeField] private float moneyGainMultiplierAddAmount = 0.1f;
    [SerializeField] private float fireRateMultiplyAmount = 0.9f;
    [SerializeField] private int damageAddAmount = 1;
    [SerializeField] private int ammoCostSubtractAmount = 5;
    [SerializeField] private int healCostSubtractAmount = 5;
    [SerializeField] private float healAddAmount = 10f;
    [SerializeField] private int ammoReplenishAddAmount = 2;
    [SerializeField] private float reloadDurationMultiplier = 0.9f;
    [SerializeField] private int magAddAmount = 1;

    [HideInInspector] public UnityEvent OnUpgradeApplied = new UnityEvent();

    AudioSource clink;

    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
    }

    private void Start()
    {
        PopulateSessionUpgradesCount();
        clink = GetComponent<AudioSource>();
    }

    public void ApplyUpgrade(UpgradeType upgradeType)
    {
        // Audio
        clink.Stop();
        clink.Play();
        
        Upgrade upgrade = GetUpgradeByUpgradeType(upgradeType);
        upgradesCounts[upgradeType]--;

        switch (upgradeType)
        {
            case UpgradeType.MaxHealth:
                player.MaxHealth += maxHealthAddAmount;
                break;
            case UpgradeType.MoneyGainMultiplier:
                player.MoneyGainMultiplier += moneyGainMultiplierAddAmount;
                break;
            case UpgradeType.FireRate:
                player.Gun.FireRate *= fireRateMultiplyAmount;
                break;
            case UpgradeType.Damage:
                player.Gun.Damage += damageAddAmount;
                break;
            case UpgradeType.AutoFire:
                player.Gun.IsAutomatic = true;
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
            case UpgradeType.FasterReload:
                player.Gun.ReloadDuration *= reloadDurationMultiplier;
                break;
            case UpgradeType.BiggerMag:
                player.Gun.MagSize += magAddAmount;
                break;
            case UpgradeType.FullHeal:
                player.CurrentHealth = player.MaxHealth;
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

            if (upgrade.UpgradeType == UpgradeType.CheaperHealth) upgradesCounts[upgrade.UpgradeType] = (int)Mathf.Ceil(player.HealCost / (float)healCostSubtractAmount - 1);
            if (upgrade.UpgradeType == UpgradeType.CheaperAmmo) upgradesCounts[upgrade.UpgradeType] = (int)Mathf.Ceil(player.AmmoReplenishCost / (float)ammoCostSubtractAmount - 1);
        }
    }

    public List<Upgrade> GetPossibleNormalUpgrades()
    {
        List<Upgrade> possibleUpgrades = new List<Upgrade>();

        foreach (Upgrade upgrade in AllUpgrades)
        {
            if (upgradesCounts[upgrade.UpgradeType] > 0 && !upgrade.IsRare) possibleUpgrades.Add(upgrade);
        }

        return possibleUpgrades;
    }

    public List<Upgrade> GetPossibleRareUpgrades()
    {
        List<Upgrade> possibleUpgrades = new List<Upgrade>();

        foreach (Upgrade upgrade in AllUpgrades)
        {
            if (upgradesCounts[upgrade.UpgradeType] > 0 && upgrade.IsRare) possibleUpgrades.Add(upgrade);
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
