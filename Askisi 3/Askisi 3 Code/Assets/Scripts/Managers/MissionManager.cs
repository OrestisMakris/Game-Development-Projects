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
        Debug.Log("Mission manager starting...");
        curLevel = 0;
        maxLevel = 4;
        status = ManagerStatus.Started;
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
        // Broadcast level complete as usual
        Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);

        if (curLevel < maxLevel)
        {
            // Proceed to next level in UIController (or MissionManager) using the normal flow.
            Debug.Log("Objective reached. Proceeding to next level...");
        }
        else
        {
            // Final level reached: show win message and restart the game after a delay.
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