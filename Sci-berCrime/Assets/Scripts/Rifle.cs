using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : GunBase
{
    // Gun control variables
    public int ammo = 100;
    public int magazine = 10;
    public float fireDelay = 0.1f;
    public float fireTimer = 0.0f;
    public int damage;

    public GameObject bullet;
    public GameObject bulletSpawn;

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
            Debug.Log(parent.name + " Rifle Fire!");
            GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, parent.transform.rotation) as GameObject;
            Destroy(newBullet, 2);
            fireTimer = fireDelay;
        }
    }
}
