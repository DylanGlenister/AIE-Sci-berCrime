using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Stats for the bullet;
    public float m_iBulletSpeed = 100f;
    public float m_fBulletLife = 1f;
    public float m_fBulletCountdown;

    public int m_iDamage;
    public int m_iPiercing;
    public int m_iExplosive;

    // Explosion variables
    public int m_iL1Damage;
    public int m_iL2Damage;
    public float m_fL1Radius;
    public float m_fL2Radius;


    public Rigidbody m_rbRigidBody;

    private void Awake ()
    {
        m_rbRigidBody = GetComponent<Rigidbody>();
        m_fBulletCountdown = m_fBulletLife;
    }

    private void Update ()
    {
        if (m_fBulletCountdown > 0)
        {
            m_fBulletCountdown -= Time.deltaTime;
            if (m_fBulletCountdown < 0)
                m_fBulletCountdown = 0;

            // Updates the bullets position over time
            transform.position += m_iBulletSpeed * transform.forward * Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void BulletFired (GunController pPlayer)
    {
        // Re-initialises variables when the bullet is fired
        m_fBulletCountdown = m_fBulletLife;
        m_iDamage = pPlayer.m_iDamage;
        m_iPiercing = pPlayer.m_iPiercing;
        m_iExplosive = pPlayer.m_iExplosive;
    }
}