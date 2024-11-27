using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float rotSpeed = 100.0f; // degrees per second
    public float obstacleRange = 5.0f;
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
        DrawFieldOfView();

        // Get the position of the player character
        Vector3 playerPos = playerCharacter.transform.position;

        // Calculate the angle between the AI and the player 
        Vector3 playerDirection = playerPos - transform.position;
        angleToPlayer = Vector3.SignedAngle(playerDirection, transform.forward, Vector3.up);
        Debug.Log("angle to player = " + angleToPlayer);

        // Get the magnitude of playerDirection
        distanceToPlayer = playerDirection.magnitude;
        Debug.Log("distance to player = " + distanceToPlayer);

        if(Mathf.Abs(angleToPlayer) <= aiFieldOfViewAngle/2 && distanceToPlayer <= aiSightRange){
            MoveToPlayer();
        } else {
            Wander();
        }        
    }

    public void SetAlive(bool alive){
        isAlive = alive;
    }

    public void Wander(){
        if (isAlive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
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
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

     public void MoveToPlayer()
    {
        Debug.Log("Moving to player");
        // Rotate AI to match player's direction and move forward
        float rotStep = rotSpeed * Time.deltaTime;

        if(isAlive){
            // Rotate towards the player by a step determined by rotSpeed
            float rotAmount = Mathf.Sign(angleToPlayer) * rotStep;
            if(Mathf.Abs(angleToPlayer) > Mathf.Abs(rotAmount)){
                transform.Rotate(0, -rotAmount, 0);
            }

            transform.Translate(0, 0, speed * Time.deltaTime);

            // Otan ftasei konta thelw na purovolisei
            /*if(distanceToPlayer <= obstacleRange){
                Wander();
            }*/
            
        }
    }

    void DrawFieldOfView()
    {
        // Calculate the left and right boundaries of the field of view
        Vector3 leftBoundary = Quaternion.Euler(0, -aiFieldOfViewAngle / 2, 0) * transform.forward * aiSightRange;
        Vector3 rightBoundary = Quaternion.Euler(0, aiFieldOfViewAngle / 2, 0) * transform.forward * aiSightRange;

        // Draw lines to represent the boundaries of the field of view
        Debug.DrawLine(transform.position, transform.position + leftBoundary, Color.red);
        Debug.DrawLine(transform.position, transform.position + rightBoundary, Color.red);
        Debug.DrawLine(transform.position, transform.position + transform.forward * aiSightRange, Color.red);
    }
}
