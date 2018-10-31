using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //extra's for the bullets
    public int m_iPenetrating;
    public int m_iExplosive;
    public int m_iDamage;

    public float m_fBulletLife = 1f;
    public float m_fBulletCountdown;
    public float m_iBulletSpeed = 100f;

    public Rigidbody m_rbRigidBody;

    private void Awake ()
    {
        m_rbRigidBody = GetComponent<Rigidbody>();
        m_iPenetrating = 0;
        m_iExplosive = 0;
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
}