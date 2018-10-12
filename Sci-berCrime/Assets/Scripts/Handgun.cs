using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : GunBase
{
    // Gun control variables
    public int m_iAmmo = 100;
    public int m_iMagazine = 10;
    public float m_fFireDelay = 0.25f;
    public float m_fFireTimer = 0.0f;
    public int m_iDamage;

    public GameObject m_goBullet;
    public GameObject m_goBulletSpawn;

    // The background updating for the gun
    public override void GunUpdate()
    {
        if (m_fFireTimer > 0)
        {
            m_fFireTimer -= Time.deltaTime;

            if (m_fFireTimer < 0)
                m_fFireTimer = 0;
        }
    }

    // The update for the currently active gun
    public override void ActiveGunUpdate(GameObject parent)
    {
        if (m_fFireTimer == 0)
        {
            Debug.Log(parent.name + " Handgun Fire!");
            GameObject newBullet = Instantiate(m_goBullet, m_goBulletSpawn.transform.position, parent.transform.rotation) as GameObject;
            Destroy(newBullet, 2);
            m_fFireTimer = m_fFireDelay;
        }
    }
}
