using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapCounter : MonoBehaviour
{
    public static CapCounter instance;

    public TMP_Text capUI;
    public int currentCaps;
    public int startCaps = 0;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentCaps = startCaps;
        capUI.text = "Mush Caps: " + currentCaps.ToString();
    }

    public void IncreaseCaps(int cap)
    {
        currentCaps += cap;
        capUI.text = "Mush Caps: " + currentCaps.ToString();
    }
}

