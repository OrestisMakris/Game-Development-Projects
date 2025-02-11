using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public int health { get; private set; }
    public int maxHealth { get; private set; }

    [SerializeField] private AudioClip healthIncreaseSound; // Sound played when health increases

    public void Startup()
    {
        Debug.Log("Player manager starting...");
        UpdateData(50, 100);
        status = ManagerStatus.Started;
    }

    public void UpdateData(int health, int maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
    }

    public void ChangeHealth(int value)
    {
        int oldHealth = health;
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }
        
        // Play health increase sound if the change was positive.
        if (value > 0 && health > oldHealth)
        {
            if (healthIncreaseSound != null)
            {
                AudioManager.Instance.PlaySound(healthIncreaseSound);
            }
        }

        if (health == 0)
        {
            Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }

        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
    }

    public void Respawn()
    {
        UpdateData(50, 100);
    }
}