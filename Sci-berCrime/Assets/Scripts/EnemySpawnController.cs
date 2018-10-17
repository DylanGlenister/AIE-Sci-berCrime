using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public bool m_bSpawningEnabled = true;

    public int m_iEnemyMax = 200;
    public int m_iEnemyCount;

    public float m_fSpawnDelay = 1f;
    public float m_fSpawnTimer;

    private List<GameObject> m_lEnemyList = new List<GameObject>();

    // Reference to the enemy prefab
    public GameObject m_goEnemyPrefab;

    // Gameobjects at the positions they spawn at
    public GameObject m_goSpawnLocation;

    void Start()
    {
        m_fSpawnTimer = m_fSpawnDelay;
    }

    void Update()
    {
        if (m_bSpawningEnabled)
        {
            if (m_lEnemyList.Count >= m_iEnemyMax)
                return;

            m_fSpawnTimer -= Time.deltaTime;
            m_fSpawnTimer = Mathf.Max(m_fSpawnTimer, 0);

            if (m_fSpawnTimer == 0)
            {
                GameObject newEnemy = Instantiate(m_goEnemyPrefab, m_goSpawnLocation.transform.position, m_goSpawnLocation.transform.rotation);
                m_lEnemyList.Add(newEnemy);
                m_fSpawnTimer = m_fSpawnDelay;
                //Debug.Log("Enemy #" + m_lEnemyList.Count + " has spawned");
                m_iEnemyCount += 1;
            }
        }
    }
}
