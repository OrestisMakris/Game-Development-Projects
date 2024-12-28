using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player falls into the detector
        PlatformerPlayer player = collision.GetComponent<PlatformerPlayer>();
        if (player != null)
        {
            Debug.Log("Player fell off the platform! Game Over.");
            // Add logic to handle game over, like restarting the level
            GameOver();
        }
    }

    void GameOver()
    {
        // Example: Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
