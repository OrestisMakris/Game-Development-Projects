using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseShield : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private float shieldCooldown = 10f;
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
        return currentShield != null && currentShield.gameObject.activeInHierarchy;
    }
}
