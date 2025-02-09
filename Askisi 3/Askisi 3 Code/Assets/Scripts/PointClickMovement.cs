using System.Collections;
using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PointClickMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float moveSpeed = 6.0f;
    public float rotSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -20.0f;
    public float minFall = -1.5f;

    public float deceleration = 25.0f;
    public float targetBuffer = 1.5f;

    private float curSpeed = 0f;
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

        // Use RaycastAll to ignore the shield collider and find the floor target.
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            if (hits.Length > 0)
            {
                // Sort the hits by distance.
                System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

                // Pick the first hit that is not the player's shield.
                foreach (RaycastHit hitInfo in hits)
                {
                    if (!hitInfo.collider.CompareTag("PlayerShield"))
                    {
                        targetPos = hitInfo.point;
                        curSpeed = moveSpeed;
                        break;
                    }
                }
            }
        }

        if (targetPos != null)
        {
            if (curSpeed > (moveSpeed * 0.5f))
            {
                Vector3 adjustedPos = new Vector3(targetPos.Value.x, transform.position.y, targetPos.Value.z);
                Quaternion targetRot = Quaternion.LookRotation(adjustedPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            }

            movement = curSpeed * Vector3.forward;
            movement = transform.TransformDirection(movement);

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

        // Raycast down to address steep slopes and drop-off edges.
        bool hitGround = false;
        RaycastHit hit;
        if (vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (charController.height + charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        // y movement: possibly jump impulse up, always accelerate down
        if (hitGround)
        {
            vertSpeed = minFall;
            animator.SetBool("Jumping", false);
        }
        else
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }
            if (contact != null)
            {
                animator.SetBool("Jumping", true);
            }

            // Workaround for standing on drop-off edge.
            if (charController.isGrounded)
            {
                if (Vector3.Dot(movement, contact.normal) < 0)
                {
                    movement = contact.normal * moveSpeed;
                }
                else
                {
                    movement += contact.normal * moveSpeed;
                }
            }
        }
        movement.y = vertSpeed;

        movement *= Time.deltaTime;
        charController.Move(movement);
    }

    // Store collision to use in Update.
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
