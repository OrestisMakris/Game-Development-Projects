using UnityEngine;
using UnityEngine.SceneManagement; // For reloading the scene

public class Spike : MonoBehaviour
{
    public PlayerStats playerStats;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlatformerPlayer>() != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
            playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
            playerStats.AddFailure();
        }
    }
}
