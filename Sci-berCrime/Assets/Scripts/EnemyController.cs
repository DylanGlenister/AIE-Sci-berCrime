using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public ShopController m_scShopController;
    public EnemySpawnController m_escEnemySpawnController;

    public bool IsAlive { get; set; }

    public int m_iHealth = 100;
    public int m_iDamage;

    public float m_fPlayerSafeBubbleSize = 1.3f;

    // References to the players
    public GameObject m_goPlayerOne;
    public GameObject m_goPlayerTwo;
    public GameObject m_goCurrentTarget;

    private NavMeshAgent m_nmaNavMeshAgent;

    private void Awake ()
    {
        m_nmaNavMeshAgent = GetComponent<NavMeshAgent>();
        m_goPlayerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        m_goPlayerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
        m_scShopController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ShopController>();
        m_escEnemySpawnController = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawnController>();
        m_goCurrentTarget = m_goPlayerOne;

        IsAlive = true;
    }

    private void Update ()
    {
        if (m_iHealth == 0)
        {
            m_scShopController.GetComponent<ShopController>().DepositToWallet(10);
            IsAlive = false;
            m_escEnemySpawnController.m_iCurrentScuttlerCount -= 1;
            m_escEnemySpawnController.m_iCurrentScuttlersKilledThisRound += 1;
            gameObject.SetActive(false);
        }

        if (IsAlive && (m_goPlayerOne || m_goPlayerTwo))
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
                    if (playerOneDistance.magnitude > m_fPlayerSafeBubbleSize
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
                    if (playerTwoDistance.magnitude > m_fPlayerSafeBubbleSize
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

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().m_iDamage);

            if (!other.gameObject.GetComponent<Bullet>().m_bPenetrating)
                other.gameObject.GetComponent<Bullet>().m_fBulletCountdown = 0;
        }
    }

    // Applies damage to the object
    public void TakeDamage (int pDamage)
    {
        m_iHealth -= pDamage;
        if (m_iHealth < 0)
            m_iHealth = 0;
    }
}