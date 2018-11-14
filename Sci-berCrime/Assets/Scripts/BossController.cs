using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour {

    public ShopController m_scShopController;
    public BossSpawnController m_bscBossSpawnController;
    public RoundController m_rcRoundController;
    public GameObject objBoss;
 
    public GameObject m_goPlayerOne;
    public GameObject m_goPlayerTwo;
    public GameObject m_goCurrentTarget;





    private NavMeshAgent m_nmaNavMeshAgent;

    public float m_fPlayerSafeBubbleSize;
    //----------Boss Stats----------
    [Header("Boss")]
    public int m_bHealth;
    public int m_sDamage;
    public int m_etEnemyType;
    public bool m_bIsAlive;
    public float m_fDroneRange = 5f;
    public float m_fTurrentRange = 15f;



    private void Awake()
    {
        m_nmaNavMeshAgent = GetComponent<NavMeshAgent>();
        m_goPlayerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        m_goPlayerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
        m_goCurrentTarget = m_goPlayerOne;
        m_scShopController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ShopController>();

        m_fPlayerSafeBubbleSize = 1.3f;

    }

    private void Update()
    {
        if (m_bHealth == 0)
        {
            m_bIsAlive = false;
            objBoss.SetActive(false);
            if (m_rcRoundController.m_bRoundOver)
            {
                m_bscBossSpawnController.m_bHasSpawned = false;
            }
        }


        if (m_bIsAlive && (m_goPlayerOne || m_goPlayerTwo))
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

                //scuttler
                if (m_etEnemyType == 0)
                {
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
                        if (playerOneDistance.magnitude > m_fPlayerSafeBubbleSize
                            && m_goPlayerOne.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                        {
                            //goes to the target's position
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
                        if (playerTwoDistance.magnitude > m_fPlayerSafeBubbleSize
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                        {
                            //Goes to target's position
                            m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                        }
                    }

                }
                //drone
                else
                if (m_etEnemyType == 2)
                {
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
                        if (playerOneDistance.magnitude > m_fDroneRange
                            && m_goPlayerOne.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                        {
                            // Moves the enemy in range;
                            m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);

                            if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) < m_fDroneRange)
                            {
                                m_nmaNavMeshAgent.isStopped = true;
                            }
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
                        if (playerTwoDistance.magnitude > m_fDroneRange
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                        {
                            // moves the enemy in range
                            m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);

                            if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) < m_fDroneRange)
                            {
                                m_nmaNavMeshAgent.isStopped = true;
                            }

                        }
                    }
                }

                // Turret
                if (m_etEnemyType == 1)
                {
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
                        if (playerOneDistance.magnitude > m_fDroneRange
                            && m_goPlayerOne.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                        {
                            // Moves the enemy in range;
                            m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);

                            if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) < m_fDroneRange)
                            {
                                m_nmaNavMeshAgent.isStopped = true;
                            }
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
                        if (playerTwoDistance.magnitude > m_fDroneRange
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                        {
                            // moves the enemy in range
                            m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);

                            if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) < m_fDroneRange)
                            {
                                m_nmaNavMeshAgent.isStopped = true;
                            }

                        }
                    }
                }

                else
                {
                    m_nmaNavMeshAgent.enabled = false;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().m_iDamage);

            if (other.gameObject.GetComponent<Bullet>().m_iPiercing == 0)
                other.gameObject.GetComponent<Bullet>().m_fBulletCountdown = 0;
        }
    }

    // Applies damage to the object
    public void TakeDamage(int pDamage)
    {
        if (m_bIsAlive)
        {
            m_bHealth -= pDamage;
        }
      
        m_scShopController.GetComponent<ShopController>().DepositToWallet(100);
        if (m_bHealth < 0)
            m_bHealth = 0;
       
    }
}
