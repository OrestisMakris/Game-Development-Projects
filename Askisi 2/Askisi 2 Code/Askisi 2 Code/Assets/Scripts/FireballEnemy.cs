using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballEnemy : MonoBehaviour
{
    public float fireballRange = 6.0f;
    public float fireballSpeed = 4.0f;

    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;
    private PlatformerPlayer player;
    private Animator anim;
    private Vector3 playerDir;

    // Start is called before the first frame update
    void Start()
    {
        // Get the player component
        player = GameObject.FindWithTag("Player").GetComponent<PlatformerPlayer>();
        
        // Get the animator component
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the position of the player character
        Vector3 playerPos = player.transform.position;

        // Get the x component of the distance between the player and the enemy
        float distanceToPlayer = playerPos.x - transform.position.x;

        // Get the player's direction
        playerDir = (playerPos - transform.position).normalized;

        if(distanceToPlayer < fireballRange && fireball == null)
        {
            // Transition to the fireball throughing animation
            anim.SetBool("fireball_instantiated", true);
        }

        // Flip the enemy to face the player
        transform.localScale = new Vector3(Mathf.Sign(playerDir.x), 1, 1);
        
    }

    public void ThrowFireball()
    {
        // Instantiate a fireball at the position of the enemy and fire it in the player's direction
        fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        fireball.GetComponent<Rigidbody2D>().velocity = playerDir * fireballSpeed; 

        // Flip the fireball to face the player
        fireball.transform.localScale = new Vector3(Mathf.Sign(playerDir.x), 1, 1);

         anim.SetBool("fireball_instantiated", false);
    }

    public void SetDifficulty(float multiplier)
    {
        fireballSpeed = fireballSpeed * multiplier;
    }
}
