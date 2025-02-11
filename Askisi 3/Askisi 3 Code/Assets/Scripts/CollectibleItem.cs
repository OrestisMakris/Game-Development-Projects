using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private AudioClip collectItemSound; // Sound played when item is collected

    void OnTriggerEnter(Collider other)
    {
        Managers.Inventory.AddItem(itemName);

        // Play collect sound using AudioManager with the default SFX volume
        if (collectItemSound != null)
        {
            AudioManager.Instance.PlaySound(collectItemSound, 0.5f);
        }

        Destroy(gameObject);
    }

    void Start() {


     }

    void Update() { 


        
    }
}