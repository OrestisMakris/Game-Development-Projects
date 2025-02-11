using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip winSound; // Sound played on winning the objective

    void OnTriggerEnter(Collider other)
    {
        // Play win sound using AudioManager
        if (winSound != null)
        {
            AudioManager.Instance.PlaySound(winSound);
        }

        Managers.Mission.ReachObjective();
    }
}