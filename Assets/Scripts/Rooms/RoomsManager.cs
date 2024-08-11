using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomsManager : MonoBehaviour
{
    [Header("Room Transforms")]
    [SerializeField] private Transform healthRoom;
    [SerializeField] private Transform ammoRoom;
    [SerializeField] private Transform upgradeRoom;
    AudioSource swish;


    public int CurrentRoom { get; private set; } // 0 - health, 1 - ammo, 2 - upgrade
    [SerializeField] private bool[] roomActive = { true, true, true };

    private void Start() {
        swish = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HandleCameraMovement();
    }

    public void SwitchRooms(int roomNumber) {
        if (!roomActive[roomNumber])
        {
            return;
        }

        CurrentRoom = roomNumber;
    }

    public void SwitchToNextRoom() {
        swish.Stop();
        swish.Play();

        int futureRoom = (CurrentRoom + 1) % 3;

        if (!roomActive[futureRoom])
        {
            SwitchRooms((futureRoom + 1) % 3);
            return;
        }

        CurrentRoom = futureRoom;
    }

    public void SwitchToPreviousRoom() {
        swish.Stop();
        swish.Play();

        int futureRoom = CurrentRoom - 1 < 0 ? 2 : CurrentRoom - 1;

        if (!roomActive[futureRoom])
        {
            SwitchRooms(futureRoom - 1 < 0 ? 2 : futureRoom - 1);
            return;
        }

        CurrentRoom = futureRoom;
    }

    public void DisableRoom(int roomNumber)
    {
        roomActive[roomNumber] = false;
    }

    private void HandleCameraMovement()
    {
        Vector3 targetCameraPosition = Vector3.zero;

        if (CurrentRoom == 0) targetCameraPosition = healthRoom.transform.position;
        if (CurrentRoom == 1) targetCameraPosition = ammoRoom.transform.position;
        if (CurrentRoom == 2) targetCameraPosition = upgradeRoom.transform.position;

        targetCameraPosition.z = -10f;

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetCameraPosition, 10f * Time.deltaTime);
    }
}
