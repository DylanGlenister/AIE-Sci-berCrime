using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool m_bPenetrating;

    public int m_iDamage;

    public float m_iBulletSpeed = 100;

    public Rigidbody m_rbRigidBody;

    private void Awake ()
    {
        m_rbRigidBody = GetComponent<Rigidbody>();
        m_bPenetrating = false;
    }

    private void Update ()
    {
        // Updates the bullets position over time
        transform.position += m_iBulletSpeed * transform.forward * Time.deltaTime;
    }
}