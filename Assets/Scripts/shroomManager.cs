using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomManager : MonoBehaviour
{
    public int s_MaxHealth = 300;
    public int s_currentHealth;
    public int capsOnDeath = 3;

    // Start is called before the first frame update
    void Start()
    {
        s_currentHealth = s_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            damageShroom(100);
            Debug.Log("Hit");
        }
    }

     public void damageShroom(int dmgAmount)
        {
            s_currentHealth -= dmgAmount;

            if(s_currentHealth <= 0)
            {
                Destroy(gameObject);
                CapCounter.instance.IncreaseCaps(capsOnDeath);
            }
        }
}
