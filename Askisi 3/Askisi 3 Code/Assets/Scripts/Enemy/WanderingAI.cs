using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 1.0f;

    private bool isAlive;
    private bool isTooClose = false;

    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;

    //[SerializeField] LayerMask obstacleLayer;

    public bool IsTooClose // Property to access isTooClose
    {
        get { return isTooClose; } // Only provide a getter 
    }

    public void SetTooClose(bool value)
    {
        isTooClose = value; // Sets the value of isTooClose
    }

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            // Fix any x axis rotation
            Vector3 currentEuler = transform.rotation.eulerAngles;
            currentEuler.x = 0;
            transform.rotation = Quaternion.Euler(currentEuler);

            // Move the enemy forward
            if (!isTooClose) transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                // Check if the hit object is the player's shield.
                ShieldController shield = hitObject.GetComponent<ShieldController>();

                // Check if the hit object is the player.
                PointClickMovement player = hitObject.GetComponent<PointClickMovement>();

                if (shield != null || player != null)
                {
                    if (fireball == null)
                    {
                        fireball = Instantiate(fireballPrefab);
                        fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        fireball.transform.rotation = transform.rotation;
                    }
                }
                if (hit.distance < obstacleRange)
                {
                    // Rotate away from the obstacle
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }

    // Detect collisions with walls using triggers
    // void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("Triggered with: " + other.gameObject.name);
    //     // Check if the collided object is on the obstacleLayer
    //     if (((1 << other.gameObject.layer) & obstacleLayer) != 0)
    //     {
    //         // Rotate away from the wall by a random angle between 135 and 225 degrees
    //         float angle = Random.Range(135, 225);
    //         transform.Rotate(0, angle, 0);
    //     }
    // }
}
