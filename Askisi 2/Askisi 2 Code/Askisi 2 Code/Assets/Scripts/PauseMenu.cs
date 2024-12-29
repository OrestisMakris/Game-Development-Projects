 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 using UnityEngine.UI;
 using TMPro; 

 public class PauseMenu : MonoBehaviour
 {
     public GameObject pauseMenuUI;
     public TMP_Dropdown difficultyDropdown;
     public PlayerStats playerStats;
     
     private bool isPaused = false;
     private float speedMultiplier = 1.0f; // Default Medium Difficulty Speed Multiplier

void Start()
{
    difficultyDropdown.value = 1; // Medium (Index 1)
    difficultyDropdown.onValueChanged.AddListener(SetDifficulty);

    // Trigger the SetDifficulty method to ensure it is applied
    SetDifficulty(difficultyDropdown.value);
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
                speedMultiplier = 0.55f;
                break;
            case 1: // Medium
                speedMultiplier = 1f;
                break;
            case 2: // Hard
                speedMultiplier = 2f;
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
 }
