using UnityEngine;
using UnityEngine.SceneManagement; // For reloading the scene

public class Spike : MonoBehaviour
{
    public PlayerStats playerStats;
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"Object entered trigger: {collision.name}"); // Log every object
        if (collision.GetComponent<PlatformerPlayer>() != null)
        {
            //Debug.Log("Player hit a spike! Game Over.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
            playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
            playerStats.AddFailure();
        }
    }
}
