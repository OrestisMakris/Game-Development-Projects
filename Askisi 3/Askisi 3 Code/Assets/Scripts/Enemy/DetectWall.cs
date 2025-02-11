using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWall : MonoBehaviour
{
    [SerializeField] LayerMask obstacleLayer;

    // Detect collisions with walls using triggers
    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is on the obstacleLayer
        if (((1 << other.gameObject.layer) & obstacleLayer) != 0)
        {
            WanderingAI enemy = transform.parent.GetComponent<WanderingAI>();

            // Get the closest point on the wall to the enemy.
            Vector3 closestPoint = other.ClosestPoint(transform.position);

            // Compute the direction from the wall to the enemy.
            Vector3 awayDirection = enemy.transform.position - closestPoint;
            awayDirection.y = 0; // Ignore vertical differences

            // Calculate the target rotation so that the enemy faces away from the wall.
            Quaternion targetRotation = Quaternion.LookRotation(awayDirection);

            // Apply the new rotation.
            enemy.transform.rotation = targetRotation;
        }
    }
}
