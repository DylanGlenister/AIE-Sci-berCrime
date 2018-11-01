using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int m_iExplosionRadius;

    private void Start()
    {
        Vector3 explosionPos = transform.position;
        
        //Make an empty array of colliders that are within a sphere the size of 'explosionRadius'
        Collider[] colliders = Physics.OverlapSphere(explosionPos, m_iExplosionRadius);

        //Deals damage in an area to all objects with ObjectHealth script
        foreach (Collider hit in colliders)
        {
            if (hit.transform.GetComponent<EnemyController>())
            {
                hit.transform.GetComponent<EnemyController>().TakeDamage(20);
            }
        }
        Destroy(this);
    }
}
