using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : GunBase
{
    // Gun control variables
    public int m_iAmmo = 100;
    public int m_iMagazine = 10;
    public float m_fFireDelay = 2.0f;
    public float m_fFireTimer = 0.0f;
    public int m_iDamage = 100;

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
            //Debug.Log(parent.name + " Railgun Fire!");
            GameObject newBullet = Instantiate(m_goBullet, m_goBulletSpawn.transform.position, parent.transform.rotation) as GameObject;
            newBullet.GetComponent<Bullet>().m_iDamage = m_iDamage;
            newBullet.GetComponent<Bullet>().m_bRailgun = true;
            Destroy(newBullet, 3);
            m_fFireTimer = m_fFireDelay;
        }
    }
}
