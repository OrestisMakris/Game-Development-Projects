using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Vector3[] spawnPositions;
    //private int currentEnemyCount;
    private int enemiesKilledCount;
    private int totalEnemyCount;

    // Expose counts for UI updates
    public int TotalEnemyCount { get { return totalEnemyCount; } }
    //public int CurrentEnemyCount { get { return currentEnemyCount; } }
    public int EnemiesKilledCount { get { return enemiesKilledCount; } }

    private void Awake()
    {
        Startup();
    }

    public void Startup()
    {
        Debug.Log("Enemy manager starting...");
        status = ManagerStatus.Started;
    }

    // Spawn enemies for a specific level and track count
    public void SpawnEnemiesForLevel(int levelIndex)
    {
        totalEnemyCount = 0;
        enemiesKilledCount = 0;
        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            for (int i = 0; i < enemyPrefabs.Length; i++)
            {
                Vector3 spawnPos = (i < spawnPositions.Length) ? spawnPositions[i] : Vector3.zero;
                InstantiateEnemy(enemyPrefabs[i], spawnPos);
                totalEnemyCount++;
            }
        }
    }

    private void InstantiateEnemy(GameObject prefab, Vector3 spawnPos)
    {
        GameObject enemy = Instantiate(prefab);
        enemy.transform.position = spawnPos;
        float angle = Random.Range(0, 360);
        enemy.transform.Rotate(0, angle, 0);
        // Each enemy calls NotifyEnemyDefeated in the ReactiveTarget script when defeated
    }

    // Called by enemy scripts when they are defeated.
    public void NotifyEnemyDefeated()
    {
        enemiesKilledCount++;
        // Broadcast enemy count update with no parameters.
        Messenger.Broadcast(GameEvent.ENEMY_COUNT_UPDATED);
        if (enemiesKilledCount >= totalEnemyCount)
        {
            Debug.Log("All enemies defeated.");
            Messenger.Broadcast(GameEvent.ENEMIES_DEFEATED);
        }
    }

}
