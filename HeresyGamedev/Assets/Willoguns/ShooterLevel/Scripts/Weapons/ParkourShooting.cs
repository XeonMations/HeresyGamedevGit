using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourShooting : MonoBehaviour
{
    [SerializeField] private WeaponStats stats;
    [SerializeField] private ParkourPlayerMovement movementController;
    private float delay;
    private float defaultDelay;
    private float currentAmmo;
    private float currentReserve;
    private bool currentlyReloading;

    private void Start()
    {
        delay = 60f / stats.RPM;
        defaultDelay = delay;
    }

    private void Update()
    {
        delay -= Time.deltaTime;
        delay = Mathf.Clamp(delay, 0, defaultDelay);

        if (currentlyReloading)
        {
            movementController.canSprint = false;
            return;
        }

        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if(stats.fullAuto)
        {
            if(delay == 0 && Input.GetKey(KeyCode.Mouse0))
            {
                Shoot();
                delay = defaultDelay;
                return;
            }
        }
        else if(!stats.fullAuto)
        {
            if(delay == 0 && Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shoot();
                delay = defaultDelay;
                return;
            }
        }

        movementController.canSprint = true;
    }

    private void Shoot()
    {
        if(movementController.isSprinting)
        {
            movementController.isSprinting = false;
            movementController.canSprint = false;
        }

        Debug.Log("shoot");
    }

    private IEnumerator Reload()
    {
        movementController.isSprinting = false;
        movementController.canSprint = false;
        return null;
    }
}
