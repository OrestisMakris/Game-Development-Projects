using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 5;
    private UseShield playerShield;

    [SerializeField] private AudioClip enemyShootSound; // Sound played when the enemy shoots a fireball
    [SerializeField] private AudioClip playerHitSound;    // Sound played when the player is hit

    void Start()
    {
        playerShield = FindObjectOfType<UseShield>();

        // Play enemy shoot sound when the fireball is spawned
        if (enemyShootSound != null)
        {
            AudioManager.Instance.PlaySound(enemyShootSound, 0.02f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the fireball hit the player's shield first
        if (playerShield.IsShieldUsed())
        {
            Destroy(gameObject);
            return;
        }

        // If the shield is not active, check if the fireball hit the player
        PointClickMovement player = other.GetComponent<PointClickMovement>();
        if (player != null)
        {
            // Play player hit sound
            if (playerHitSound != null)
            {
                AudioManager.Instance.PlaySound(playerHitSound, 0.5f);
            }
            
            Managers.Player.ChangeHealth(-damage);
        }
        Destroy(gameObject);
    }
}