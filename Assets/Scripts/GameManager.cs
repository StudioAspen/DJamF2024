using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent startGame;
    public UnityEvent endGame;
    // Start is called before the first frame update
    void Awake()
    {
        if (startGame == null)
        {
            startGame = new UnityEvent();
        }

        if (endGame == null) 
        {
            endGame = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

}
