using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;
    public float jumpForce = 12.0f;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D box;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;

        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x - 0.01f, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x + 0.01f, min.y - 0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = hit != null;

        // Platform descent: independent of horizontal movement
        if (grounded && Input.GetKeyDown(KeyCode.S)&& hit.CompareTag("Platform"))
        {
            StartCoroutine(IgnoreCollisionS(hit));
        }

        // Jumping
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        MovingPlatform platform = null;

        if (hit != null)
        {
            platform = hit.GetComponent<MovingPlatform>();
        }

        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }

        // Update animation speed
        anim.SetFloat("speed", Mathf.Abs(deltaX));

        // Flip player sprite based on direction
        Vector3 pScale = Vector3.one;
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }

        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1);
        }
    }

// temp disable collision with a platform
    private IEnumerator IgnoreCollisionS(Collider2D platform)
    {
        if (platform != null)
        {
            Physics2D.IgnoreCollision(platform, box, true); // Disable collision
            yield return new WaitForSeconds(0.5f); // Wait briefly
            Physics2D.IgnoreCollision(platform, box, false); // Re-enable collision
        }
    }
}
