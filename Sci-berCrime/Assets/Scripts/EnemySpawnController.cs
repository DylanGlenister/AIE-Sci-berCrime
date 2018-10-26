using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public float m_fSpawnDelay = 0.25f;
    private float m_fSpawnTimer;

    //----------Scuttler----------
    [Header("Scuttler")]
    public int m_iCurrentScuttlerCount;
    public int m_iCurrentScuttlersKilledThisRound;

    public int m_iMaxScuttlersForRound;
    public int m_iStartMaxScuttlersForRound = 100;

    public int m_iMaxScuttlersAtOnce = 10;
    public int m_iPotentionalMaxScuttlersAtOnce = 200;
    //----------Turret----------
    [Header("Turret")]
    public int m_iCurrentTurretCount;
    public int m_iCurrentTurretsKilledThisRound;

    public int m_iMaxTurretsForRound;
    public int m_iStartMaxTurretsForRound = 50;

    public int m_iMaxTurretsAtOnce = 20;
    public int m_iPotentionalMaxTurretsAtOnce = 200;
    //----------Drone----------
    [Header("Drone")]
    public int m_iCurrentDroneCount;
    public int m_iCurrentDronesKilledThisRound;

    public int m_iMaxDronesForRound;
    public int m_iStartMaxDronesForRound = 50;

    public int m_iMaxDronesAtOnce = 20;
    public int m_iPotentionalMaxDronesAtOnce = 200;
    
    [Header("Other")]
    public RoundController m_rcRoundController;

    private List<GameObject> m_lgoEnemyList;

    // Reference to the enemy prefab
    public GameObject m_goEnemyPrefab;

    // Gameobjects at the positions they spawn at
    public GameObject m_goSpawnLocation1;
    public GameObject m_goSpawnLocation2;
    public GameObject m_goSpawnLocation3;
    public GameObject m_goSpawnLocation4;
    public GameObject m_goSpawnLocation5;
    public GameObject m_goSpawnLocation6;

    private GameObject m_goChosenSpawnLocation;

    void Awake ()
    {
        // Initialises variables
        m_fSpawnTimer = m_fSpawnDelay;
        m_iMaxScuttlersForRound = m_iStartMaxScuttlersForRound;

        // Populates the inactive enemies list
        m_lgoEnemyList = new List<GameObject>();
        for (int i = 0; i < m_iPotentionalMaxScuttlersAtOnce; i++)
        {
            GameObject obj = Instantiate(m_goEnemyPrefab);
            obj.SetActive(false);
            m_lgoEnemyList.Add(obj);
        }
    }

    void Update()
    {
        if (!m_bSpawningEnabled)
            return;
        
        // Only 'spawns' enemies as long at the max for the current round and the max on screen haven't been reached
        if (m_iCurrentScuttlerCount < m_iMaxScuttlersAtOnce && m_iCurrentScuttlersKilledThisRound < m_iMaxScuttlersForRound)
        {
            m_fSpawnTimer -= Time.deltaTime;

            if (m_fSpawnTimer < 0)
                m_fSpawnTimer = 0;

            if (m_fSpawnTimer == 0)
            {
                SpawnEnemy(EnemyType.Drone);
                m_fSpawnTimer = m_fSpawnDelay;
            }
        }

        // Ends round once all enemies required have been killed
        if (m_iCurrentScuttlersKilledThisRound == m_iMaxScuttlersForRound)
        {
            m_bSpawningEnabled = false;
            m_rcRoundController.m_bRoundOver = true;
        }
    }

    public void SpawnEnemy (EnemyType pEnemyType)
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

        // Picks the first enemy that isn't enable and spawns it in
        for (int i = 0; i < m_lgoEnemyList.Count; i++)
        {
            if (!m_lgoEnemyList[i].activeInHierarchy)
            {
                m_lgoEnemyList[i].transform.position = m_goChosenSpawnLocation.transform.position;
                m_lgoEnemyList[i].transform.rotation = m_goChosenSpawnLocation.transform.rotation;
                m_lgoEnemyList[i].GetComponent<EnemyController>().m_iHealth = 100;
                m_lgoEnemyList[i].GetComponent<EnemyController>().IsAlive = true;
                m_lgoEnemyList[i].SetActive(true);
                break;
            }
        }
    }
}
