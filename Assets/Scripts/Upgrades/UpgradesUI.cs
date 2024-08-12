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
        List<Upgrade> normalUpgrades = upgradesGiver.GetPossibleNormalUpgrades();
        List<Upgrade> rareUpgrades = upgradesGiver.GetPossibleRareUpgrades();

        for (int i = 0; i < 3; i++)
        {
            List<Upgrade> potentialUpgrades = normalUpgrades;

            float randomPercent = Random.Range(0, 1f);

            if (randomPercent < upgradesGiver.ChanceToGetRareUpgrade) potentialUpgrades = rareUpgrades;
            if(potentialUpgrades.Count == 0) potentialUpgrades = normalUpgrades;

            int randomIndex = Random.Range(0, potentialUpgrades.Count);
            Upgrade randomUpgrade = potentialUpgrades[randomIndex];

            upgradeButton[i].onClick.RemoveAllListeners();
            upgradeButton[i].GetComponent<Image>().color = randomUpgrade.IsRare ? Color.gray : Color.white;

            upgradeButton[i].onClick.AddListener(() => upgradesGiver.ApplyUpgrade(randomUpgrade.UpgradeType));
            upgradeDescriptionText[i].text = randomUpgrade.Description;

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
