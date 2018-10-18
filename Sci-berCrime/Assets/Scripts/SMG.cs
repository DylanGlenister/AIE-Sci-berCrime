using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : GunBase
{
    // Gun control variables
    public int m_iDamage = 25;
    public int m_iAmmo = 100;
    public float m_fFireDelay = 0.05f;
    public float m_fFireTimer = 0.0f;

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
        if (m_fFireTimer == 0 && m_iAmmo > 0)
        {
            //Debug.Log(parent.name + " SMG Fire!");
            // Creates bullet and launches it forward
            GameObject newBullet = Instantiate(m_goBullet, m_goBulletSpawn.transform.position, parent.transform.rotation) as GameObject;
            newBullet.GetComponent<Bullet>().m_iDamage = m_iDamage;
            // Destroys bullet after 3 seconds
            Destroy(newBullet, 3);

            m_iAmmo -= 1;
            m_fFireTimer = m_fFireDelay;
        }
    }
}
