using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private float shieldDuration = 3f;
    [SerializeField] private GameObject shieldVisual;
    private CapsuleCollider shieldCollider;

    private void Awake()
    {
        shieldCollider = GetComponent<CapsuleCollider>();
        DeactivateShield();
    }

    public void ActivateShield()
    {
        shieldVisual.SetActive(true);
        shieldCollider.enabled = true;
        Invoke(nameof(DeactivateShield), shieldDuration);
    }

    private void DeactivateShield()
    {
        shieldVisual.SetActive(false);
        shieldCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyFireball"))
        {
            Destroy(other.gameObject);
        }
    }
}