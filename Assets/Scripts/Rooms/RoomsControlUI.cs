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
    private PlayerUI playerUI;
    [SerializeField] AudioSource moneyDing;
    [SerializeField] AudioSource deny;

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
        upgradesUI = FindObjectOfType<UpgradesUI>();
        playerUI = FindObjectOfType<PlayerUI>();

        rightButton.onClick.AddListener(NextRoom);
        leftButton.onClick.AddListener(PrevRoom);

        buyHealthButton.onClick.AddListener(() => {
            if (player.Money < player.HealCost || player.CurrentHealth == player.MaxHealth) {
                deny.Stop();
                deny.Play();
                return;
            }
            moneyDing.Stop();
            moneyDing.Play();

            playerUI.PlayMoneySubtractAnimation(player.HealCost);
            player.Money -= player.HealCost;

            player.CurrentHealth += player.HealAmount;
        });

        buyAmmoButton.onClick.AddListener(() => {
            if (player.Money < player.AmmoReplenishCost) {
                deny.Stop();
                deny.Play();
                return;
            }
            moneyDing.Stop();
            moneyDing.Play();

            playerUI.PlayMoneySubtractAnimation(player.AmmoReplenishCost);
            player.Money -= player.AmmoReplenishCost;

            player.Gun.TotalAmmo += player.AmmoReplenishAmount;
        });

        buyUpgradeButton.onClick.AddListener(() => {
            if (player.Money < player.UpgradeCost) {
                deny.Stop();
                deny.Play();
                return;
            }
            moneyDing.Stop();
            moneyDing.Play();

            playerUI.PlayMoneySubtractAnimation(player.UpgradeCost);
            player.Money -= player.UpgradeCost;
            player.UpgradeCost = (int)Mathf.Floor(player.UpgradeCost * player.UpgradeCostMultiplier);

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
        buyHealthButtonText.text = $"Buy {player.HealAmount} Health: {player.HealCost} Caps";
        buyAmmoButtonText.text = $"Buy {player.AmmoReplenishAmount} Ammo: {player.AmmoReplenishCost} Caps";
        buyUpgradeButtonText.text = $"Buy Upgrade: {player.UpgradeCost} Caps";
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
