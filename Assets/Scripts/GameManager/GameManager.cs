using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int totalRounds;
    int currentRound;

    [SerializeField] float roundDuration;
    public float roundTimer;

    private void Start() {
        currentRound = 0;
    }

    private void InitalizeNewRound() {
        currentRound++;
        roundTimer = roundDuration;
    }

    public void RoundStart() {
        Debug.Log("Start Game");
    }

    public void RoundEnd() {
        Debug.Log("End");
    }
}
