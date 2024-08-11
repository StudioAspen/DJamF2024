using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoUI : MonoBehaviour
{
    [SerializeField] Gun owner;
    [SerializeField] TMP_Text totalAmmo;
    [SerializeField] TMP_Text currentAmmo;

    private void Update() {
        UpdateUI();
    }

    public void UpdateUI() {
        totalAmmo.text = "Total Ammo: " + owner.TotalAmmo.ToString();
        currentAmmo.text = owner.CurrentAmmo + " / " + owner.MagSize;
    }
}
