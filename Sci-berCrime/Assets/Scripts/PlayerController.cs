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

    // Reference to the guns class
    public GunBase m_goGunPrimary;
    public GunBase m_goGunSecondary;
    public GunBase m_goGunTertiary;
    public GunBase m_goGunQuadary;

    // Currently selected gun
    public GunBase m_goCurrentGun;

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
            Destroy(this.gameObject);
        }

        //Debug.Log(isAlive);

        if (m_bPlayerOne)
        {
            if (Input.GetButtonDown("P1 Weapon Swap"))
            {
                if (m_goCurrentGun == m_goGunPrimary)
                    m_goCurrentGun = m_goGunSecondary;
                else if (m_goCurrentGun == m_goGunSecondary)
                    m_goCurrentGun = m_goGunTertiary;
                else if (m_goCurrentGun == m_goGunTertiary)
                    m_goCurrentGun = m_goGunQuadary;
                else
                    m_goCurrentGun = m_goGunPrimary;
            }

            // Calls a specific update function for the currently active gun
            if (m_goCurrentGun && m_bIsAlive && Input.GetButton("P1 Fire"))
            {
                m_goCurrentGun.ActiveGunUpdate(this.gameObject);
            }
        }
        else
        {
            if (Input.GetButtonDown("P2 Weapon Swap"))
            {
                if (m_goCurrentGun = m_goGunPrimary)
                    m_goCurrentGun = m_goGunSecondary;
                else if (m_goCurrentGun = m_goGunSecondary)
                    m_goCurrentGun = m_goGunTertiary;
                else
                    m_goCurrentGun = m_goGunPrimary;
            }

            // Calls a specific update function for the currently active gun
            if (m_goCurrentGun && m_bIsAlive && Input.GetButton("P2 Fire"))
            {
                m_goCurrentGun.ActiveGunUpdate(this.gameObject);
            }
        }

        // Call an update function for each of the equiped guns
        if (m_goGunPrimary)
            m_goGunPrimary.GunUpdate();
        if (m_goGunSecondary)
            m_goGunSecondary.GunUpdate();
        if (m_goGunTertiary)
            m_goGunTertiary.GunUpdate();
        if (m_goGunQuadary)
            m_goGunQuadary.GunUpdate();
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