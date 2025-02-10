using System.Collections;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public WanderingAI enemyAI;  // Reference to the WanderingAI script
    private Transform playerTransform;

    private float maxDistance = 5.0f;
    private float rotationSpeed = 90f; // Controls how fast the enemy rotates (degrees per second)

    public MeshRenderer fovMeshRenderer; // Reference to the mesh renderer

    void Start()
    {
        fovMeshRenderer.enabled = false; // Hide the mesh on start

        // Find the player object in the scene by locating the PlayerCharacter component
        PointClickMovement player = FindObjectOfType<PointClickMovement>();
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        PointClickMovement player = other.GetComponent<PointClickMovement>();
        ShieldController shield = other.GetComponent<ShieldController>();
        if (player && playerTransform != null || shield && playerTransform != null)
        {
            if (HasLineOfSight())
            {
                Debug.Log("Player entered FOV and is in line of sight");
                fovMeshRenderer.enabled = true; // Show the mesh when the player enters the FOV
                RotateTowardsPlayer();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        PointClickMovement player = other.GetComponent<PointClickMovement>();
        ShieldController shield = other.GetComponent<ShieldController>();
        if (player && playerTransform != null || shield && playerTransform != null)
        {
            if (HasLineOfSight())
            {
                fovMeshRenderer.enabled = true; // Show the mesh when the player is in the FOV
                RotateTowardsPlayer();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        PointClickMovement player = other.GetComponent<PointClickMovement>();
        ShieldController shield = other.GetComponent<ShieldController>();
        if (player || shield)
        {
            Debug.Log("Player exited FOV");
            fovMeshRenderer.enabled = false; // Hide the mesh when the player exits the FOV
            enemyAI.SetTooClose(false);
        }
    }

    bool HasLineOfSight()
    {
        Vector3 directionToPlayer = playerTransform.position - enemyAI.transform.position;
        Ray ray = new Ray(enemyAI.transform.position, directionToPlayer);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object is the player's shield.
            ShieldController shield = hit.collider.GetComponent<ShieldController>();

            // Check if the hit object is the player.
            PointClickMovement player = hit.collider.GetComponent<PointClickMovement>();

            if (shield != null || player != null)
            {
                if (hit.distance < maxDistance)
                {
                    enemyAI.SetTooClose(true);
                }
                else
                {
                    enemyAI.SetTooClose(false);
                }
                return true; // No obstacles between enemy and player
            }
        }
        return false; // Obstacle detected
    }

    void RotateTowardsPlayer()
    {
        // Calculate the direction to the player and instantly rotate to face the player
        Vector3 direction = playerTransform.position - enemyAI.transform.position;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate from the current rotation towards the target rotation.
        enemyAI.transform.rotation = Quaternion.RotateTowards(
            enemyAI.transform.rotation,
            lookRotation,
            rotationSpeed * Time.deltaTime
        );
    }

}