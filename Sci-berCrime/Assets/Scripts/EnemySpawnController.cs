using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public bool m_bSpawningEnabled = true;

    public int m_iEnemyMax = 1000;
    public int m_iEnemyCount;

    public float m_fSpawnDelay = 0.1f;
    private float m_fSpawnTimer;

    private List<GameObject> m_lEnemyList = new List<GameObject>();

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

    void Start()
    {
        m_fSpawnTimer = m_fSpawnDelay;
    }

    void Update()
    {
        if (!m_bSpawningEnabled || m_lEnemyList.Count >= m_iEnemyMax)
            return;

        m_fSpawnTimer -= Time.deltaTime;
        m_fSpawnTimer = Mathf.Max(m_fSpawnTimer, 0);

        if (m_fSpawnTimer == 0)
        {
            // Chooses random spawn location
            int rand = Random.Range(0, 5);

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

            GameObject newEnemy = Instantiate(m_goEnemyPrefab, m_goChosenSpawnLocation.transform.position, m_goSpawnLocation1.transform.rotation);
            m_lEnemyList.Add(newEnemy);
            m_fSpawnTimer = m_fSpawnDelay;
            //Debug.Log("Enemy #" + m_lEnemyList.Count + " has spawned");
            m_iEnemyCount += 1;
        }
    }
}
