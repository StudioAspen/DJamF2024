using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnDeath : MonoBehaviour
{
    [SerializeField] AudioSource source;

    private void Update() {
        if(!source.isPlaying) {
            Destroy(gameObject);
        }
    }
}
