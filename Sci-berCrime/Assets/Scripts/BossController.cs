using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    public ShopController m_scShopController;
    public BossSpawnController m_bscBossSpawnController;
    public RoundController m_rcRoundController;
    public CameraScript m_csCameraScript;

    public enum M_btBossType
    {
        Scuttler,
        Turret,
        Drone
    }

    public int m_btEnemyType;

    public float m_fPlayerSafeBubbleSize;

    private NavMeshAgent m_nmaNavMeshAgent;

    //----------Boss Stats----------
    [Header("Boss")]
    public bool m_bIsAlive;

    public int m_bHealth;
    public int m_sDamage;
    public float m_bRange;
    public float m_bDroneTimer;
    public float m_bTurretTimer;
    public float m_bScuttlerTimer;
 
    public GameObject m_goPlayerOne;
    public GameObject m_goPlayerTwo;
    public GameObject m_goCurrentTarget;
    public GameObject m_goExplosion;

    private void Awake()
    {
        m_nmaNavMeshAgent = GetComponent<NavMeshAgent>();
        m_goPlayerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        m_goPlayerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
        m_scShopController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ShopController>();
        m_rcRoundController = GameObject.FindGameObjectWithTag("GameController").GetComponent<RoundController>();
        m_bscBossSpawnController = GameObject.FindGameObjectWithTag("GameController").GetComponent<BossSpawnController>();
        m_csCameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
        m_goCurrentTarget = m_goPlayerOne;
        m_bTurretTimer = m_bscBossSpawnController.m_bDefaultTurretTimer;
        m_bDroneTimer = m_bscBossSpawnController.m_bDefaultDroneTimer;
        m_bScuttlerTimer = m_bscBossSpawnController.m_bDefaultScuttlerTimer;

        m_fPlayerSafeBubbleSize = 1.3f;
    }

    private void Update()
    {
        if (m_bIsAlive && m_bHealth == 0)
        {
            m_bIsAlive = false;
            m_rcRoundController.m_bBossDead = true;
            Destroy(gameObject);
        }

        if (m_rcRoundController.m_bRoundOver && m_bIsAlive)
        {
            m_bscBossSpawnController.m_bHasSpawned = false;
        }

        if (m_bIsAlive && (m_goPlayerOne || m_goPlayerTwo))
        {
            if (m_btEnemyType == 0)
            {
                EnemyMove(M_btBossType.Scuttler);
            }
            else if (m_btEnemyType == 1)
            {
                EnemyMove(M_btBossType.Turret);
            }
            else if (m_btEnemyType == 2)
            {
                EnemyMove(M_btBossType.Drone);
            }
        }
    }

    private void EnemyMove(M_btBossType p_EtEnemyType)
    {
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
                //scuttler
                if (p_EtEnemyType == M_btBossType.Scuttler)
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
                        if (!m_goPlayerOne.gameObject.GetComponent<PlayerController>().IsAlive
                            || (playerTwoDistance.magnitude < playerOneDistance.magnitude
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().IsAlive))
                        {
                            //Debug.Log("Target is now player 2");
                            m_goCurrentTarget = m_goPlayerTwo;
                        }
                        // Only paths to target if they aren't already touching the target
                        else if (playerOneDistance.magnitude > m_fPlayerSafeBubbleSize
                            && m_goPlayerOne.gameObject.GetComponent<PlayerController>().IsAlive)
                        {
                            //goes to the target's position
                            m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                        }
                    }
                    else
                    {
                        // Either player two is dead or player one is closer as long as player one is alive
                        if (!m_goPlayerTwo.gameObject.GetComponent<PlayerController>().IsAlive
                            || (playerOneDistance.magnitude < playerTwoDistance.magnitude
                            && m_goPlayerOne.gameObject.GetComponent<PlayerController>().IsAlive))
                        {
                            //Debug.Log("Target is now player 1");
                            m_goCurrentTarget = m_goPlayerOne;
                        }
                        // Only paths to target if they aren't already touching the target
                        else if (playerTwoDistance.magnitude > m_fPlayerSafeBubbleSize
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().IsAlive)
                        {
                            //Goes to target's position
                            m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                        }
                    }
                    if (Vector3.Distance(m_goCurrentTarget.transform.position, m_nmaNavMeshAgent.transform.position) < 5.0f)
                    {
                        m_bScuttlerTimer -= Time.deltaTime;
                        if (m_bScuttlerTimer <= 0)
                        {
                            m_bScuttlerTimer = 0;
                        }
                        if (m_bScuttlerTimer == 0)
                        {
                            if (m_goCurrentTarget == m_goPlayerOne)
                            {

                                m_goPlayerOne.GetComponent<PlayerController>().TakeDamage(m_sDamage);
                            }
                            else
                            {

                                m_goPlayerTwo.GetComponent<PlayerController>().TakeDamage(m_sDamage);
                            }
                            m_bScuttlerTimer = m_bscBossSpawnController.m_bDefaultScuttlerTimer;
                        }
                    }
                }
                //drone
                if (p_EtEnemyType == M_btBossType.Drone)
                {
                    m_bRange = m_bscBossSpawnController.m_fbDroneRange;
                    // Calculates the distance from the enemy to each player
                    Vector3 playerOneDistance = transform.position - m_goPlayerOne.transform.position;
                    Vector3 playerTwoDistance = transform.position - m_goPlayerTwo.transform.position;
                    if (playerOneDistance.magnitude < 0)
                        playerOneDistance *= -1;

                    if (playerTwoDistance.magnitude < 0)
                        playerTwoDistance *= -1;
                    if (m_goCurrentTarget == m_goPlayerOne)
                    {
                        // Either player one is dead or player two is closer as long as player two is alive
                        if (!m_goPlayerOne.gameObject.GetComponent<PlayerController>().IsAlive
                            || (playerTwoDistance.magnitude < playerOneDistance.magnitude
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().IsAlive))
                        {
                            //Debug.Log("Target is now player 2");
                            m_goCurrentTarget = m_goPlayerTwo;
                        }
                        // Only paths to target if they aren't already touching the target
                        else
                        if (playerOneDistance.magnitude > m_bRange
                            && m_goPlayerOne.gameObject.GetComponent<PlayerController>().IsAlive)
                        {
                            if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) > m_bRange)
                            {
                                m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                                m_nmaNavMeshAgent.stoppingDistance = m_bRange;
                            }
                        }
                    }
                    else
                    {
                        // Either player two is dead or player one is closer as long as player one is alive
                        if (!m_goPlayerTwo.gameObject.GetComponent<PlayerController>().IsAlive
                            || (playerOneDistance.magnitude < playerTwoDistance.magnitude
                            && m_goPlayerOne.gameObject.GetComponent<PlayerController>().IsAlive))
                        {
                            //Debug.Log("Target is now player 1");
                            m_goCurrentTarget = m_goPlayerOne;
                        }
                        // Only paths to target if they aren't already touching the target
                        else
                        if (playerTwoDistance.magnitude > m_bRange
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().IsAlive)
                        {
                            // moves the enemy in m_bRange

                            if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) > m_bRange)
                            {
                                m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                                m_nmaNavMeshAgent.stoppingDistance = m_bRange;

                            }
                        }
                    }

                    m_nmaNavMeshAgent.transform.LookAt(m_goCurrentTarget.transform);

                    if (Vector3.Distance(m_goCurrentTarget.transform.position, m_nmaNavMeshAgent.transform.position) < m_bRange)
                    {
                        m_bDroneTimer -= Time.deltaTime;
                        if (m_bDroneTimer <= 0)
                        {
                            m_bDroneTimer = 0;
                        }
                        if (m_bDroneTimer == 0)
                        {
                            if (m_goCurrentTarget == m_goPlayerOne)
                            {
                               
                                m_goPlayerOne.GetComponent<PlayerController>().TakeDamage(m_sDamage);
                            }
                            else
                            {
                                
                                m_goPlayerTwo.GetComponent<PlayerController>().TakeDamage(m_sDamage);
                            }
                            m_bDroneTimer = m_bscBossSpawnController.m_bDefaultDroneTimer;
                        }
                    }
                }

                // Turret
                if (p_EtEnemyType == M_btBossType.Turret)
                {
                    m_bRange = m_bscBossSpawnController.m_fbTurretRange;
                    // Calculates the distance from the enemy to each player
                    Vector3 playerOneDistance = transform.position - m_goPlayerOne.transform.position;
                    Vector3 playerTwoDistance = transform.position - m_goPlayerTwo.transform.position;
                    if (playerOneDistance.magnitude < 0)
                        playerOneDistance *= -1;

                    if (playerTwoDistance.magnitude < 0)
                        playerTwoDistance *= -1;
                    if (m_goCurrentTarget == m_goPlayerOne)
                    {
                        // Either player one is dead or player two is closer as long as player two is alive
                        if (!m_goPlayerOne.gameObject.GetComponent<PlayerController>().IsAlive
                            || (playerTwoDistance.magnitude < playerOneDistance.magnitude
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().IsAlive))
                        {
                            //Debug.Log("Target is now player 2");
                            m_goCurrentTarget = m_goPlayerTwo;
                        }
                        // Only paths to target if they aren't already touching the target
                        else
                        if (playerOneDistance.magnitude > m_bRange
                            && m_goPlayerOne.gameObject.GetComponent<PlayerController>().IsAlive)
                        {
                            // Moves the enemy in m_bRange;

                            if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) > m_bRange)
                            {
                                m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                                m_nmaNavMeshAgent.stoppingDistance = m_bRange;

                            }
                        }
                    }
                    else
                    {
                        // Either player two is dead or player one is closer as long as player one is alive
                        if (!m_goPlayerTwo.gameObject.GetComponent<PlayerController>().IsAlive
                            || (playerOneDistance.magnitude < playerTwoDistance.magnitude
                            && m_goPlayerOne.gameObject.GetComponent<PlayerController>().IsAlive))
                        {
                            //Debug.Log("Target is now player 1");
                            m_goCurrentTarget = m_goPlayerOne;
                        }
                        // Only paths to target if they aren't already touching the target
                        else
                        if (playerTwoDistance.magnitude > m_bRange
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().IsAlive)
                        {
                            // moves the enemy in m_bRange

                            if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) > m_bRange)
                            {
                                m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                                m_nmaNavMeshAgent.stoppingDistance = m_bRange;

                            }

                        }

                    }
                    m_nmaNavMeshAgent.transform.LookAt(m_goCurrentTarget.transform);

                    if (Vector3.Distance(m_goCurrentTarget.transform.position, m_nmaNavMeshAgent.transform.position) < m_bRange)
                    {
                        m_bTurretTimer -= Time.deltaTime;
                        if (m_bTurretTimer <= 0)
                        {
                            m_bTurretTimer = 0;
                        }
                        if (m_bTurretTimer == 0)
                        {
                            if (m_goCurrentTarget = m_goPlayerOne)
                            {
                                
                                m_goPlayerOne.GetComponent<PlayerController>().TakeDamage(m_sDamage);
                            }
                            else
                            {
                                
                                m_goPlayerTwo.GetComponent<PlayerController>().TakeDamage(m_sDamage);
                            }
                            m_bTurretTimer = m_bscBossSpawnController.m_bDefaultTurretTimer;
                        }
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
            TakeDamage(other.gameObject.GetComponent<Bullet>().m_iDamage);

            if (other.gameObject.GetComponent<Bullet>().m_iPiercing == 0)
                other.gameObject.GetComponent<Bullet>().m_fBulletCountdown = 0;

            if (m_bHealth == 0)
            {
                // If explosive upgrade has been bought, create explosion based on level
                if (other.gameObject.GetComponent<Bullet>().m_iExplosive != 0)
                {
                    // Spawns explosion at death location if killer has upgrade
                    GameObject obj = Instantiate(m_goExplosion, transform.position, transform.rotation);
                    m_csCameraScript.PlayExplosion();

                    if (other.gameObject.GetComponent<Bullet>().m_iExplosive == 1)
                    {
                        obj.GetComponent<Explosion>().m_iExplosionDamage = other.gameObject.GetComponent<Bullet>().m_iL1Damage;
                        obj.GetComponent<Explosion>().m_fExplosionRadius = other.gameObject.GetComponent<Bullet>().m_fL1Radius;
                    }
                    else
                    {
                        obj.GetComponent<Explosion>().m_iExplosionDamage = other.gameObject.GetComponent<Bullet>().m_iL2Damage;
                        obj.GetComponent<Explosion>().m_fExplosionRadius = other.gameObject.GetComponent<Bullet>().m_fL2Radius;
                    }
                }

            }
        }
    }

    // Applies damage to the object
    public void TakeDamage(int pDamage)
    {
        if (m_bIsAlive)
        {
            m_bHealth -= pDamage;
        }

        if (m_bHealth <= 0)
        {
            m_scShopController.GetComponent<ShopController>().DepositToWallet(20000);
            m_bHealth = 0;
        }
    }
}
