using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSpawnController : MonoBehaviour
{

    // LEAVE THIS TO LIAM NOBODY FUCKING TOUCH IT


    public enum BossType
    {
        Scuttler,
        Turret,
        Drone
    }


    //----------Other----------
    [Header("Other")]
    // Boss Prefabs
    public GameObject m_goScuttlerBossPrefab;
    public GameObject m_goTurretBossPrefab;
    public GameObject m_goDroneBossPrefab;

    public int m_iHealthMultiplierScuttler;
    public int m_iHealthMultiplierDrone;
    public int m_iHealthMultiplierTurret;

    // Access to other classes
    public RoundController m_rcRoundController;
    public BossController m_bcBossController;
    public ShopController m_scShopController;

    public float m_bDefaultTurretTimer;
    public float m_bDefaultDroneTimer;
    public float m_fbDroneRange;
    public float m_fbTurretRange;


    // Variables for the bosses
    public bool m_bHasSpawned;

    // Spawn position
    public GameObject m_goBossSpawnLocation;


  

    private void Awake()
    {
        
    }

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
        if (pBossType == BossType.Scuttler)
        {
            m_bcBossController.m_btEnemyType = 0;
            m_bcBossController.objBoss= Instantiate(m_goScuttlerBossPrefab);
            m_bcBossController.objBoss.SetActive(true);
            m_bcBossController.objBoss.transform.position = m_goBossSpawnLocation.transform.position;
            m_bcBossController.m_bHealth *= 15;
            m_bHasSpawned = true;
        }

        if (pBossType == BossType.Drone)
        {
            m_bcBossController.m_btEnemyType = 2;
            m_bcBossController.objBoss = Instantiate(m_goDroneBossPrefab);
            m_bcBossController.objBoss.SetActive(true);
            m_bcBossController.objBoss.transform.position = m_goBossSpawnLocation.transform.position;
            m_bHasSpawned = true;
        }
        
        if (pBossType == BossType.Turret)
        {
            m_bcBossController.m_btEnemyType = 1;
            m_bcBossController.objBoss = Instantiate(m_goTurretBossPrefab);
            m_bcBossController.objBoss.SetActive(true);
            m_bcBossController.objBoss.transform.position = m_goBossSpawnLocation.transform.position;
            m_bHasSpawned = true;
        }
        m_rcRoundController.m_bBossDead = false;
    }
}
