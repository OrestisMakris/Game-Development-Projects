using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Vector3[] spawnPositions;

    public void Startup()
    {
        Debug.Log("Enemy manager starting...");
        status = ManagerStatus.Started;
    }

    // Spawn enemies for a specific level
    public void SpawnEnemiesForLevel(int levelIndex)
    {
        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            for (int i = 0; i < enemyPrefabs.Length; i++)
            {
                Vector3 spawnPos = (i < spawnPositions.Length) ? spawnPositions[i] : Vector3.zero;
                InstantiateEnemy(enemyPrefabs[i], spawnPos);
            }
        }
    }

    private void InstantiateEnemy(GameObject prefab, Vector3 spawnPos)
    {
        GameObject enemy = Instantiate(prefab);
        enemy.transform.position = spawnPos;
        float angle = Random.Range(0, 360);
        enemy.transform.Rotate(0, angle, 0);
    }

}
