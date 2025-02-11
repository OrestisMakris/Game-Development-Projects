using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PointClickMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] private float walkSpeed = 3.0f;  // Default walk speed
    [SerializeField] private float runSpeed = 8.0f;     // Increased run speed
    private float curSpeed = 0f;                        // Current speed

    public float rotSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -20.0f;
    public float minFall = -1.5f;
    public float deceleration = 25.0f;
    public float targetBuffer = 1.5f;

    [SerializeField] private float jumpForce = 16f;      // Upward force for jump

    private Vector3? targetPos;
    private float vertSpeed;
    private ControllerColliderHit contact;
    private CharacterController charController;
    private Animator animator;
    public float pushForce = 3.0f;

    // Use this for initialization
    void Start()
    {
        vertSpeed = minFall;
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // start with zero and add movement components progressively
        Vector3 movement = Vector3.zero;

        // Handle input to pick a target position
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            if (hits.Length > 0)
            {
                // Sort hits by distance and ignore the player's shield
                System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

                foreach (RaycastHit hitInfo in hits)
                {
                    if (!hitInfo.collider.CompareTag("PlayerShield"))
                    {
                        targetPos = hitInfo.point;
                        // Check if Shift is held down at click for running
                        curSpeed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? runSpeed : walkSpeed;
                        break;
                    }
                }
            }
        }

        // Rotate smoothly toward the target if one exists
        if (targetPos != null)
        {
            if (curSpeed > (walkSpeed * 0.5f))
            {
                Vector3 adjustedPos = new Vector3(targetPos.Value.x, transform.position.y, targetPos.Value.z);
                Quaternion targetRot = Quaternion.LookRotation(adjustedPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            }

            movement = curSpeed * Vector3.forward;
            movement = transform.TransformDirection(movement);

            // Deceleration when approaching the target
            if (Vector3.Distance(targetPos.Value, transform.position) < targetBuffer)
            {
                curSpeed -= deceleration * Time.deltaTime;
                if (curSpeed <= 0)
                {
                    targetPos = null;
                }
            }
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Determine if we are on the ground using a raycast
        bool hitGround = false;
        RaycastHit hit;
        if (vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (charController.height + charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        // Handle Jumping:
        if (hitGround)
        {
            // When on the ground, allow jump on Space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vertSpeed = jumpForce; // Apply jump force
                animator.SetBool("Jumping", true);
            }
            else
            {
                // If not jumping, ensure the character stays grounded
                vertSpeed = minFall;
                animator.SetBool("Jumping", false);
            }
        }
        else
        {
            // When in air, apply gravity
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }
            animator.SetBool("Jumping", true);

            // Adjust movement when standing on drop-off edge.
            if (charController.isGrounded && contact != null)
            {
                if (Vector3.Dot(movement, contact.normal) < 0)
                {
                    movement = contact.normal * walkSpeed;
                }
                else
                {
                    movement += contact.normal * walkSpeed;
                }
            }
        }

        movement.y = vertSpeed;
        movement *= Time.deltaTime;
        charController.Move(movement);
    }

    // Store collision information for use in Update.
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }

    // Public property to check if the player is moving.
    public bool IsMoving()
    {
        return targetPos != null && curSpeed > 0.1f;
    }
}