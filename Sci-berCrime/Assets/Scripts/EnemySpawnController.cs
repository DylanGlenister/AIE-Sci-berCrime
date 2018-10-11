using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public bool spawningEnabled = true;

    public int enemyMax = 200;
    public int enemyCount;

    public float spawnDelay = 0.001f;
    public float spawnTimer;

    private List<GameObject> m_list = new List<GameObject>();

    // Reference to the enemy prefab
    public GameObject enemyPrefab;

    // Gameobjects at the positions they spawn at
    public GameObject spawnLocation;

    void Start()
    {
        spawnTimer = spawnDelay;
    }

    void Update()
    {
        if (spawningEnabled)
        {
            if (m_list.Count >= enemyMax)
                return;

            spawnTimer -= Time.deltaTime;
            spawnTimer = Mathf.Max(spawnTimer, 0);

            if (spawnTimer == 0)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
                m_list.Add(newEnemy);
                spawnTimer = spawnDelay;
                Debug.Log("Enemy #" + m_list.Count + " has spawned");
                enemyCount += 1;
            }
        }
    }
}
