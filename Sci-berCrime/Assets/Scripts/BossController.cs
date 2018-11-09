using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{

    // LEAVE THIS TO LIAM NOBODY FUCKING TOUCH IT
    //----------Other----------
    [Header("Other")]
    // Boss Prefabs
    public GameObject m_goScuttlerBossPrefab;
    public GameObject m_goTurretBossPrefab;
    public GameObject m_goDroneBossPrefab;
    GameObject objScuttler;
    GameObject objDrone;
    GameObject objTurret;


    // Access to other classes
    public RoundController m_rcRoundController;
    public EnemyController m_ecEnemyController;
    public ShopController m_scShopController;
    
  

    // Variables for the bosses
    public bool m_bHasSpawned;
    public float m_fPlayerSafeBubbleSize;

    // Spawn position
    public GameObject m_goBossSpawnLocation;
    

    //----------Scuttler----------
    [Header("Scuttler")]
    public int m_sHealth;
    public int m_sDamage;
    public bool m_bScuttlerIsAlive;

    //----------Turret----------
    [Header("Turret")]
    public int m_tHealth;
    public int m_tDamage;
    public bool m_bTurretIsAlive;

    //----------Drone----------
    [Header("Drone")]
    public int m_dHealth;
    public int m_dDamage;
    public bool m_bDroneIsAlive;

    private void Awake()
    {
        m_sHealth =  100 * 50;
        m_sDamage =  20* 10;

        // Change these values to the health values of the other enemy types
        m_dHealth =  100 * 25;
        m_dDamage =  20 * 5;
        m_tHealth = 100 * 100;
        m_tDamage = 20 * 20;


        m_fPlayerSafeBubbleSize = 1.3f;

}

    private void Update()
    {
        if (m_sHealth == 0)
        {
            m_bScuttlerIsAlive = false;
            objScuttler.SetActive(false);
        }
        if (m_dHealth == 0)
        {
            m_bDroneIsAlive = false;
            objDrone.SetActive(false);
        }
        if (m_tHealth ==0)
        {
            m_bTurretIsAlive = false;
            objTurret.SetActive(false);
        }

        if (m_rcRoundController.m_iCurrentRound == 5 || m_rcRoundController.m_iCurrentRound == 10 || m_rcRoundController.m_iCurrentRound == 15 || m_rcRoundController.m_iCurrentRound == 20 && !m_bHasSpawned)
        {
            m_bHasSpawned = true;
        }
        else if (!m_bDroneIsAlive || !m_bScuttlerIsAlive || !m_bTurretIsAlive)
        {
            m_bHasSpawned = false;
        }

        if (m_bHasSpawned)
        {

            if (m_rcRoundController.m_iCurrentRound == 5)
            {
                objScuttler = Instantiate(m_goScuttlerBossPrefab);
                objScuttler.SetActive(true);
                objScuttler.transform.position = m_goBossSpawnLocation.transform.position;
            }
            if (m_rcRoundController.m_iCurrentRound == 10)
            {
                objDrone = Instantiate(m_goDroneBossPrefab);
                objDrone.SetActive(true);
                objDrone.transform.position = m_goBossSpawnLocation.transform.position;
            }
            if (m_rcRoundController.m_iCurrentRound == 15)
            {
                objTurret = Instantiate(m_goTurretBossPrefab);
                objTurret.SetActive(true);
                objTurret.transform.position = m_goBossSpawnLocation.transform.position;
            }
            if (m_rcRoundController.m_iCurrentRound == 20)
            {
                objScuttler = Instantiate(m_goScuttlerBossPrefab);
                objDrone = Instantiate(m_goDroneBossPrefab);
                objTurret = Instantiate(m_goTurretBossPrefab);

                objScuttler.SetActive(true);
                objDrone.SetActive(true);
                objTurret.SetActive(true);
            }
        }



    }


    
}
