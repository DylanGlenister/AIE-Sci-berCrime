using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{


    // WORK ON THE DISTANCE AND STOPPING DISTANCE

    public ShopController m_scShopController;
    public EnemySpawnController m_escEnemySpawnController;
    
    public bool IsAlive { get; set; }

    public int m_iHealth;
    public int m_iDamage;

    public float range;

    public float m_TurretTimer;
    public float m_DroneTimer;

    public float m_fPlayerSafeBubbleSize = 1.3f;


    public int m_etEnemyType;

    public enum M_etEnemyType
    {
        Drone,
        Scuttler,
        Turret
    }
    

    // References to the players
    public GameObject m_goPlayerOne;
    public GameObject m_goPlayerTwo;
    public GameObject m_goCurrentTarget;

    public GameObject m_goExplosion;

    private NavMeshAgent m_nmaNavMeshAgent;

    private void Awake()
    {
        m_TurretTimer = m_escEnemySpawnController.m_DefaultTurretTimer;
        m_DroneTimer = m_escEnemySpawnController.m_DefaultDroneTimer;
        m_nmaNavMeshAgent = GetComponent<NavMeshAgent>();
        m_goPlayerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        m_goPlayerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
        m_scShopController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ShopController>();
        m_escEnemySpawnController = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawnController>();
        m_goCurrentTarget = m_goPlayerOne;

        IsAlive = true;
    }

    private void Update()
    {
        if (m_iHealth == 0)
        {

            IsAlive = false;
            m_escEnemySpawnController.m_iCurrentScuttlerCount -= 1;
            m_escEnemySpawnController.m_iCurrentScuttlersKilledThisRound += 1;
            // Hides object from scene
            gameObject.SetActive(false);
        }
        // Assign id to the enemies
        if (IsAlive && (m_goPlayerOne || m_goPlayerTwo))
        {
            if (m_etEnemyType == 0)
            {
                EnemyMove(M_etEnemyType.Scuttler);
            }
            else if (m_etEnemyType == 1)
            {
                EnemyMove(M_etEnemyType.Turret);
            }
            else if (m_etEnemyType == 2)
            {
                EnemyMove(M_etEnemyType.Drone);
            }
        }
    }

    private void EnemyMove(M_etEnemyType p_EtEnemyType)
    {
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



                //scuttler
                if (p_EtEnemyType == M_etEnemyType.Scuttler)
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
                        else if (playerOneDistance.magnitude > m_fPlayerSafeBubbleSize
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
                        else if (playerTwoDistance.magnitude > m_fPlayerSafeBubbleSize
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                        {
                            //Goes to target's position
                            m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                        }
                    }

                }
                //drone
                else if (p_EtEnemyType == M_etEnemyType.Drone)
                {
                    range = m_escEnemySpawnController.m_fDroneRange;
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
                        if (!m_goPlayerOne.gameObject.GetComponent<PlayerController>().m_bIsAlive
                            || (playerTwoDistance.magnitude < playerOneDistance.magnitude
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive))
                        {
                            //Debug.Log("Target is now player 2");
                            m_goCurrentTarget = m_goPlayerTwo;
                        }
                        // Only paths to target if they aren't already touching the target
                        else 
                        if (playerOneDistance.magnitude > range
                            && m_goPlayerOne.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                        {


                            if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) > range)
                            {
                                m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                                m_nmaNavMeshAgent.stoppingDistance = range;
                                
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
                        if (playerTwoDistance.magnitude > range
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                        {
                            // moves the enemy in range

                            if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) > range)
                            {
                                m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                                m_nmaNavMeshAgent.stoppingDistance = range;

                            }
                        }
                    }

                    if (Vector3.Distance(m_goCurrentTarget.transform.position, m_nmaNavMeshAgent.transform.position) < range)
                    {
                        m_DroneTimer -= Time.deltaTime;
                        if (m_DroneTimer <= 0)
                        {
                            m_DroneTimer = 0;
                        }
                        if (m_DroneTimer == 0)
                        {
                            if (m_goCurrentTarget == m_goPlayerOne)
                            {
                                Debug.Log("Drone has attacked");
                                m_goPlayerOne.GetComponent<PlayerController>().TakeDamage(m_iDamage);
                            }
                            else
                            {
                                Debug.Log("Drone has attacked player 2");
                                m_goPlayerTwo.GetComponent<PlayerController>().TakeDamage(m_iDamage);
                            }
                            m_DroneTimer = m_escEnemySpawnController.m_DefaultDroneTimer;
                        }
                    }
                }

                // Turret
                if (p_EtEnemyType == M_etEnemyType.Turret)
                {
                    range = m_escEnemySpawnController.m_fTurretRange;
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
                        if (!m_goPlayerOne.gameObject.GetComponent<PlayerController>().m_bIsAlive
                            || (playerTwoDistance.magnitude < playerOneDistance.magnitude
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive))
                        {
                            //Debug.Log("Target is now player 2");
                            m_goCurrentTarget = m_goPlayerTwo;
                        }
                        // Only paths to target if they aren't already touching the target
                        else
                        if (playerOneDistance.magnitude > range
                            && m_goPlayerOne.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                        {
                            // Moves the enemy in range;

                            if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) > range)
                            {
                                m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                                m_nmaNavMeshAgent.stoppingDistance = range;

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
                        if (playerTwoDistance.magnitude > range
                            && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().m_bIsAlive)
                        {
                            // moves the enemy in range

                            if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) > range)
                            {
                                m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                                m_nmaNavMeshAgent.stoppingDistance = range;

                            }

                        }

                    }
                    if (Vector3.Distance(m_goCurrentTarget.transform.position, m_nmaNavMeshAgent.transform.position) < range)
                    {
                        m_TurretTimer -= Time.deltaTime;
                        if (m_TurretTimer <= 0)
                        {
                            m_TurretTimer = 0;
                        }
                        if (m_TurretTimer == 0)
                        {
                            if (m_goCurrentTarget = m_goPlayerOne)
                            {
                                Debug.Log("Turret Attacked");
                                m_goPlayerOne.GetComponent<PlayerController>().TakeDamage(m_iDamage);
                            }
                            else
                            {
                                Debug.Log("Turret Attacked Player 2");
                                m_goPlayerTwo.GetComponent<PlayerController>().TakeDamage(m_iDamage);
                            }
                            m_TurretTimer = m_escEnemySpawnController.m_DefaultTurretTimer;
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


    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().m_iDamage);
            
            if (other.gameObject.GetComponent<Bullet>().m_iPiercing == 0)
                other.gameObject.GetComponent<Bullet>().m_fBulletCountdown = 0;

            if (m_iHealth == 0)
            {
                // If explosive upgrade has been bought, create explosion based on level
                if (other.gameObject.GetComponent<Bullet>().m_iExplosive != 0)
                {
                    // Spawns explosion at death location if killer has upgrade
                    GameObject obj = Instantiate(m_goExplosion, transform.position, transform.rotation);

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
    public void TakeDamage (int pDamage)
    {
        
        m_iHealth -= pDamage;
        m_scShopController.GetComponent<ShopController>().DepositToWallet(pDamage/4);
        if (m_iHealth < 0)
            m_iHealth = 0;
    }
}