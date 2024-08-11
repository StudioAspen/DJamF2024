using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomsControlUI : MonoBehaviour
{
    private RoomsManager roomsManager;
    private PlayerManager player;
    private UpgradesUI upgradesUI;

    [Header("Switch Rooms")]
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;

    [Header("Room Shops")]
    [SerializeField] private Button buyHealthButton;
    [SerializeField] private Button buyAmmoButton;
    [SerializeField] private Button buyUpgradeButton;
    [SerializeField] private TMP_Text buyHealthButtonText;
    [SerializeField] private TMP_Text buyAmmoButtonText;
    [SerializeField] private TMP_Text buyUpgradeButtonText;

    private void Awake()
    {
        roomsManager = FindObjectOfType<RoomsManager>();
        player = FindObjectOfType<PlayerManager>();
        UpgradesUI upgradesUI = FindObjectOfType<UpgradesUI>();

        rightButton.onClick.AddListener(NextRoom);
        leftButton.onClick.AddListener(PrevRoom);

        buyHealthButton.onClick.AddListener(() => {
            if (player.Money < player.HealCost) return;

            player.Money -= player.HealCost;

            player.CurrentHealth += player.HealAmount;
        });

        buyAmmoButton.onClick.AddListener(() => {
            if (player.Money < player.AmmoReplenishCost) return;

            player.Money -= player.AmmoReplenishCost;
        });

        buyUpgradeButton.onClick.AddListener(() => {
            if (player.Money < player.UpgradeCost) return;

            player.Money -= player.UpgradeCost;

            upgradesUI.Show();
            upgradesUI.Generate3RandomUpgrades();
        });
    }

    private void Update()
    {
        HandleBuyButtonsVisibility();
        HandleShopButtonsCostUI();
    }

    private void HandleBuyButtonsVisibility()
    {
        buyHealthButton.gameObject.SetActive(roomsManager.CurrentRoom == 0);
        buyAmmoButton.gameObject.SetActive(roomsManager.CurrentRoom == 1);
        buyUpgradeButton.gameObject.SetActive(roomsManager.CurrentRoom == 2);
    }

    private void HandleShopButtonsCostUI()
    {
        buyHealthButtonText.text = $"Buy {player.HealAmount} Health: {player.HealCost} Money";
        buyAmmoButtonText.text = $"Buy {player.AmmoReplenishAmount} Ammo: {player.AmmoReplenishCost} Money";
        buyUpgradeButtonText.text = $"Buy Upgrade: {player.UpgradeCost} Money";
    }

    public void NextRoom()
    {
        roomsManager.SwitchToNextRoom();
    }

    public void PrevRoom()
    {
        roomsManager.SwitchToPreviousRoom();
    }
}
