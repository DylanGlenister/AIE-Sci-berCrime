using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public UIController m_uicUIController;

    // Gun control variables
    public int m_iDamage = 35;
    public int m_iAmmo = 1000;
    public int m_iMaxAmmo = 1000;
    public int m_iMaxBulletsAtOnce = 300;

    // Upgrades for the gun
    public int m_iPiercing = 0;
    public int m_iExplosive = 0;
    public int m_iSpread = 0;

    // Explosive upgrade variables
    public int m_iL1Damage = 80;
    public int m_iL2Damage = 200;
    public float m_fL1Radius = 5f;
    public float m_fL2Radius = 10f;

    public float m_fFireDelay = 0.01f;
    public float m_fFireTimer = 0.0f;

    private List<GameObject> m_lgoBulletList;

    public GameObject m_goBulletPrefab;
    public GameObject m_goBulletSpawn;

    private void Awake()
    {
        // Populates the bullets list
        m_lgoBulletList = new List<GameObject>();
        for (int i = 0; i < m_iMaxBulletsAtOnce; i++)
        {
            GameObject obj = Instantiate(m_goBulletPrefab);

            // Initalises variables
            obj.GetComponent<Bullet>().m_iDamage = m_iDamage;
            obj.GetComponent<Bullet>().m_iPiercing = m_iPiercing;
            obj.GetComponent<Bullet>().m_iExplosive = m_iExplosive;
            obj.GetComponent<Bullet>().m_iL1Damage = m_iL1Damage;
            obj.GetComponent<Bullet>().m_iL2Damage = m_iL2Damage;
            obj.GetComponent<Bullet>().m_fL1Radius = m_fL1Radius;
            obj.GetComponent<Bullet>().m_fL2Radius = m_fL2Radius;

            obj.SetActive(false);
            m_lgoBulletList.Add(obj);
        }
    }

    // The background updating for the gun
    public void GunUpdate()
    {
        // Counts down the delay between shots
        // Value is allowed to go below 0 to compensate for framerate
        if (m_fFireTimer > 0)
            m_fFireTimer -= Time.deltaTime;
    }

    // The update for the currently active gun
    public void ActiveGunUpdate(PlayerController pParent)
    {
        if (m_fFireTimer <= 0 && m_iAmmo > 0)
        {
            // Picks the bullet that isn't enable and spawns it in
            for (int i = 0; i < m_lgoBulletList.Count; i++)
            {
                if (!m_lgoBulletList[i].activeInHierarchy)
                {
                    m_lgoBulletList[i].transform.position = m_goBulletSpawn.transform.position;
                    m_lgoBulletList[i].transform.rotation = m_goBulletSpawn.transform.rotation;
                    m_lgoBulletList[i].GetComponent<Bullet>().BulletFired(this);
                    m_lgoBulletList[i].SetActive(true);
                    m_iAmmo -= 1;
                    break;
                }
            }

            // If spread upgrade is level 1
            if (m_iSpread > 0)
            {
                // Iterates through the bullet list twice, firing one facing slightly to the right and one slightly to the left
                for (int i = 0; i < m_lgoBulletList.Count; i++)
                {
                    if (!m_lgoBulletList[i].activeInHierarchy)
                    {
                        // +15 degrees
                        m_lgoBulletList[i].transform.position = m_goBulletSpawn.transform.position;
                        m_lgoBulletList[i].transform.rotation = m_goBulletSpawn.transform.rotation;
                        m_lgoBulletList[i].transform.rotation *= Quaternion.Euler(0, 5, 0);
                        m_lgoBulletList[i].SetActive(true);
                        m_lgoBulletList[i].GetComponent<Bullet>().BulletFired(this);
                        m_iAmmo -= 1;
                        break;
                    }
                }
                
                for (int i = 0; i < m_lgoBulletList.Count; i++)
                {
                    if (!m_lgoBulletList[i].activeInHierarchy)
                    {
                        // -15 degrees
                        m_lgoBulletList[i].transform.position = m_goBulletSpawn.transform.position;
                        m_lgoBulletList[i].transform.rotation = m_goBulletSpawn.transform.rotation;
                        m_lgoBulletList[i].transform.rotation *= Quaternion.Euler(0, -5, 0);
                        m_lgoBulletList[i].SetActive(true);
                        m_lgoBulletList[i].GetComponent<Bullet>().BulletFired(this);
                        m_iAmmo -= 1;
                        break;
                    }
                }

                // If the spread upgrade is at level 2
                if (m_iSpread == 2)
                {
                    for (int i = 0; i < m_lgoBulletList.Count; i++)
                    {
                        if (!m_lgoBulletList[i].activeInHierarchy)
                        {
                            // +30 degrees
                            m_lgoBulletList[i].transform.position = m_goBulletSpawn.transform.position;
                            m_lgoBulletList[i].transform.rotation = m_goBulletSpawn.transform.rotation;
                            m_lgoBulletList[i].transform.rotation *= Quaternion.Euler(0, 10, 0);
                            m_lgoBulletList[i].SetActive(true);
                            m_lgoBulletList[i].GetComponent<Bullet>().BulletFired(this);
                            m_iAmmo -= 1;
                            break;
                        }
                    }

                    for (int i = 0; i < m_lgoBulletList.Count; i++)
                    {
                        if (!m_lgoBulletList[i].activeInHierarchy)
                        {
                            // -30 degrees
                            m_lgoBulletList[i].transform.position = m_goBulletSpawn.transform.position;
                            m_lgoBulletList[i].transform.rotation = m_goBulletSpawn.transform.rotation;
                            m_lgoBulletList[i].transform.rotation *= Quaternion.Euler(0, -10, 0);
                            m_lgoBulletList[i].SetActive(true);
                            m_lgoBulletList[i].GetComponent<Bullet>().BulletFired(this);
                            m_iAmmo -= 1;
                            break;
                        }
                    }
                }
            }

            if (m_iAmmo < 0)
                m_iAmmo = 0;

            m_fFireTimer += m_fFireDelay;

            // Updates the associated UI element for ammo based on which player the gun script is attached to
            if (pParent.m_bPlayerOne)
                m_uicUIController.SetPlayerOneUIAmmo(m_iAmmo);
            else
                m_uicUIController.SetPlayerTwoUIAmmo(m_iAmmo);
        }
    }
}
