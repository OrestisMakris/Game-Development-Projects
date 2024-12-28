 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 using UnityEngine.UI;

 public class PauseMenu : MonoBehaviour
 {
     public GameObject pauseMenuUI;
     public Dropdown difficultyDropdown;
     public PlayerStats playerStats;
     
     private bool isPaused = false;
     private float speedMultiplier = 1.0f; // Default Medium Difficulty Speed Multiplier
     void Start()
     {
        // Set default difficulty
        difficultyDropdown.onValueChanged.AddListener(SetDifficulty);
     }

     void Update()
     {
         if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
         {
             if (isPaused)
             {
                 Resume();
             }
             else
             {
                 Pause();
             }
         }
     }
     
     public void Resume()
     {
         pauseMenuUI.SetActive(false);
         Time.timeScale = 1f; // Resume the game
         isPaused = false;
     }

     public void Pause()
     {
         pauseMenuUI.SetActive(true);
         Time.timeScale = 0f; // Pause the game
         isPaused = true;
     }

     public void Restart()
     {
         Time.timeScale = 1f; // Reset time scale
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
         playerStats.ResetFailures(); // Reset failures
     }

     public void SetDifficulty(int index)
     {
        switch (index)
        {
            case 0: // Easy
                speedMultiplier = 0.75f;
                break;
            case 1: // Medium
                speedMultiplier = 1f;
                break;
            case 2: // Hard
                speedMultiplier = 1.25f;
                break;
        }

        // Update WanderingEnemy instances
        WanderingEnemy[] wanderingEnemies = FindObjectsOfType<WanderingEnemy>();
        foreach (WanderingEnemy enemy in wanderingEnemies){
            enemy.SetDifficulty(speedMultiplier);
        }

        // Update FireballEnemy instances
        FireballEnemy[] fireballEnemies = FindObjectsOfType<FireballEnemy>();
        foreach (FireballEnemy enemy in fireballEnemies){
            enemy.SetDifficulty(speedMultiplier);
        }
     }

     public void QuitGame()
     {
         Application.Quit();
     }
 }
