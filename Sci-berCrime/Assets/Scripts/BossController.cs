using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{

    // LEAVE THIS TO LIAM NOBODY FUCKING TOUCH IT

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
    
    private NavMeshAgent m_nmaNavMeshAgent;

    // References to other players
    public GameObject m_goPlayerOne;
    public GameObject m_goPlayerTwo;
    public GameObject m_goCurrentTarget;

    // Variables for the bosses
    public bool m_bHasSpawned;
    

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
        m_sHealth = m_ecEnemyController.m_iHealth * 50;
        m_sDamage = m_ecEnemyController.m_iDamage * 10;

        // Change these values to the health values of the other enemy types
        m_dHealth = m_ecEnemyController.m_iHealth * 25;
        m_dDamage = m_ecEnemyController.m_iDamage * 5;
        m_tHealth = m_ecEnemyController.m_iHealth * 100;
        m_tDamage = m_ecEnemyController.m_iDamage * 20;


        m_nmaNavMeshAgent = GetComponent<NavMeshAgent>();
        m_goPlayerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        m_goPlayerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
        m_goCurrentTarget = m_goPlayerOne;

        
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

        if ((!m_bDroneIsAlive || !m_bScuttlerIsAlive || !m_bTurretIsAlive) && (m_goPlayerOne || m_goPlayerTwo))
        {
            // If one player is dead only target the other player
            if (!m_goPlayerOne)
            {
                if (m_goCurrentTarget != m_goPlayerTwo)
                    m_goCurrentTarget = m_goPlayerTwo;

                m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
            }
            else if (!m_goPlayerTwo)
            {
                if (m_goCurrentTarget != m_goPlayerOne)
                    m_goCurrentTarget = m_goPlayerOne;

                m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
            }
            else
            {
                // Calculates the distance from the enemy to each player
                Vector3 playerOneDistance = transform.position - m_goPlayerOne.transform.position;
                Vector3 playerTwoDistance = transform.position - m_goPlayerTwo.transform.position;

                if (playerOneDistance.magnitude < 0)
                    playerOneDistance *= -1;

                if (playerTwoDistance.magnitude < 0)
                    playerTwoDistance *= -1;

                // Seperates distance checking by target player for slight increase in efficiency
                if (m_goCurrentTarget == m_goPlayerOne)
                {
                    // Either player one is dead or player two is closer as long as player two is alive
                    if (!m_goPlayerOne.gameObject.GetComponent<PlayerController>().m_bIsAlive
                        || (playerTwoDistance.magnitude < playerOneDistance.magnitude
                        && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive))
                    {
                        //Debug.Log("Target is now player 2");
                        m_goCurrentTarget = m_goPlayerTwo;
                    }
                    // Only paths to target if they aren't already touching the target
                    else
                    if (playerOneDistance.magnitude > m_ecEnemyController.m_fPlayerSafeBubbleSize
                        && m_goPlayerOne.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                    {
                        m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                    }
                }
                else
                {
                    // Either player two is dead or player one is closer as long as player one is alive
                    if (!m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive
                        || (playerOneDistance.magnitude < playerTwoDistance.magnitude
                        && m_goPlayerOne.gameObject.GetComponent<PlayerController>().m_bIsAlive))
                    {
                        //Debug.Log("Target is now player 1");
                        m_goCurrentTarget = m_goPlayerOne;
                    }
                    // Only paths to target if they aren't already touching the target
                    else
                    if (playerTwoDistance.magnitude > m_ecEnemyController.m_fPlayerSafeBubbleSize
                        && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                    {
                        m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                    }
                }
            }
        }
        else
        {
            m_nmaNavMeshAgent.enabled = false;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            m_ecEnemyController.TakeDamage(other.gameObject.GetComponent<Bullet>().m_iDamage);

        }


    }

    public void TakeDamage(int pDamage)
    {
        if (m_bScuttlerIsAlive)
        {
            m_sHealth-= pDamage;
        }
        else if (m_bDroneIsAlive)
        {
            m_dHealth -= pDamage;
        }
        else if (m_bTurretIsAlive)
        {
            m_tHealth -= pDamage;
        }
         
        m_scShopController.GetComponent<ShopController>().DepositToWallet(pDamage);

        if (m_sHealth < 0)
        {
            m_sHealth = 0;
        }
        else if (m_dHealth < 0)
        {
            m_dHealth = 0;
        }
        else if (m_tHealth < 0)
        {
            m_tHealth = 0;
        }
    }
}
