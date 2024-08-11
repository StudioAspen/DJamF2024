using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CrossHairUI : MonoBehaviour
{
    [SerializeField] Gun owner;
    [SerializeField] Image reloadGague;
    RectTransform rect;

    private void Start() {
        rect = GetComponent<RectTransform>();
    }

    private void Update() {
        UpdateUI();
        UpdatePosition();
    }
    private void UpdatePosition() {
        Vector3 screenPos = owner.crosshairPos;
        rect.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
    }
    private void UpdateUI() {
        reloadGague.fillAmount = owner.reloadTimer / owner.reloadDuration;
    }
}
