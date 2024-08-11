using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shroomManager : MonoBehaviour
{
    public int s_MaxHealth = 300;
    public int s_currentHealth;
    public int moneyDropped = 3;
    public GameObject Shroom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            damageShroom();
            Debug.Log("Hit");
        }

        if (s_currentHealth <= 0)
        {
            //Delete Shroom
        }

    }

     private void damageShroom()
        {
            s_currentHealth -= 100;
        }
}
