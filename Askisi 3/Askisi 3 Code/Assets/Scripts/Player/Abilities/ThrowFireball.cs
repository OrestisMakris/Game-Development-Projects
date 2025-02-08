using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFireball : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;
    [SerializeField] float rotationSpeed = 10f;
    private Quaternion targetRotation;
    private bool isRotating = false;


    private PointClickMovement movementScript; // Reference to the movement script

    void Start()
    {
        // Get the movement script component
        movementScript = GetComponent<PointClickMovement>();
    }

    void Update()
    {
        // Check if the right mouse button was pressed.
        if (Input.GetMouseButtonDown(1))
        {
            // Create a ray from the camera through the mouse position.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Vector3 targetPoint;
            // If the raycast hits something in the scene...
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                // Otherwise, pick a point far away in the ray's direction.
                targetPoint = ray.origin + ray.direction * 100f;
            }

            // Calculate the horizontal direction from the player to the target.
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0;  // Ignore vertical difference 

            // Only rotate if there is a valid direction.
            if (direction.sqrMagnitude > 0.001f)
            {
                // Check if the player is moving and the target is behind
                bool isMoving = movementScript.IsMoving();
                float dot = Vector3.Dot(transform.forward, direction.normalized);

                if (isMoving && dot < 0)
                {
                    // Throw immediately if moving and target is behind
                    InstantiateFireball(Quaternion.LookRotation(direction), spawnBehind: true);
                }
                else
                {
                    // Otherwise, start smooth rotation
                    targetRotation = Quaternion.LookRotation(direction);
                    isRotating = true;
                }
            }
        }

        // Smoothly rotate the player towards the target rotation.
        if (isRotating)
        {
            // Interpolate between the current rotation and the target rotation.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check if the rotation is close enough to the target rotation.
            if (Quaternion.Angle(transform.rotation, targetRotation) < 10.0f)
            {
                isRotating = false;  // Stop rotating.

                InstantiateFireball(transform.rotation, spawnBehind: false);  // Throw the fireball.
            }
        }
    }

    private void InstantiateFireball(Quaternion rotation, bool spawnBehind)
    {
        // Calculate spawn position based on the fireball's rotation
        Vector3 spawnPosition;
        if (spawnBehind)
        {
            // Spawn behind the player in the direction the fireball is facing
            spawnPosition = transform.position + rotation * Vector3.forward;
        }
        else
        {
            // Spawn in front of the player (local forward)
            spawnPosition = transform.TransformPoint(Vector3.forward);
        }

        fireball = Instantiate(fireballPrefab);
        fireball.transform.position = spawnPosition;
        fireball.transform.rotation = rotation;
    }
}