using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 5;
    private UseShield playerShield;

    void Start()
    {
        playerShield = FindObjectOfType<UseShield>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the fireball hit the player shield first
        if (playerShield.IsShieldActive())
        {
            Destroy(gameObject);
            return;
        }

        // If the shield is not active, check if the fireball hit the player
        PointClickMovement player = other.GetComponent<PointClickMovement>();
        if (player != null)
        {
            Managers.Player.ChangeHealth(-damage);
        }
        Destroy(this.gameObject);
    }
}
