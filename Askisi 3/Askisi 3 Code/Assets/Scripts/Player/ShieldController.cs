using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private float shieldDuration = 3f;
    [SerializeField] private GameObject shieldVisual;
    private CapsuleCollider shieldCollider;

    private UseShield useShield; // To check if shield is used and start cooldown
    private ShieldCooldownUI shieldCooldownUI; // To start UI cooldown effect
    public bool isShieldActive = false;

    private void Awake()
    {
        isShieldActive = false;
        shieldCollider = GetComponent<CapsuleCollider>();
    }

    public void ActivateShield()
    {
        if (!useShield.IsShieldUsed())
        {
            shieldVisual.SetActive(true);
            shieldCollider.enabled = true;
            isShieldActive = true;
            Invoke(nameof(DeactivateShield), shieldDuration);
        }
    }

    private void DeactivateShield()
    {
        useShield.StartCooldown(); // Reset shield after cooldown
        shieldCooldownUI.StartCooldown(useShield.GetShieldCooldown()); // Start UI cooldown effect
        shieldVisual.SetActive(false);
        shieldCollider.enabled = false;
        isShieldActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyFireball"))
        {
            Destroy(other.gameObject);
        }
    }

    public void SetUseShield(UseShield useShield)
    {
        this.useShield = useShield;
    }

    public void SetShieldCooldownUI(ShieldCooldownUI shieldCooldownUI)
    {
        this.shieldCooldownUI = shieldCooldownUI;
    }

}