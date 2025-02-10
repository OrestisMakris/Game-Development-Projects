using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] FOV FovBox;
    [SerializeField] private int health = 100;

    private Renderer objectRenderer;
    private Color originalColor;

    private Transform playerTransform; // Reference to the player's transform
    private float rotationSpeed = 360f; // Controls how fast the enemy rotates (degrees per second)

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color; // Capture the original color of the object

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
        this.transform.Rotate(-75, 0, 0);
        FovBox.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
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

        // Continue rotating until the angle difference is very small.
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            // RotateTowards gradually rotates the enemy's current rotation towards the target.
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            yield return null;
        }
        // Ensure the final rotation is set exactly.
        transform.rotation = targetRotation;
    }
}
