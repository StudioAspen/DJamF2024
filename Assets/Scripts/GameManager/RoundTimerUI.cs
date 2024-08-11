using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundTimerUI : MonoBehaviour
{
    [SerializeField] GameManager owner;
    [SerializeField] TMP_Text timer;

    private void Update() {
        UpdateUI();
    }

    private void UpdateUI() {
        TimeSpan timeSpan = TimeSpan.FromSeconds(owner.roundTimer);
        timer.text = timeSpan.ToString(@"mm\:ss");
    }
}
