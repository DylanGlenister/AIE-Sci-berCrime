using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool m_bIsAlive { get; set; }

    public bool m_bPlayerOne;
    public int m_iHealth = 100;
    public float m_fMovementSpeed = 50.0f;

    public GameObject m_goAimTarget;
    public GameObject m_goBulletSpawn;
    public GameObject m_goBulletPrefab;
    
    // Currently selected gun
    public GunController m_gcGun;

    // Reference to the rigidbody
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        m_bIsAlive = true;
    }

    void FixedUpdate()
    {
        if (m_bIsAlive)
        {
            // All controls for player one
            if (m_bPlayerOne)
            {
                //----------Movement----------
                // Vertical movement
                rb.AddForce(new Vector3(0, 0, Input.GetAxis("P1 LS Vertical") * m_fMovementSpeed), ForceMode.Force);
                // Horizontal movement
                rb.AddForce(new Vector3(Input.GetAxis("P1 LS Horizontal") * m_fMovementSpeed, 0, 0), ForceMode.Force);

                //----------Rotation----------
                // Moves target object
                m_goAimTarget.transform.position = this.transform.position + new Vector3(Input.GetAxis("P1 RS Horizontal"), 0, Input.GetAxis("P1 RS Vertical"));

                // Calculates direction needed for facing
                Vector3 targetDir = m_goAimTarget.transform.position - this.transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 9999, 0.0f);
                this.transform.rotation = Quaternion.LookRotation(newDir);
            }
            // All controls for player two
            else
            {
                //----------Movement----------
                // Vertical movement
                rb.AddForce(new Vector3(0, 0, Input.GetAxis("P2 LS Vertical") * m_fMovementSpeed), ForceMode.Force);
                // Horizontal movement
                rb.AddForce(new Vector3(Input.GetAxis("P2 LS Horizontal") * m_fMovementSpeed, 0, 0), ForceMode.Force);

                //----------Rotation----------
                // Moves target object
                m_goAimTarget.transform.position = this.transform.position + new Vector3(Input.GetAxis("P2 RS Horizontal"), 0, Input.GetAxis("P2 RS Vertical"));

                // Calculates direction needed for facing
                Vector3 targetDir = m_goAimTarget.transform.position - this.transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 9999, 0.0f);
                this.transform.rotation = Quaternion.LookRotation(newDir);
            }
        }
    }

    void Update()
    {
        if (m_iHealth == 0)
        {
            m_bIsAlive = false;
            this.gameObject.SetActive(false);
        }

        //Debug.Log(isAlive);

        if (m_bPlayerOne)
        {
            // Calls a specific update function for the currently active gun
            if (m_gcGun && m_bIsAlive && Input.GetButton("P1 Fire"))
            {
                m_gcGun.ActiveGunUpdate(this.gameObject);
            }
        }
        else
        {
            // Calls a specific update function for the currently active gun
            if (m_gcGun && m_bIsAlive && Input.GetButton("P2 Fire"))
            {
                m_gcGun.ActiveGunUpdate(this.gameObject);
            }
        }

        // Call an update function for each of the equiped guns
        if (m_gcGun)
            m_gcGun.GunUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Player dies if hit by enemy
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyController>().m_iDamage);
        }
    }

    // Applies damage to the object
    public void TakeDamage(int pDamage)
    {
        m_iHealth -= pDamage;
        if (m_iHealth < 0)
            m_iHealth = 0;
    }
}