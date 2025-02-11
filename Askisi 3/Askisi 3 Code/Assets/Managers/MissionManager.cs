using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public int curLevel { get; private set; }
    public int maxLevel { get; private set; }

    public void Startup()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        Debug.Log("Mission manager starting...");
        curLevel = 0;
        maxLevel = 2;
        status = ManagerStatus.Started;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // When the scene is loaded, spawn enemies for the current level
        Managers.Enemies.SpawnEnemiesForLevel(curLevel);
    }

    private void OnDestroy()
    {
        // When the MissionManager is destroyed, unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
        Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
    }
}