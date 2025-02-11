using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip winSound; // Sound played on winning the objective

    void OnTriggerEnter(Collider other)
    {
        // Only play winSound if all enemies have been cleared.
        if (Managers.Mission != null && Managers.Mission.enemiesCleared)
        {
            if (winSound != null)
            {
                AudioManager.Instance.PlaySound(winSound);
            }
        }

        Managers.Mission.ReachObjective();
    }
}