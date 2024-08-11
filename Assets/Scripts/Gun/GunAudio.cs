using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAudio : MonoBehaviour
{
    [SerializeField] AudioSource gunShot;
    [SerializeField] AudioSource reload;

    public void PlayShot() {
        gunShot.Stop();
        gunShot.Play();
        
    }

    public void PlayReload() {
        if(!reload.isPlaying) {
            reload.Play();
        }
    }
}
