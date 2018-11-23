using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnController : MonoBehaviour
{
    public enum EnemyType
    {
        Scuttler,
        Turret,
        Drone
    }

    public bool m_bSpawningEnabled = true;
    public bool funMode = false;
    
    public float m_fSpawnDelay = 1f;
    private float m_fSpawnTimer;
    public float m_fDroneSpawnDelay;
    public float m_fTurretSpawnDelay;

    //----------Scuttler----------
    [Header("Scuttler")]
    public int m_iCurrentScuttlerCount;
    public int m_iCurrentScuttlersSpawnedThisRound;
    public int m_iCurrentScuttlersKilledThisRound;

    public int m_iMaxScuttlersForRound;
    public int m_iStartMaxScuttlersForRound = 100;

    public int m_iMaxScuttlersAtOnce = 10;
    public int m_iPotentionalMaxScuttlersAtOnce = 200;
    public int m_iScuttlerDefaultHealth;
    //----------Turret----------
    [Header("Turret")]
    public int m_iCurrentTurretCount;
    public int m_iCurrentTurretsSpawnedThisRound;
    public int m_iCurrentTurretsKilledThisRound;

    public int m_iMaxTurretsForRound;
    public int m_iStartMaxTurretsForRound = 50;

    public int m_iMaxTurretsAtOnce = 20;
    public int m_iPotentionalMaxTurretsAtOnce = 200;
    public int m_iTurretDefaultHealth;
    //----------Drone----------
    [Header("Drone")]
    public int m_iCurrentDroneCount;
    public int m_iCurrentDronesSpawnedThisRound;
    public int m_iCurrentDronesKilledThisRound;

    public int m_iMaxDronesForRound;
    public int m_iStartMaxDronesForRound = 50;

    public int m_iMaxDronesAtOnce = 20;
    public int m_iPotentionalMaxDronesAtOnce = 200;
    public int m_iDroneDefaultHealth;

    //---Other---
    [Header("Other")]
    public RoundController m_rcRoundController;

    private List<GameObject> m_lgoScuttlerList;
    private List<GameObject> m_lgoDroneList;
    private List<GameObject> m_lgoTurretList;
    // Reference to the enemy prefab
    public GameObject m_goScuttlerPrefab;
    public GameObject m_goDronePrefab;
    public GameObject m_goTurretPrefab;

    // Gameobjects at the positions they spawn at
    public GameObject m_goSpawnLocation1;
    public GameObject m_goSpawnLocation2;
    public GameObject m_goSpawnLocation3;
    public GameObject m_goSpawnLocation4;
    public GameObject m_goSpawnLocation5;
    public GameObject m_goSpawnLocation6;

    private GameObject m_goChosenSpawnLocation;


    // Leave this here for now, will be arranged in the correct position during the gold stage

    public float m_DefaultScuttlerTimer = 0.5f;
    public float m_DefaultTurretTimer = 1f;
    public float m_DefaultDroneTimer = 0.2f;
    public float m_fDroneRange = 5;
    public float m_fTurretRange = 20;

    private void Awake ()
    {
        // Initialises variables
        m_fSpawnTimer = m_fSpawnDelay;
        m_fTurretSpawnDelay = 5;
        m_fDroneSpawnDelay = 3;
        m_iMaxScuttlersForRound = m_iStartMaxScuttlersForRound;
        m_iMaxDronesForRound = 0;
        m_iMaxTurretsForRound = 0;

        // Populates the inactive enemies list
        m_lgoScuttlerList = new List<GameObject>();
        m_lgoDroneList = new List<GameObject>();
        m_lgoTurretList = new List<GameObject>();

        for (int i = 0; i < m_iPotentionalMaxDronesAtOnce; i++)
        {
            GameObject objScuttler = Instantiate(m_goScuttlerPrefab);
            objScuttler.SetActive(false);
            m_lgoScuttlerList.Add(objScuttler);
            
        }
        for (int i = 0; i < m_iPotentionalMaxScuttlersAtOnce; i++)
        {
            GameObject objDrone = Instantiate(m_goDronePrefab);
            objDrone.SetActive(false);
            m_lgoDroneList.Add(objDrone);
            
        }
        for (int i = 0; i < m_iPotentionalMaxTurretsAtOnce; i++)
        {
            GameObject objTurret = Instantiate(m_goTurretPrefab);
            objTurret.SetActive(false);
            m_lgoTurretList.Add(objTurret);
            
        }
    }

    private void Update()
    {
        if (!m_bSpawningEnabled)
            return;

        // Only 'spawns' enemies as long at the max for the current round and the max on screen haven't been reached

        if (m_rcRoundController.m_iCurrentRound == 1)
        {
            
            if (m_iCurrentScuttlerCount < m_iMaxScuttlersAtOnce && m_iCurrentScuttlersSpawnedThisRound <= m_iMaxScuttlersForRound)
            {
                m_fSpawnTimer -= Time.deltaTime;

                if (m_fSpawnTimer < 0)
                    m_fSpawnTimer = 0;

                if (m_fSpawnTimer == 0)
                {
                    if (m_iCurrentScuttlerCount < m_iMaxScuttlersAtOnce && m_iCurrentScuttlersSpawnedThisRound < m_iMaxScuttlersForRound)
                    {
                        SpawnEnemy(EnemyType.Scuttler);
                        m_fSpawnTimer = m_fSpawnDelay;
                    }
                }
            }
        }

        else if (m_rcRoundController.m_iCurrentRound == 2)
        {
            
            if (m_iCurrentScuttlerCount < m_iMaxScuttlersAtOnce && m_iCurrentScuttlersSpawnedThisRound <= m_iMaxScuttlersForRound
            && m_iCurrentTurretCount < m_iMaxTurretsAtOnce && m_iCurrentTurretsSpawnedThisRound <= m_iMaxTurretsForRound)
            {
                m_fSpawnTimer -= Time.deltaTime;

                if (m_fSpawnTimer < 0)
                    m_fSpawnTimer = 0;
               

                if (m_fSpawnTimer == 0)
                {
                    int rand = Random.Range(0, 2);
                    switch (rand)
                    {
                        case 0:
                            if (m_iCurrentScuttlerCount < m_iMaxScuttlersAtOnce && m_iCurrentScuttlersSpawnedThisRound < m_iMaxScuttlersForRound)
                                SpawnEnemy(EnemyType.Scuttler);
                            break;
                        case 1:
                            if (m_iCurrentTurretCount < m_iMaxTurretsAtOnce && m_iCurrentTurretsSpawnedThisRound < m_iMaxTurretsForRound)
                                SpawnEnemy(EnemyType.Turret);
                            break;
                    }
                    m_fSpawnTimer = m_fSpawnDelay;
                }
            }
        }
        else
        {
            
            if (m_iCurrentScuttlerCount < m_iMaxScuttlersAtOnce && m_iCurrentScuttlersSpawnedThisRound <= m_iMaxScuttlersForRound
                && m_iCurrentTurretCount < m_iMaxTurretsAtOnce && m_iCurrentTurretsSpawnedThisRound <= m_iMaxTurretsForRound
                && m_iCurrentDroneCount < m_iMaxDronesAtOnce && m_iCurrentDronesSpawnedThisRound <= m_iMaxDronesForRound)
            {
                m_fSpawnTimer -= Time.deltaTime;

                if (m_fSpawnTimer < 0)
                    m_fSpawnTimer = 0;

               
                if (m_fSpawnTimer == 0)
                {
                    int rand = Random.Range(0, 3);
                    switch (rand)
                    {
                        case 0:
                            if (m_iCurrentScuttlerCount < m_iMaxScuttlersAtOnce && m_iCurrentScuttlersSpawnedThisRound < m_iMaxScuttlersForRound)
                                SpawnEnemy(EnemyType.Scuttler);
                            break;
                        case 1:
                            if (m_iCurrentTurretCount < m_iMaxTurretsAtOnce && m_iCurrentTurretsSpawnedThisRound < m_iMaxTurretsForRound)
                                SpawnEnemy(EnemyType.Turret);
                            break;
                        case 2:
                            if (m_iCurrentDroneCount < m_iMaxDronesAtOnce && m_iCurrentDronesSpawnedThisRound < m_iMaxDronesForRound)
                                SpawnEnemy(EnemyType.Drone);
                            break;
                    }
                    m_fSpawnTimer = m_fSpawnDelay;
                    
                }
            }
        }
        

        if (m_rcRoundController.m_iCurrentRound == 1)
        {
            if (m_iCurrentScuttlersKilledThisRound == m_iMaxScuttlersForRound)
            {
                m_bSpawningEnabled = false;
                m_rcRoundController.m_bEnemiesDead = true;
            }
        }
        else if (m_rcRoundController.m_iCurrentRound == 2)
        {
            if (m_iCurrentScuttlersKilledThisRound == m_iMaxScuttlersForRound && m_iCurrentTurretsKilledThisRound == m_iMaxTurretsForRound)
            {
                m_bSpawningEnabled = false;
                m_rcRoundController.m_bEnemiesDead = true;
            }
        }
        // Ends round once all enemies required have been killed
        else if (m_rcRoundController.m_iCurrentRound >= 3)
        {
            if (m_iCurrentScuttlersKilledThisRound == m_iMaxScuttlersForRound
                && m_iCurrentDronesKilledThisRound == m_iMaxDronesForRound
                && m_iCurrentTurretsKilledThisRound == m_iMaxTurretsForRound)
            {
                m_bSpawningEnabled = false;
                m_rcRoundController.m_bEnemiesDead = true;
            }
        }
    }
    public void SpawnEnemy(EnemyType pEnemyType)
    {
        // Chooses random spawn location
        int rand = Random.Range(0, 6);

        switch (rand)
        {
            case 0:
                m_goChosenSpawnLocation = m_goSpawnLocation1;
                break;
            case 1:
                m_goChosenSpawnLocation = m_goSpawnLocation2;
                break;
            case 2:
                m_goChosenSpawnLocation = m_goSpawnLocation3;
                break;
            case 3:
                m_goChosenSpawnLocation = m_goSpawnLocation4;
                break;
            case 4:
                m_goChosenSpawnLocation = m_goSpawnLocation5;
                break;
            case 5:
                m_goChosenSpawnLocation = m_goSpawnLocation6;
                break;
        }


        if (pEnemyType == EnemyType.Scuttler)
        {
            for (int i = 0; i < m_lgoScuttlerList.Count; i++)
            {
                if (!m_lgoScuttlerList[i].activeInHierarchy)
                {
                    m_lgoScuttlerList[i].GetComponent<EnemyController>().m_iEnemyType = 0;
                    m_lgoScuttlerList[i].transform.position = m_goChosenSpawnLocation.transform.position;
                    m_lgoScuttlerList[i].transform.rotation = m_goChosenSpawnLocation.transform.rotation;
                    m_lgoScuttlerList[i].GetComponent<EnemyController>().m_iHealth = m_iScuttlerDefaultHealth * m_rcRoundController.m_iCurrentRound;
                    m_lgoScuttlerList[i].GetComponent<EnemyController>().IsAlive = true;
                    m_lgoScuttlerList[i].SetActive(true);
                    m_lgoScuttlerList[i].GetComponent<NavMeshAgent>().enabled = true;
                    m_iCurrentScuttlerCount += 1;
                    m_iCurrentScuttlersSpawnedThisRound += 1;
                    break;
                }
            }
        }

        if (pEnemyType == EnemyType.Drone)
        {
            for (int i = 0; i < m_lgoDroneList.Count; i++)
            {
                if (!m_lgoDroneList[i].activeInHierarchy)
                {
                    m_lgoDroneList[i].GetComponent<EnemyController>().m_iEnemyType = 2;
                    m_lgoDroneList[i].transform.position = m_goChosenSpawnLocation.transform.position;
                    m_lgoDroneList[i].transform.rotation = m_goChosenSpawnLocation.transform.rotation;
                    m_lgoDroneList[i].GetComponent<EnemyController>().m_iHealth = m_iDroneDefaultHealth * m_rcRoundController.m_iCurrentRound;
                    m_lgoDroneList[i].GetComponent<EnemyController>().IsAlive = true;
                    m_lgoDroneList[i].SetActive(true);
                    m_lgoDroneList[i].GetComponent<NavMeshAgent>().enabled = true;
                    m_iCurrentDroneCount += 1;
                    m_iCurrentDronesSpawnedThisRound += 1;
                    break;
                }
            }
        }

        if (pEnemyType == EnemyType.Turret)
        {
            for (int i = 0; i < m_lgoTurretList.Count; i++)
            {
                if (!m_lgoTurretList[i].activeInHierarchy)
                {
                    m_lgoTurretList[i].GetComponent<EnemyController>().m_iEnemyType = 1;
                    m_lgoTurretList[i].transform.position = m_goChosenSpawnLocation.transform.position;
                    m_lgoTurretList[i].transform.rotation = m_goChosenSpawnLocation.transform.rotation;
                    m_lgoTurretList[i].GetComponent<EnemyController>().m_iHealth = m_iTurretDefaultHealth * m_rcRoundController.m_iCurrentRound;
                    m_lgoTurretList[i].GetComponent<EnemyController>().IsAlive = true;
                    m_lgoTurretList[i].SetActive(true);
                    m_lgoTurretList[i].GetComponent<NavMeshAgent>().enabled = true;
                    m_iCurrentTurretCount += 1;
                    m_iCurrentTurretsSpawnedThisRound += 1;
                    break;
                }
            }
        }

    }
}
