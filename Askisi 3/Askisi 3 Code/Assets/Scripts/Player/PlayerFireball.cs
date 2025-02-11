using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    public float speed = 15.0f;
    public int damage = 5;
    [SerializeField] private AudioClip shootSound; // Fireball shoot sound

    // Start is called before the first frame update
    void Start()
    {
        // Play sound when the fireball is spawned via AudioManager
        if(shootSound != null)
        {
            AudioManager.Instance.PlaySound(shootSound, 0.06f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ShieldController>() != null)
        {
            return;
        }
        ReactiveTarget enemy = other.GetComponent<ReactiveTarget>();
        if (enemy != null)
        {
            enemy.ReactToHit(damage);
        }
        Destroy(this.gameObject);
    }
}