using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool m_bRailgun;
    public int m_iDamage;
    public float m_iSpeed = 1;

    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        m_bRailgun = false;
    }

    private void Update()
    {
        transform.position += m_iSpeed * transform.forward * Time.deltaTime;
        //rb.AddForce(new Vector3(0, 0, m_speed), ForceMode.Impulse);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        other.gameObject.GetComponent<EnemyController>().TakeDamage(m_iDamage);
    //    }

    //    if (!m_bRailgun)
    //        Destroy(this.gameObject);
    //}
}