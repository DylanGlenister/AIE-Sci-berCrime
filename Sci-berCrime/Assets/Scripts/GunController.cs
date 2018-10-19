using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // Gun control variables
    public int m_iDamage = 35;
    public int m_iAmmo = 500;

    public float m_fFireDelay = 0.01f;
    public float m_fFireTimer = 0.0f;

    // The background updating for the gun
    public void GunUpdate()
    {
        if (m_fFireTimer > 0)
        {
            m_fFireTimer -= Time.deltaTime;

            if (m_fFireTimer < 0)
                m_fFireTimer = 0;
        }
    }

    // The update for the currently active gun
    public void ActiveGunUpdate(GameObject pParent)
    {
        if (m_fFireTimer == 0 && m_iAmmo > 0)
        {
            //Debug.Log(parent.name + " Handgun Fire!");
            // Creates bullet and launches it forward
            GameObject newBullet = Instantiate(pParent.GetComponent<PlayerController>().m_goBulletPrefab,
                pParent.GetComponent<PlayerController>().m_goBulletSpawn.transform.position,
                pParent.transform.rotation) as GameObject;
            newBullet.GetComponent<Bullet>().m_iDamage = this.m_iDamage;
            // Destroys bullet after 3 seconds
            Destroy(newBullet, 3);

            m_iAmmo -= 1;
            m_fFireTimer = m_fFireDelay;
        }
    }
}
