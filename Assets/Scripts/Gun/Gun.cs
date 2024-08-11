using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] GunAudio gunAudio;
    [SerializeField] float damage;
    [SerializeField] float fireRate;
    float fireTimer;
    [SerializeField] public float reloadDuration;
    public float reloadTimer;
    bool reloading = false;

    public int currentAmmo;
    public int magSize;
    [SerializeField] public int totalAmmo;

    [SerializeField] bool isAutomatic;
    public Vector2 crosshairPos;

    private void Start() {
        currentAmmo = magSize;
        gunAudio = GetComponentInChildren<GunAudio>();
    }
    private void Update() {
        // Inputs
        if(isAutomatic && Input.GetMouseButton(0)) {
            Shoot();
        }
        else if(Input.GetMouseButtonDown(0)) {
            Shoot();
        }

        // Updating Timer
        fireTimer -= Time.deltaTime;

        // Reload update
        ReloadUpdate();

        // Updates
        UpdateCrosshair();
    }

    private void Shoot() {

        if(fireTimer <= 0 && currentAmmo > 0) {
            gunAudio.PlayShot();
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(crosshairPos.x, crosshairPos.y, 0));

            Collider2D foundCollider = Physics2D.OverlapPoint(worldPoint);
            Debug.DrawLine(worldPoint, worldPoint + Vector2.up, Color.white, 0.5f);

            // Ammo + reloading
            currentAmmo--;
            if(currentAmmo <= 0) {
                Reload();
            }
            
            Debug.Log("shot");

            // Resetting timer
            fireTimer = fireRate;
        }
    }

    private void Reload() {
        if(!reloading) {
            gunAudio.PlayReload();
            reloading = true;
            reloadTimer = reloadDuration;
        }
    }

    private void ReloadUpdate() {
        if (reloading) {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0) {
                int loadDifference = magSize - currentAmmo;
                int loadAmount = Mathf.Min(totalAmmo, loadDifference);
                currentAmmo = loadAmount;
                totalAmmo -= loadAmount;

                reloading = false;
            }
        }
    }

    private void UpdateCrosshair() {
        crosshairPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }
}
