using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseShield : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private float shieldCooldown = 5f;
    [SerializeField] private ShieldCooldownUI shieldCooldownUI;
    private ShieldController currentShield;
    private bool isShieldReady = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isShieldReady)
        {
            ActivateShield();
        }
    }

    private void ActivateShield()
    {
        if (currentShield == null)
        {
            GameObject shieldObj = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
            currentShield = shieldObj.GetComponent<ShieldController>();
            currentShield.SetCooldownUI(shieldCooldownUI); // Set the cooldown UI reference
        }

        currentShield.ActivateShield();
        isShieldReady = false;
        Invoke(nameof(ResetShield), shieldCooldown); // Reset shield after cooldown
    }

    private void ResetShield()
    {
        isShieldReady = true;
    }

    public bool IsShieldActive()
    {
        return currentShield != null && currentShield.isShieldActive;
    }

    // Shield cooldown gette
    public float GetShieldCooldown()
    {
        return shieldCooldown;
    }
}
