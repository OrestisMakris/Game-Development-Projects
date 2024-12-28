using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For reloading the scene

public class Fireball : MonoBehaviour
{
    public PlayerStats playerStats;
    public float fireballLifeTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Destroy the fireball after 5 seconds if it still exists
        Destroy(gameObject, fireballLifeTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            // Reload the scene if it hits the player
            //Debug.Log("Fireball hit the player!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            playerStats= GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
            playerStats.AddFailure();
        }
        // Ignore collisions with the enemy that fired it
        else if (!collision.CompareTag("Enemy")) 
        {
            // Destroy the fireball on any other surface
            Destroy(gameObject);
        }
    }
}
