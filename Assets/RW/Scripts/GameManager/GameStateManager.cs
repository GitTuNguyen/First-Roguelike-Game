using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public EnemySpawner enemySpawner;
    public FireballController fireballController;
    public PlayerController playerController;
    public GameObject gameOverUI;
    public bool isGameOver;
    public int enemyKilled;
    public float timer;
    private float timePlayed;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        isGameOver = false;
    }
    private void Start()
    {
        enemyKilled = 0;
        timePlayed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.timeSinceLevelLoad - timePlayed;
    }

    public void GameOver()
    {
        enemySpawner.DestroyAllEnemy();
        isGameOver = true;
        StopGame();
        gameOverUI.SetActive(true);
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ResetGame()
    {
        ResumeGame();
        isGameOver = false;
        Player player = FindObjectOfType<Player>();
        enemyKilled = 0;
        timePlayed += timer;
        player.ResetGame();
    }

    public void UpdateEnemyKilled()
    {
        enemyKilled++;
    }
}
