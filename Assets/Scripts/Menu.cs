using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private BlackScreenFade blackScreenFade;
    [SerializeField] private Button playButton;
    private bool started;

    private void Awake()
    {
        blackScreenFade = FindObjectOfType<BlackScreenFade>();

        playButton.onClick.AddListener(() => {
            if (started) return;

            StartCoroutine(Play());
            
        });
    }

    void Start()
    {
        blackScreenFade.FadeIn();
    }
    
    IEnumerator Play()
    {
        started = true;

        blackScreenFade.FadeInOut();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Main");
    }
}
