using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 15.0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerBody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerBody != null)
            {
                playerBody.velocity = new Vector2(playerBody.velocity.x, bounceForce);
            }
        }
    }
}

