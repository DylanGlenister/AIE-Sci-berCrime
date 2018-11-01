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

    // upgrades for the bullets
    public int m_iPenetrating;
    public int m_iExplosive;
    public int m_baseExplosive;
    public int m_iSpread;


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
            obj.SetActive(false);
            obj.GetComponent<Bullet>().m_iDamage = m_iDamage;
            m_lgoBulletList.Add(obj);
        }
        m_iPenetrating = 0;
        m_iExplosive = 0;
        m_iSpread = 0;
    }
    
  

    // The background updating for the gun
    public void GunUpdate()
    {
        if (m_fFireTimer > 0)
        {
            m_fFireTimer -= Time.deltaTime;
        }
    }

    // The update for the currently active gun
    public void ActiveGunUpdate(GameObject pParent)
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
                    m_lgoBulletList[i].GetComponent<Bullet>().m_fBulletCountdown = m_lgoBulletList[i].GetComponent<Bullet>().m_fBulletLife;
                    m_lgoBulletList[i].SetActive(true);
                    m_iAmmo -= 1;
                    break;
                }


            }
            // If spread upgrade is set to 1
            if (m_iSpread > 0)
            {
                for (int i = 0; i < m_lgoBulletList.Count; i++)
                {
                    if (!m_lgoBulletList[i].activeInHierarchy)
                    {
                        // +15 degrees
                        m_lgoBulletList[i].transform.position = m_goBulletSpawn.transform.position;
                        m_lgoBulletList[i].transform.rotation = m_goBulletSpawn.transform.rotation;
                        m_lgoBulletList[i].transform.rotation *= Quaternion.Euler(0, 5, 0);
                        m_lgoBulletList[i].GetComponent<Bullet>().m_fBulletCountdown = m_lgoBulletList[i].GetComponent<Bullet>().m_fBulletLife;
                        m_lgoBulletList[i].SetActive(true);
                        m_iAmmo -= 1;
                        break;
                    }
                }
                // This mirrors the bullets going at -15 degrees from the middle point,
                // when in actuality it's adding the total degrees minus the 15 from the middle
                // so that we can avoid the inverse function.
                for (int i = 0; i < m_lgoBulletList.Count; i++)
                {
                    if (!m_lgoBulletList[i].activeInHierarchy)
                    {
                        // -15 degrees
                        m_lgoBulletList[i].transform.position = m_goBulletSpawn.transform.position;
                        m_lgoBulletList[i].transform.rotation = m_goBulletSpawn.transform.rotation;
                        m_lgoBulletList[i].transform.rotation *= Quaternion.Euler(0, -5, 0);
                        m_lgoBulletList[i].GetComponent<Bullet>().m_fBulletCountdown = m_lgoBulletList[i].GetComponent<Bullet>().m_fBulletLife;
                        m_lgoBulletList[i].SetActive(true);
                        m_iAmmo -= 1;
                        break;
                    }
                }

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
                            m_lgoBulletList[i].GetComponent<Bullet>().m_fBulletCountdown = m_lgoBulletList[i].GetComponent<Bullet>().m_fBulletLife;
                            m_lgoBulletList[i].SetActive(true);
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
                            m_lgoBulletList[i].GetComponent<Bullet>().m_fBulletCountdown = m_lgoBulletList[i].GetComponent<Bullet>().m_fBulletLife;
                            m_lgoBulletList[i].SetActive(true);
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
            if (pParent.gameObject.GetComponent<PlayerController>().m_bPlayerOne)
                m_uicUIController.SetPlayerOneAmmo(m_iAmmo);
            else
                m_uicUIController.SetPlayerTwoAmmo(m_iAmmo);
        }
    }
}
