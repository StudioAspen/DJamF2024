using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomManager : MonoBehaviour
{
    private ShroomSpawner shroomSpawner;

    public int s_MaxHealth = 300;
    public int s_currentHealth;
    public int capsOnDeath = 3;
    [SerializeField] GameObject squishSound;

    private Coroutine flashCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        s_currentHealth = s_MaxHealth;

        GetComponentInChildren<SpriteRenderer>().flipX = Random.Range(0, 2) == 0;
        transform.localScale = s_MaxHealth/7.5f * Random.Range(0.8f, 1.5f) * Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Init(ShroomSpawner sp)
    {
        shroomSpawner = sp;
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
            shroomSpawner.RemoveShroom(gameObject);
            shroomSpawner.ShroomCount--;
            FindObjectOfType<PlayerManager>().Money += (int)Mathf.Ceil(capsOnDeath * FindObjectOfType<PlayerManager>().MoneyGainMultiplier);
        }
    }

    private IEnumerator Flash()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    private void StopFlash()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
