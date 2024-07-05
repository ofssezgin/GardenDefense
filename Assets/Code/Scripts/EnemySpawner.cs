using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner main;

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] public GameObject youWinUI;
    [SerializeField] public GameObject youLostUI;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalinFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;
    [SerializeField] public int currentWave = 1;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; // enemies per second
    private bool isSpawning = false;

    private void Awake() {
        main = this;
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start() {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning) return;

        if(currentWave == 11) 
        {
            gameOverWin();
        }

        if(LevelManager.main.baseHealth == 0)
        {
            gameOverLose();
            
        }
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f/ eps) && enemiesLeftToSpawn > 0) {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0) {
            EndWave();
        }

    }
    public void gameOverLose()
    {
        youLostUI.SetActive(true);
        timeSinceLastSpawn = 0;
    }
    public void gameOverWin()
    {
        youWinUI.SetActive(true);
        timeSinceLastSpawn = 0;
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {   
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    private void EndWave() {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy() {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalinFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalinFactor), 0f, enemiesPerSecondCap);
    }


}
