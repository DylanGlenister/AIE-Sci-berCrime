using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : GunBase
{
    // Gun control variables
    public int ammo = 100;
    public int magazine = 10;
    public float fireDelay = 0.05f;
    public float fireTimer = 0.0f;
    public int damage;

    // The background updating for the gun
    public override void GunUpdate()
    {
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;

            if (fireTimer < 0)
                fireTimer = 0;
        }
    }

    // The update for the currently active gun
    public override void ActiveGunUpdate(GameObject parent)
    {
        if (fireTimer == 0)
        {
            Debug.Log(parent.name + " SMG Fire!");
            fireTimer = fireDelay;
        }
    }
}
