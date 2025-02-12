using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public int curLevel { get; private set; }
    public int maxLevel { get; private set; }
    public bool enemiesCleared = false;

    public void Startup()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        Debug.Log("Mission manager starting...");
        curLevel = 0;
        maxLevel = 4;
        status = ManagerStatus.Started;
    }

    // When the scene is loaded, spawn enemies for the current level
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset enemy clearance before spawning new enemies
        enemiesCleared = false;
        // Find EnemyManager placed in the current level scene.
        EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.SpawnEnemiesForLevel(curLevel);
        }
    }

    private void OnDestroy()
    {
        // When the MissionManager is destroyed, unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnEnable()
    {
        // When the MissionManager is enabled, listen for the ENEMIES_DEFEATED event
        Messenger.AddListener(GameEvent.ENEMIES_DEFEATED, OnEnemiesDefeated);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ENEMIES_DEFEATED, OnEnemiesDefeated);
    }

    private void OnEnemiesDefeated()
    {
        // When the ENEMIES_DEFEATED event is received, set enemiesCleared to true
        enemiesCleared = true;
        Debug.Log("All enemies in current level are defeated.");
    }

    public void GoToNext()
    {
        if (curLevel < maxLevel)
        {
            curLevel++;
            string name = $"Level{curLevel}";
            Debug.Log($"Loading {name}");
            SceneManager.LoadScene(name);
        }
        else
        {
            Debug.Log("Last level");
        }
    }

    public void RestartCurrent()
    {
        string name = $"Level{curLevel}";
        Debug.Log($"Loading {name}");
        SceneManager.LoadScene(name);
    }

    public void ReachObjective()
    {
        // Only complete the level if all enemies are defeated
        if (!enemiesCleared)
        {
            Debug.Log("Cannot complete level. Enemies are still present.");
            return;
        }
        // Broadcast level complete as usual
        Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);

        if (curLevel < maxLevel)
        {
            Debug.Log("Objective reached. Proceeding to next level...");
        }
        else
        {
            Debug.Log("You Won!");
            StartCoroutine(RestartGame());
        }
    }

    private IEnumerator RestartGame()
    {
        // Wait for 2 seconds to allow the win message to show
        yield return new WaitForSeconds(2f);
        // Restart the game by loading your designated restart scene (e.g. Level0 or MainMenu).
        SceneManager.LoadScene("Startup"); // Adjust the scene name as needed.
    }
}