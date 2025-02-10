using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject normalEnemyPrefab;
    [SerializeField] GameObject heavyEnemyPrefab;
    [SerializeField] GameObject lightEnemyPrefab;

    void Start()
    {
        // Spawn enemies at specific locations
        SpawnEnemy(normalEnemyPrefab, new Vector3(-20, 1, 0));
        SpawnEnemy(heavyEnemyPrefab, new Vector3(-10, 1, -21));
        SpawnEnemy(lightEnemyPrefab, new Vector3(18, 0.5f, -8));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnEnemy(GameObject enemyPrefab, Vector3 position)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = position;
        float angle = Random.Range(0, 360);
        enemy.transform.Rotate(0, angle, 0);

    }
}
