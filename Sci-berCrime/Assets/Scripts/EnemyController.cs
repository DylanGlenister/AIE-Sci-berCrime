using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public bool m_bIsAlive { get; set; }

    public int m_iHealth = 100;
    public int m_iDamage;
    public float m_fPlayerSafeBubbleSize = 1.3f;

    // References to the players
    public GameObject m_goPlayerOne;
    public GameObject m_goPlayerTwo;
    public GameObject m_goCurrentTarget;

    private NavMeshAgent m_nmaNavMeshAgent;

    void Awake()
    {
        m_nmaNavMeshAgent = GetComponent<NavMeshAgent>();
        m_goPlayerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        m_goPlayerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
        m_goCurrentTarget = m_goPlayerOne;

        m_bIsAlive = true;
    }

    void Update()
    {
        if (m_iHealth == 0)
        {
            m_bIsAlive = false;
            Destroy(this.gameObject);
        }

        if (m_bIsAlive && (m_goPlayerOne.gameObject.GetComponent<PlayerController>().m_bIsAlive || m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive))
        {
            // Calculates the distance from the enemy to each player
            Vector3 playerOneDistance = this.transform.position - m_goPlayerOne.transform.position;
            Vector3 playerTwoDistance = this.transform.position - m_goPlayerTwo.transform.position;

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
                    || !m_goPlayerOne.gameObject.GetComponent<PlayerController>().m_bIsAlive)
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
                    || !m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                {
                    m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
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
            TakeDamage(other.gameObject.GetComponent<Bullet>().m_iDamage);

            if (!other.gameObject.GetComponent<Bullet>().m_bRailgun)
                Destroy(other.gameObject);
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