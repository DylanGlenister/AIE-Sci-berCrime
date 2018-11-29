using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSpawnController : MonoBehaviour
{
    
    public enum BossType
    {
        Scuttler,
        Turret,
        Drone
    }

    // Access to other classes
    public RoundController m_rcRoundController;
    public ShopController m_scShopController;

    // Variables for the bosses
    public bool m_bHasSpawned;

    public int m_bHealthScuttler;
    public int m_bHealthDrone;
    public int m_bHealthTurret;

    // Variables to set for bosses

    public float m_bDefaultTurretTimer;
    public float m_bDefaultDroneTimer;
    public float m_bDefaultScuttlerTimer;
    public float m_fbDroneRange;
    public float m_fbTurretRange;
    
    // Spawn position
    public GameObject m_goBossSpawnLocation1;
    public GameObject m_goBossSpawnLocation2;
    public GameObject m_goBossSpawnLocation3;
    public GameObject m_goBossSpawnLocation4;
    public GameObject m_goBossSpawnLocation5;
    public GameObject m_goBossSpawnLocation6;
    private GameObject m_goChosenSpawnLocation;

    [Header("Boss Prefabs")]
    // Boss Prefabs
    public GameObject m_goScuttlerBossPrefab;
    public GameObject m_goTurretBossPrefab;
    public GameObject m_goDroneBossPrefab;
    
    private void Update()
    {
        if (!m_bHasSpawned)
        {
            switch (m_rcRoundController.m_iCurrentRound)
            {
                case 5:
                    SpawnEnemy(BossType.Scuttler);
                    break;
                case 10:
                    SpawnEnemy(BossType.Turret);
                    break;
                case 15:
                    SpawnEnemy(BossType.Drone);
                    break;
                case 20:
                    SpawnEnemy(BossType.Scuttler);
                    SpawnEnemy(BossType.Drone);
                    SpawnEnemy(BossType.Turret);
                    break;
                default:
                    break;
            }
        }
    }

    public void SpawnEnemy(BossType pBossType)
    {

        int rand = Random.Range(0, 6);

        switch (rand)
        {
            case 0:
                m_goChosenSpawnLocation = m_goBossSpawnLocation1;
                break;
            case 1:
                m_goChosenSpawnLocation = m_goBossSpawnLocation2;
                break;
            case 2:
                m_goChosenSpawnLocation = m_goBossSpawnLocation3;
                break;
            case 3:
                m_goChosenSpawnLocation = m_goBossSpawnLocation4;
                break;
            case 4:
                m_goChosenSpawnLocation = m_goBossSpawnLocation5;
                break;
            case 5:
                m_goChosenSpawnLocation = m_goBossSpawnLocation6;
                break;
        }

        // Instantiates and initialises a boss of a specified type
        if (pBossType == BossType.Scuttler)
        {
            GameObject objNewScuttlerBoss = Instantiate(m_goScuttlerBossPrefab);
            BossController scuttlerBossController = objNewScuttlerBoss.GetComponent<BossController>();
            scuttlerBossController.enabled = false;
            scuttlerBossController.m_btEnemyType = 0;
            objNewScuttlerBoss.transform.position = m_goChosenSpawnLocation.transform.position;
            scuttlerBossController.enabled = true;
            scuttlerBossController.m_bIsAlive = true;
            scuttlerBossController.m_bHealth = m_bHealthScuttler;
            m_bHasSpawned = true;
            if (m_rcRoundController.m_iCurrentRound == 20)
            {
                scuttlerBossController.m_bHealth = m_bHealthScuttler * 2;
            }
            

        }

        if (pBossType == BossType.Turret)
        {
            GameObject objNewTurretBoss = Instantiate(m_goTurretBossPrefab);
            BossController turretBossController = objNewTurretBoss.GetComponent<BossController>();
            turretBossController.enabled = false;
            turretBossController.m_btEnemyType = 1;
            objNewTurretBoss.transform.position = m_goChosenSpawnLocation.transform.position;
            turretBossController.enabled = true;
            turretBossController.m_bHealth = m_bHealthTurret;
            turretBossController.m_bIsAlive = true;
            m_bHasSpawned = true;
            if(m_rcRoundController.m_iCurrentRound == 20)
            {
                turretBossController.m_bHealth = m_bHealthTurret * 2;
            }
        }
        
        if (pBossType == BossType.Drone)
        {
            GameObject objNewDroneBoss = Instantiate(m_goDroneBossPrefab);
            BossController droneBossController = objNewDroneBoss.GetComponent<BossController>();
            droneBossController.enabled = false;
            droneBossController.m_btEnemyType = 2;
            objNewDroneBoss.transform.position = m_goChosenSpawnLocation.transform.position;
            droneBossController.enabled = true;
            droneBossController.m_bHealth = m_bHealthDrone;
            droneBossController.m_bIsAlive = true;
            m_bHasSpawned = true;
            if (m_rcRoundController.m_iCurrentRound == 20)
            {
                droneBossController.m_bHealth = m_bHealthDrone * 2;
            }
        }
        m_rcRoundController.m_bBossDead = false;
    }
}
