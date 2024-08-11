using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomManager : MonoBehaviour
{
    public int s_MaxHealth = 300;
    public int s_currentHealth;
    public int capsOnDeath = 3;
    [SerializeField] GameObject squishSound;

    private Coroutine flashCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        s_currentHealth = s_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void damageShroom(int dmgAmount)
    {
        if (flashCoroutine != null) StopFlash();
        flashCoroutine = StartCoroutine(Flash());

        s_currentHealth -= dmgAmount;

        if (s_currentHealth <= 0)
        {
            Instantiate(squishSound);
            Destroy(gameObject);
            FindObjectOfType<PlayerManager>().Money += capsOnDeath;
        }
    }

    private IEnumerator Flash()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.05f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    private void StopFlash()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
