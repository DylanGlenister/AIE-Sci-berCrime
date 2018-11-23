using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public ShopController m_scShopController;
    public EnemySpawnController m_escEnemySpawnController;
    public CameraScript m_csCameraScript;

    public enum EnemyType
    {
        Drone,
        Scuttler,
        Turret
    }
    
    public bool IsAlive { get; set; }

    public int m_iHealth;
    public int m_iDamage;
    public int m_iReward;    //New product by apple
    public int m_iEnemyType;

    public float m_fRange;
    public float m_fTurretTimer;
    public float m_fScuttlerTimer;
    public float m_fDroneTimer;

    public float m_fPlayerSafeBubbleSize = 1.3f;

    // References to the players
    public GameObject m_goPlayerOne;
    public GameObject m_goPlayerTwo;
    public GameObject m_goCurrentTarget;

    public GameObject m_goExplosionPrefab;
    public GameObject m_goLinePrefab;

    private NavMeshAgent m_nmaNavMeshAgent;

    private void Awake()
    {
        m_scShopController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ShopController>();
        m_escEnemySpawnController = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemySpawnController>();
        m_csCameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();

        m_goPlayerOne = GameObject.FindGameObjectWithTag("PlayerOne");
        m_goPlayerTwo = GameObject.FindGameObjectWithTag("PlayerTwo");
        m_goCurrentTarget = m_goPlayerOne;

        m_nmaNavMeshAgent = GetComponent<NavMeshAgent>();

        IsAlive = true;

        m_fTurretTimer = m_escEnemySpawnController.m_DefaultTurretTimer;
        m_fDroneTimer = m_escEnemySpawnController.m_DefaultDroneTimer;
        m_fScuttlerTimer = m_escEnemySpawnController.m_DefaultScuttlerTimer;
    }

    private void Update ()
    {
        // Called when the enemy dies
        if (m_iHealth == 0)
        {
            IsAlive = false;
            if (m_iEnemyType == 0)
            {
                m_escEnemySpawnController.m_iCurrentScuttlerCount -= 1;
                m_escEnemySpawnController.m_iCurrentScuttlersKilledThisRound += 1;
            }
            else if (m_iEnemyType == 1)
            {
                m_escEnemySpawnController.m_iCurrentTurretCount -= 1;
                m_escEnemySpawnController.m_iCurrentTurretsKilledThisRound += 1;
            }
            else if (m_iEnemyType == 2)
            {
                m_escEnemySpawnController.m_iCurrentDroneCount -= 1;
                m_escEnemySpawnController.m_iCurrentDronesKilledThisRound += 1;
            }
            // Hides object from scene
            gameObject.SetActive(false);
        }

        // Assign id to the enemies
        if (IsAlive && (m_goPlayerOne || m_goPlayerTwo))
        {
            if (m_iEnemyType == 0)
            {
                EnemyMove(EnemyType.Scuttler);
                m_iReward = 20;
            }
            else if (m_iEnemyType == 1)
            {
                EnemyMove(EnemyType.Turret);
                m_iReward = 40;
            }
            else if (m_iEnemyType == 2)
            {
                EnemyMove(EnemyType.Drone);
                m_iReward = 30;
            }
        }
    }

    private void EnemyMove (EnemyType p_EtEnemyType)
    {
        if (IsAlive && (m_goPlayerOne || m_goPlayerTwo))
        {
            // Scuttler
            if (p_EtEnemyType == EnemyType.Scuttler)
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

                // Scuttler attacking player
                if (Vector3.Distance(m_goCurrentTarget.transform.position, m_nmaNavMeshAgent.transform.position) < 2.0f)
                {
                    m_fScuttlerTimer -= Time.deltaTime;
                    if (m_fScuttlerTimer <= 0)
                        m_fScuttlerTimer = 0;

                    if (m_fScuttlerTimer == 0)
                    {
                        if (m_goCurrentTarget == m_goPlayerOne)
                            m_goPlayerOne.GetComponent<PlayerController>().TakeDamage(m_iDamage);
                        else
                            m_goPlayerTwo.GetComponent<PlayerController>().TakeDamage(m_iDamage);

                        m_fScuttlerTimer = m_escEnemySpawnController.m_DefaultScuttlerTimer;
                    }
                }
            }
            // Turret
            else if (p_EtEnemyType == EnemyType.Turret)
            {
                m_fRange = m_escEnemySpawnController.m_fTurretRange;
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
                    if (playerOneDistance.magnitude > m_fRange
                        && m_goPlayerOne.gameObject.GetComponent<PlayerController>().IsAlive)
                    {
                        // Moves the enemy in range
                        if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) > m_fRange)
                        {
                            m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                            m_nmaNavMeshAgent.stoppingDistance = m_fRange;
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
                    if (playerTwoDistance.magnitude > m_fRange
                        && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().IsAlive)
                    {
                        // Moves the enemy in range
                        if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) > m_fRange)
                        {
                            m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                            m_nmaNavMeshAgent.stoppingDistance = m_fRange;
                        }
                    }
                }

                // Drone attacking player
                if (Vector3.Distance(m_goCurrentTarget.transform.position, m_nmaNavMeshAgent.transform.position) < m_fRange)
                {
                    m_fTurretTimer -= Time.deltaTime;
                    if (m_fTurretTimer <= 0)
                        m_fTurretTimer = 0;

                    if (m_fTurretTimer == 0)
                    {
                        if (m_goCurrentTarget == m_goPlayerOne)
                            m_goPlayerOne.GetComponent<PlayerController>().TakeDamage(m_iDamage);
                        else
                            m_goPlayerTwo.GetComponent<PlayerController>().TakeDamage(m_iDamage);

                        // Draws a line from the enemy to the player to show they are being shot
                        GameObject bulletTrace = Instantiate(m_goLinePrefab);
                        bulletTrace.GetComponent<Line>().DrawLineToTarget(gameObject, m_goCurrentTarget);

                        m_fTurretTimer = m_escEnemySpawnController.m_DefaultTurretTimer;
                    }
                }
            }
            // Drone
            else if (p_EtEnemyType == EnemyType.Drone)
            {
                m_fRange = m_escEnemySpawnController.m_fDroneRange;
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
                    if (playerOneDistance.magnitude > m_fRange
                        && m_goPlayerOne.gameObject.GetComponent<PlayerController>().IsAlive)
                    {
                        if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) > m_fRange)
                        {
                            m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                            m_nmaNavMeshAgent.stoppingDistance = m_fRange;
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
                    if (playerTwoDistance.magnitude > m_fRange
                        && m_goPlayerTwo.gameObject.GetComponent<PlayerController>().IsAlive)
                    {
                        // Moves the enemy in range
                        if (Vector3.Distance(m_nmaNavMeshAgent.transform.position, m_goCurrentTarget.transform.position) > m_fRange)
                        {
                            m_nmaNavMeshAgent.SetDestination(m_goCurrentTarget.transform.position);
                            m_nmaNavMeshAgent.stoppingDistance = m_fRange;
                        }
                    }
                }

                // Turret attacking player
                if (Vector3.Distance(m_goCurrentTarget.transform.position, m_nmaNavMeshAgent.transform.position) < m_fRange)
                {
                    m_fDroneTimer -= Time.deltaTime;
                    if (m_fDroneTimer <= 0)
                        m_fDroneTimer = 0;

                    if (m_fDroneTimer == 0)
                    {
                        if (m_goCurrentTarget == m_goPlayerOne)
                            m_goPlayerOne.GetComponent<PlayerController>().TakeDamage(m_iDamage);
                        else
                            m_goPlayerTwo.GetComponent<PlayerController>().TakeDamage(m_iDamage);

                        // Draws a line from the enemy to the player to show they are being shot
                        GameObject bulletTrace = Instantiate(m_goLinePrefab);
                        bulletTrace.GetComponent<Line>().DrawLineToTarget(gameObject, m_goCurrentTarget);

                        m_fDroneTimer = m_escEnemySpawnController.m_DefaultDroneTimer;
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
                    GameObject obj = Instantiate(m_goExplosionPrefab, transform.position, transform.rotation);
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
        m_iHealth -= pDamage;
        if (m_iHealth <= 0)
        {
            m_iHealth = 0;
            m_scShopController.DepositToWallet(m_iReward);
        }
    }
}