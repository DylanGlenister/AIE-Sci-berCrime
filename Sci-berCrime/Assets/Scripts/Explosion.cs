using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private bool m_bExploded = false;
    public float m_fExplosionDelay = 0.25f;
    public float m_fExplosionCountdown;

    public int m_iExplosionDamage;
    public float m_fExplosionRadius;

    private void Awake()
    {
        m_fExplosionCountdown = m_fExplosionDelay;
    }

    private void Update()
    {
        if (m_fExplosionCountdown != 0)
        {
            m_fExplosionCountdown -= Time.deltaTime;

            if (m_fExplosionCountdown < 0)
                m_fExplosionCountdown = 0;
        }
        else if (!m_bExploded)
        {
            Vector3 explosionPos = transform.position;

            //Make an empty array of colliders that are within a sphere the size of 'explosionRadius'
            Collider[] colliders = Physics.OverlapSphere(explosionPos, m_fExplosionRadius);

            //Deals damage in an area to all objects with ObjectHealth script
            foreach (Collider hit in colliders)
            {
                if (hit.transform.GetComponent<EnemyController>())
                {
                    hit.transform.GetComponent<EnemyController>().TakeDamage(80);
                }
            }
            Destroy(this.gameObject, 0.5f);
        }
    }
}
