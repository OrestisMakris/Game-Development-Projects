using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotSpeed = 100.0f; // degrees per second
    public float obstacleRange = 2.0f;
    private bool isAlive;

    // Get the player character component
    private PlayerCharacter playerCharacter;
    private float angleToPlayer;
    private float distanceToPlayer;
    public float aiFieldOfViewAngle = 60.0f;
    public float aiSightRange = 20.0f;  

    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;


    // Start is called before the first frame update
    void Start(){
        isAlive = true;

        // Get the player character component
        playerCharacter = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
    }

    // Update is called once per frame
    void Update(){
        // Always move forward
        if (isAlive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        // Get the position of the player character
        Vector3 playerPos = playerCharacter.transform.position;

        // Calculate the angle between the AI and the player 
        Vector3 playerDirection = playerPos - transform.position;
        angleToPlayer = Vector3.SignedAngle(playerDirection, transform.forward, Vector3.up);

        // Get the magnitude of playerDirection
        distanceToPlayer = playerDirection.magnitude;

        // Create a Sphere Cast
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 1.0f, out hit)){
            // If the player is hit by the sphere cast shoot fireball
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (fireball == null)
                {
                    fireball = Instantiate(fireballPrefab);
                    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    fireball.transform.rotation = transform.rotation;
                }
            }
            // If the enemy hits a wall then rotate randomly
            if (hit.distance < obstacleRange)
            {
                if(hit.collider.tag != "Fireball"){
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }

        if(Mathf.Abs(angleToPlayer) <= aiFieldOfViewAngle/2 && distanceToPlayer <= aiSightRange){
            MoveToPlayer();   
        }
    }

    public void SetAlive(bool alive){
        isAlive = alive;
    }

     public void MoveToPlayer()
    {
        // Rotate Enemy to match player's direction and move forward
        float rotStep = rotSpeed * Time.deltaTime;

        if(isAlive){
            // Rotate towards the player by a step determined by rotSpeed
            float rotAmount = Mathf.Sign(angleToPlayer) * rotStep;
            if(Mathf.Abs(angleToPlayer) > Mathf.Abs(rotAmount)){
                transform.Rotate(0, -rotAmount, 0);
            }

            transform.Translate(0, 0, speed * Time.deltaTime);
            
        }
    }
}
