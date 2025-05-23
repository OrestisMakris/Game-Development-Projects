using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] FOV FovBox;
    [SerializeField] private int health = 100;
    // Add audio clips for hit and death sounds
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;

    private Renderer objectRenderer;
    private Color originalColor;
    // Removed local AudioSource since AudioManager is used now.
    // private AudioSource audioSource;

    private Transform playerTransform; // Reference to the player's transform
    //private float rotationSpeed = 360f; // Controls how fast the enemy rotates (degrees per second)

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color; // Capture the original color of the object
        // No longer needed: audioSource = GetComponent<AudioSource>();

        // Find the player object in the scene by locating the PlayerCharacter component
        PointClickMovement player = FindObjectOfType<PointClickMovement>();
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    public void ReactToHit(int damage)
    {
        health -= damage;

        // Play hit sound using AudioManager
        if (hitSound != null)
        {
            AudioManager.Instance.PlaySound(hitSound, 0.4f);
        }

        // Rotate towards the player
        if (playerTransform != null)
        {
            StartCoroutine(RotateTowardsPlayer(playerTransform));
        }

        StartCoroutine(GetHurt());
        if (health < 1)
        {
            WanderingAI behavior = GetComponent<WanderingAI>();
            if (behavior != null)
            {
                behavior.SetAlive(false);
            }
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        // Play death sound using AudioManager
        if (deathSound != null)
        {
            AudioManager.Instance.PlaySound(deathSound, 0.1f);
        }
        this.transform.Rotate(-75, 0, 0);
        FovBox.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        // Notify the local EnemyManager that this enemy is defeated.
        EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.NotifyEnemyDefeated();
        }
        Destroy(this.gameObject);
    }

    private IEnumerator GetHurt()
    {
        objectRenderer.material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        objectRenderer.material.color = originalColor;
    }

    private IEnumerator RotateTowardsPlayer(Transform target)
    {
        // Determine the target rotation so that the enemy faces the player.
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = targetRotation;
        yield return null;
    }
}