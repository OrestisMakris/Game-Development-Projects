using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] targets;
    [SerializeField] private string requiredKey; // The specific key required to activate this trigger

    void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(requiredKey) && (Managers.Inventory.equippedItem == null || !Managers.Inventory.equippedItem.Equals(requiredKey)))
        {
            return;
        }
        foreach (GameObject target in targets)
        {
            target.SendMessage("Activate");
        }
    }

    void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }
}