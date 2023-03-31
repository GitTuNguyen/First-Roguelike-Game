using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public EnemySpawner enemySpawner;
    public FireballController fireballController;
    public PlayerController playerController;
    public bool isGameOver;
    public float timer;
    public int enemyKilled;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        isGameOver = false;
    }
    private void Start()
    {
        enemyKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.timeSinceLevelLoad;
    }

    public void GameOver()
    {
        isGameOver = true;
        enemySpawner.DestroyAllEnemy();
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void RemuseGame()
    {
        Time.timeScale = 1;
    }
    
    public void UpdateEnemyKilled()
    {
        enemyKilled++;
    }
}
