using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For reloading the scene

public class WanderingEnemy : MonoBehaviour
{
    public PlayerStats playerStats;
    public float speed = 2.0f;
    public float wanderDist = 1.0f;
    private Rigidbody2D body;
    private Vector3 startPos;
    private bool movingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        float direction = movingRight ? 1 : -1;

        // Move the enemy
        body.velocity = new Vector2(direction * speed, body.velocity.y);

        // Check if the enemy has reached the movement boundary
        if (movingRight && transform.position.x >= startPos.x + wanderDist)
        {
            movingRight = false; // Change direction to left
        }
        else if (!movingRight && transform.position.x <= startPos.x - wanderDist)
        {
            movingRight = true; // Change direction to right
        }

        // Flip the enemy's sprite to face the direction of movement
        transform.localScale = new Vector3(direction, 1, 1);   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            playerStats.AddFailure();
        }
    }

    public void SetDifficulty(float multiplier)
    {
        speed = speed * multiplier;
    }
}
