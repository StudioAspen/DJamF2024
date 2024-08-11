using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesUI : MonoBehaviour
{
    private UpgradesGiver upgradesGiver;

    [SerializeField] private GameObject upgradePanelObject;

    [Header("Upgrade Cards")]
    [SerializeField] private Button[] upgradeButton = new Button[3];
    [SerializeField] private TMP_Text[] upgradeDescriptionText = new TMP_Text[3];
    [SerializeField] private Image[] upgradeImage = new Image[3];

    private void Awake()
    {
        upgradesGiver = FindAnyObjectByType<UpgradesGiver>();

        upgradesGiver.OnUpgradeApplied.AddListener(Hide);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F)) Generate3RandomUpgrades();
    }

    public void Generate3RandomUpgrades()
    {
        for (int i = 0; i < 3; i++)
        {
            float randomPercent = Random.Range(0, 1f);
            List<Upgrade> potentialUpgrades = randomPercent < upgradesGiver.ChanceToGetRareUpgrade ? upgradesGiver.GetPossibleRareUpgrades() : upgradesGiver.GetPossibleNormalUpgrades();

            if(potentialUpgrades.Count == 0) potentialUpgrades = upgradesGiver.GetPossibleNormalUpgrades();

            int randomIndex = Random.Range(0, potentialUpgrades.Count);
            Upgrade randomUpgrade = potentialUpgrades[randomIndex];

            upgradeButton[i].onClick.RemoveAllListeners();
            upgradeButton[i].GetComponent<Image>().color = randomUpgrade.IsRare ? Color.yellow : Color.white;

            upgradeButton[i].onClick.AddListener(() => upgradesGiver.ApplyUpgrade(randomUpgrade.UpgradeType));
            upgradeDescriptionText[i].text = randomUpgrade.Description;
            upgradeImage[i].sprite = randomUpgrade.Sprite;

            potentialUpgrades.Remove(randomUpgrade);
        }
    }

    public void Show()
    {
        upgradePanelObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Hide()
    {
        upgradePanelObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
