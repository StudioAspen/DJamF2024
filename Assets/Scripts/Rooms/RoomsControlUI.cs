using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomsControlUI : MonoBehaviour
{
    private RoomsManager roomsManager;

    [Header("Switch Rooms")]
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;

    [Header("Room Shops")]
    [SerializeField] private Button buyHealthButton;
    [SerializeField] private Button buyAmmoButton;
    [SerializeField] private Button buyUpgradeButton;

    private void Awake()
    {
        roomsManager = FindObjectOfType<RoomsManager>();

        rightButton.onClick.AddListener(NextRoom);
        leftButton.onClick.AddListener(PrevRoom);
    }

    private void Update()
    {
        HandleBuyButtonsVisibility();
    }

    private void HandleBuyButtonsVisibility()
    {
        buyHealthButton.gameObject.SetActive(roomsManager.CurrentRoom == 0);
        buyAmmoButton.gameObject.SetActive(roomsManager.CurrentRoom == 1);
        buyUpgradeButton.gameObject.SetActive(roomsManager.CurrentRoom == 2);
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
