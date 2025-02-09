using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    public float speed = 15.0f;
    public int damage = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has a ReactiveTarget component (enemy)
        ReactiveTarget enemy = other.GetComponent<ReactiveTarget>();
        if (enemy != null)
        {
            // Damage the enemy
            enemy.ReactToHit(damage);
        }
        Destroy(this.gameObject);
    }
}

