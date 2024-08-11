using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject startScreen;
    [SerializeField] TMP_Text startTitle;
    [SerializeField] GameObject endScreen;
    [SerializeField] TMP_Text endTitle;
    [SerializeField] BlackScreenFade fade;
    [SerializeField] int totalRounds;
    int currentRound;

    [SerializeField] float roundDuration;
    public float roundTimer;
    [SerializeField] string sceneAtEndGame;

    [SerializeField] ShroomSpawner[] shroomSpawners = new ShroomSpawner[3];

    bool roundActive;

    private void Start() {
        currentRound = 0;
        fade.FadeIn(InitalizeNewRound);
    }

    private void Update() {
        if (roundActive) {
            roundTimer -= Time.deltaTime;
            if(roundTimer <= 0) {
                RoundEnd();
            }
        }
    }

    private void InitalizeNewRound() {

        Debug.Log("Initalize Round");
        currentRound++;
        roundTimer = roundDuration;
        startScreen.SetActive(true);
        endScreen.SetActive(false);

        FindObjectOfType<RoomsManager>().EnableAllRooms();

        startTitle.text = "Day " + currentRound;

        // Changing round when final rounds
        if (currentRound > totalRounds) {
            SceneManager.LoadScene(sceneAtEndGame);
        }
    }

    public void RoundStart() {
        Debug.Log("Start Game");
        roundActive = true;
        startScreen.SetActive(false);

        foreach (var shroom in shroomSpawners)
        {
            shroom.CanSpawn = true;
        }
    }

    public void RoundEnd() {
        Debug.Log("End");
        roundActive = false;
        endScreen.SetActive(true);
        endTitle.text = "End of Day " + currentRound;

        foreach (var shroom in shroomSpawners)
        {
            shroom.CanSpawn = false;
            shroom.KillAllShrooms();

            shroom.SpawnIntervalMultiplier *= 0.75f;
        }
    }

    public void EndRoundButton() {
        Debug.Log("end round button");

        fade.FadeInOut(InitalizeNewRound);
        startScreen.SetActive(false);
    }
}
