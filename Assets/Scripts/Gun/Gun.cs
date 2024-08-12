using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
    public float Damage;
    public float FireRate;
    [SerializeField] GunAudio gunAudio;
    float fireTimer;
    public float ReloadDuration;
    [HideInInspector]public float ReloadTimer;
    bool reloading = false;

    public int CurrentAmmo;
    public int MagSize;
    public int TotalAmmo;

    public bool IsAutomatic;
    public Vector2 CrosshairPos;

    [SerializeField] LayerMask shroomLayer;

    private void Start() {
        CurrentAmmo = MagSize;
        gunAudio = GetComponentInChildren<GunAudio>();
    }
    private void Update() {
        // Inputs
        if(IsAutomatic && Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject()) {
            Shoot();
        }
        else if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
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
        if(CurrentAmmo <= 0 && TotalAmmo > 0)
        {
            if (CurrentAmmo <= 0)
            {
                Reload();
            }

            // Resetting timer
            fireTimer = FireRate;
        }

        if(fireTimer <= 0 && CurrentAmmo > 0) {
            gunAudio.PlayShot();
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(CrosshairPos.x, CrosshairPos.y, 0));

            Collider2D foundCollider = Physics2D.OverlapPoint(worldPoint, shroomLayer);

            if(foundCollider) {
                foundCollider.GetComponent<ShroomManager>().damageShroom(Mathf.FloorToInt(Damage));
            }

            FindObjectOfType<MuzzleFlash>(true).FlashMuzzle();

            // Ammo + reloading
            CurrentAmmo--;
            if(CurrentAmmo <= 0) {
                Reload();
            }

            // Resetting timer
            fireTimer = FireRate;
        }
    }

    private void Reload() {
        if(!reloading) {
            gunAudio.PlayReload();
            reloading = true;
            ReloadTimer = ReloadDuration;
        }
    }

    private void ReloadUpdate() {
        if (reloading) {
            ReloadTimer -= Time.deltaTime;
            if (ReloadTimer <= 0) {
                int loadDifference = MagSize - CurrentAmmo;
                int loadAmount = Mathf.Min(TotalAmmo, loadDifference);
                CurrentAmmo = loadAmount;
                TotalAmmo -= loadAmount;

                reloading = false;
            }
        }
    }

    private void UpdateCrosshair() {
        CrosshairPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }
}
