using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private float shieldDuration = 3f;
    [SerializeField] private GameObject shieldVisual;
    [SerializeField] private AudioClip shieldSound; // Shield activation sound

    private CapsuleCollider shieldCollider;
    private UseShield useShield;
    private ShieldCooldownUI shieldCooldownUI;
    public bool isShieldActive = false;

    private void Awake()
    {
        isShieldActive = false;
        shieldCollider = GetComponent<CapsuleCollider>();
        if(GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }
    }

    public void ActivateShield()
    {
        if (!useShield.IsShieldUsed())
        {
            shieldVisual.SetActive(true);
            shieldCollider.enabled = true;
            isShieldActive = true;
            
            if(shieldSound != null)
            {
                AudioManager.Instance.PlaySound(shieldSound);
            }
            
            Invoke(nameof(DeactivateShield), shieldDuration);
        }
    }

    private void DeactivateShield()
    {
        useShield.StartCooldown();
        shieldCooldownUI.StartCooldown(useShield.GetShieldCooldown());
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