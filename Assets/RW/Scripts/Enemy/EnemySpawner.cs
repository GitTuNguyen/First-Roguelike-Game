using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float maxNumberOfEnemy;
    public List<GameObject> normalEnemyPrefabs = new List<GameObject>();
    public List<GameObject> bossPrefabs = new List<GameObject>();

    public float timeBetweenSpawns;
    public float radiusSpawnerCircle;
    [HideInInspector]
    public List<GameObject> enemyList = new List<GameObject>();
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnEnemy()
    {
        if (enemyList.Count < maxNumberOfEnemy)
        {
            if (player == null)
            {
                player = FindObjectOfType<Player>();
            }
            Vector2 playerPosition = player.transform.position;
            Vector2 spawnPosition = new Vector2(playerPosition.x, playerPosition.y) + Random.insideUnitCircle.normalized * radiusSpawnerCircle + new Vector2(Random.Range(0, 5), Random.Range(0, 5));
            GameObject enemySpawner = normalEnemyPrefabs[Random.Range(0, normalEnemyPrefabs.Count)];
            GameObject enemy = Instantiate(enemySpawner, spawnPosition, enemySpawner.transform.rotation);
            enemy.AddComponent<Enemy>();
            enemyList.Add(enemy);
            enemy.GetComponent<Enemy>().SetSpawner(this);
        }        
    }
    
    private IEnumerator SpawnRoutine()
    {
        while (!GameStateManager.Instance.isGameOver)
        {            
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void RemoveEnemyFromList(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }

    public void DestroyAllEnemy()
    {
        foreach (GameObject enemy in enemyList)
        {
            Destroy(enemy);
        }

        enemyList.Clear();
    }
}
