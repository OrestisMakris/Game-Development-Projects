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
            ShieldUsed();
        }
    }

    private void ShieldUsed()
    {
        if (currentShield == null)
        {
            GameObject shieldObj = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
            currentShield = shieldObj.GetComponent<ShieldController>();
            if (currentShield != null)
            {
                currentShield.SetUseShield(this);
                currentShield.SetShieldCooldownUI(shieldCooldownUI);
                // currentShield.useShield = this;
                // currentShield.shieldCooldownUI = shieldCooldownUI;
            }
        }

        currentShield.ActivateShield();
        isShieldReady = false;
    }

    public void StartCooldown()
    {
        Invoke(nameof(ResetShield), shieldCooldown); // Reset shield after cooldown
    }

    private void ResetShield()
    {
        isShieldReady = true;
    }

    public bool IsShieldUsed()
    {
        return currentShield != null && !isShieldReady;
    }

    // Shield cooldown getter
    public float GetShieldCooldown()
    {
        return shieldCooldown;
    }
}
