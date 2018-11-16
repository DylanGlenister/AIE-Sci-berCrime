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

    public int m_iHealthMultiplierScuttler;
    public int m_iHealthMultiplierDrone;
    public int m_iHealthMultiplierTurret;
    
    public float m_bDefaultTurretTimer;
    public float m_bDefaultDroneTimer;
    public float m_fbDroneRange;
    public float m_fbTurretRange;
    
    // Spawn position
    public GameObject m_goBossSpawnLocation;

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
        // Instantiates and initialises a boss of a specified type
        if (pBossType == BossType.Scuttler)
        {
            GameObject objNewScuttlerBoss = Instantiate(m_goScuttlerBossPrefab);
            BossController scuttlerBossController = objNewScuttlerBoss.GetComponent<BossController>();
            scuttlerBossController.m_btEnemyType = 0;
            scuttlerBossController.m_bHealth = 1500;
            objNewScuttlerBoss.transform.position = m_goBossSpawnLocation.transform.position;
            m_bHasSpawned = true;
        }

        if (pBossType == BossType.Turret)
        {
            GameObject objNewTurretBoss = Instantiate(m_goTurretBossPrefab);
            BossController scuttlerBossController = objNewTurretBoss.GetComponent<BossController>();
            scuttlerBossController.m_btEnemyType = 1;
            scuttlerBossController.m_bHealth = 1500;
            objNewTurretBoss.transform.position = m_goBossSpawnLocation.transform.position;
            m_bHasSpawned = true;
        }
        
        if (pBossType == BossType.Drone)
        {
            GameObject objNewDroneBoss = Instantiate(m_goDroneBossPrefab);
            BossController scuttlerBossController = objNewDroneBoss.GetComponent<BossController>();
            scuttlerBossController.m_btEnemyType = 2;
            scuttlerBossController.m_bHealth = 1500;
            objNewDroneBoss.transform.position = m_goBossSpawnLocation.transform.position;
            m_bHasSpawned = true;
        }
        m_rcRoundController.m_bBossDead = false;
    }
}
