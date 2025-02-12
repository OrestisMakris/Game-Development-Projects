using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] targets;
    [SerializeField] private string requiredKey;
    private bool isActivated = false;

    void OnTriggerEnter(Collider other)
    {
        // Only the player should trigger the door
        if (!other.CompareTag("Player"))
            return;

        if (isActivated)
            return;

        if (!string.IsNullOrEmpty(requiredKey) && 
            (Managers.Inventory.equippedItem == null || 
             !Managers.Inventory.equippedItem.Equals(requiredKey, System.StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                target.SendMessage("Activate");
            }
        }
        isActivated = true;
    }

    void OnTriggerExit(Collider other)
    {
        // Only the player should deactivate the door
        if (!other.CompareTag("Player"))
            return;
            
        if (!isActivated)
            return;

        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                target.SendMessage("Deactivate");
            }
        }
        isActivated = false;
    }

    private void OnDisable()
    {
        // Ensure doors are deactivated when trigger is disabled
        if (isActivated)
        {
            foreach (GameObject target in targets)
            {
                if (target != null)
                {
                    target.SendMessage("Deactivate");
                }
            }
            isActivated = false;
        }
    }
}