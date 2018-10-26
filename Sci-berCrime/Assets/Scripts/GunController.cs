using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public UIController m_uicUIController;

    // Gun control variables
    public int m_iDamage = 35;
    public int m_iAmmo = 1000;
    public int m_iMaxBulletsAtOnce = 57;

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
    }

    // The background updating for the gun
    public void GunUpdate ()
    {
        if (m_fFireTimer > 0)
        {
            m_fFireTimer -= Time.deltaTime;

            if (m_fFireTimer < 0)
                m_fFireTimer = 0;
        }
    }

    // The update for the currently active gun
    public void ActiveGunUpdate (GameObject pParent)
    {
        if (m_fFireTimer == 0 && m_iAmmo > 0)
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
                    break;
                }
            }

            m_iAmmo -= 1;
            m_fFireTimer = m_fFireDelay;

            // Updates the associated UI element for ammo based on which player the gun script is attached to
            if (pParent.gameObject.GetComponent<PlayerController>().m_bPlayerOne)
                m_uicUIController.SetPlayerOneAmmo(m_iAmmo);
            else
                m_uicUIController.SetPlayerTwoAmmo(m_iAmmo);
        }
    }
}
