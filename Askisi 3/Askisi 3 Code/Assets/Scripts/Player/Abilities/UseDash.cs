using System.Collections;
using UnityEngine;

public class UseDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;    // Duration of the dash (in seconds)
    [SerializeField] private float dashCooldown = 5f;      // Cooldown time before dash can be used again

    private bool isDashReady = true;  // Indicates if dash is ready
    private bool isDashing = false;   // Indicates if the player is currently dashing

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // When the player presses "W", start the dash if it is ready and the player is not already dashing.
        if (Input.GetKeyDown(KeyCode.W) && isDashReady && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashReady = false;
        isDashing = true;
        float startTime = Time.time;
        Vector3 dashDirection = transform.forward;  // Dash in the forward direction of the player

        // Continue moving the player at dashSpeed for the duration of the dash
        while (Time.time < startTime + dashDuration)
        {
            characterController.Move(dashDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }

        isDashing = false;
        // Start the cooldown before the dash can be used again
        Invoke(nameof(ResetDash), dashCooldown);
    }

    private void ResetDash()
    {
        isDashReady = true;
    }
}
