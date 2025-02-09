using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private float shieldDuration = 3f;
    [SerializeField] private GameObject shieldVisual;
    private CapsuleCollider shieldCollider;
    private ShieldCooldownUI shieldCooldownUI;
    public bool isShieldActive = false;

    private void Awake()
    {
        isShieldActive = false;
        shieldCollider = GetComponent<CapsuleCollider>();
    }

    public void ActivateShield()
    {
        shieldVisual.SetActive(true);
        shieldCollider.enabled = true;
        isShieldActive = true;
        Invoke(nameof(DeactivateShield), shieldDuration);
    }

    private void DeactivateShield()
    {
        shieldVisual.SetActive(false);
        shieldCollider.enabled = false;
        isShieldActive = false;
        shieldCooldownUI.StartCooldown(); // Start UI cooldown effect
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyFireball"))
        {
            Destroy(other.gameObject);
        }
    }

    public void SetCooldownUI(ShieldCooldownUI cooldownUI)
    {
        shieldCooldownUI = cooldownUI;
    }

}