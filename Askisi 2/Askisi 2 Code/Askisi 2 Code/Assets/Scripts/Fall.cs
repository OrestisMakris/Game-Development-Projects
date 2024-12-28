using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FallDetector : MonoBehaviour
{
    public PlayerStats playerStats;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player falls into the detector
        PlatformerPlayer player = collision.GetComponent<PlatformerPlayer>();
        if (player != null)
        {
            // Debug.Log("Player fell off the platform! Game Over.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
            playerStats.AddFailure();
        }
    }
}
