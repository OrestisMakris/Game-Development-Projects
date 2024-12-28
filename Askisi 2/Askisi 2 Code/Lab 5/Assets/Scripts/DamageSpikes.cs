using UnityEngine;

public class Spike : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Object entered trigger: {collision.name}"); // Log every object
        if (collision.GetComponent<PlatformerPlayer>() != null)
        {
            Debug.Log("Player hit a spike! Game Over.");
            // Add player damage or reset logic here
        }
    }
}
