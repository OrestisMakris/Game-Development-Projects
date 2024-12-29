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

void Start()
{
    // Load the saved difficulty from PlayerPrefs, 
    // default to Medium (index 1) if not set
    int savedDifficulty = PlayerPrefs.GetInt("GameDifficulty", 1); 
    difficultyDropdown.value = savedDifficulty;
    difficultyDropdown.onValueChanged.AddListener(SetDifficulty);

    // Apply the loaded or default difficulty
    SetDifficulty(savedDifficulty);
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

         // Reset difficulty to Medium (index 1) and save it
        difficultyDropdown.value = 1;
        PlayerPrefs.SetInt("GameDifficulty", 1);
        PlayerPrefs.Save();
     }

     public void SetDifficulty(int index)
     {
        // Save the selected difficulty to PlayerPrefs
        PlayerPrefs.SetInt("GameDifficulty", index);
        PlayerPrefs.Save();

        // Update WanderingEnemy instances
        WanderingEnemy[] wanderingEnemies = FindObjectsOfType<WanderingEnemy>();
        foreach (WanderingEnemy enemy in wanderingEnemies){
            enemy.SetDifficulty(index);
        }

        // Update FireballEnemy instances
        FireballEnemy[] fireballEnemies = FindObjectsOfType<FireballEnemy>();
        foreach (FireballEnemy enemy in fireballEnemies){
            enemy.SetDifficulty(index);
        }
     }
 }
