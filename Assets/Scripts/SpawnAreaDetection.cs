using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaDetection : MonoBehaviour
{
    [SerializeField] private GameObject[] shrooms;
    [SerializeField] private Collider2D roomspawnarea;

     // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }
    private void Spawn()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spawn");
            shroomSpawner.instance.SpawnShrooms(roomspawnarea, shrooms);
        }
    }
}
