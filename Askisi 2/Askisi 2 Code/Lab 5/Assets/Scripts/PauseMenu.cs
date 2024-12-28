// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class PauseMenu : MonoBehaviour
// {
//     public GameObject pauseMenuUI;
//     public Dropdown difficultyDropdown;
//     private bool isPaused = false;

//     void Start()
//     {
//         // Set default difficulty
//         difficultyDropdown.onValueChanged.AddListener(SetDifficulty);
//         SetDifficulty(1); // Default to Medium
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
//         {
//             if (isPaused)
//             {
//                 Resume();
//             }
//             else
//             {
//                 Pause();
//             }
//         }
//     }

//     public void Resume()
//     {
//         pauseMenuUI.SetActive(false);
//         Time.timeScale = 1f; // Resume the game
//         isPaused = false;
//     }

//     public void Pause()
//     {
//         pauseMenuUI.SetActive(true);
//         Time.timeScale = 0f; // Pause the game
//         isPaused = true;
//     }

//     public void Restart()
//     {
//         Time.timeScale = 1f; // Reset time scale
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//         PlayerStats.failures = 0; // Reset failures
//     }

//     public void SetDifficulty(int index)
//     {
//         switch (index)
//         {
//             case 0: // Easy
//                 Enemy.speedMultiplier = 0.75f;
//                 break;
//             case 1: // Medium
//                 Enemy.speedMultiplier = 1f;
//                 break;
//             case 2: // Hard
//                 Enemy.speedMultiplier = 1.25f;
//                 break;
//         }
//     }

//     public void QuitGame()
//     {
//         Application.Quit();
//     }
// }
