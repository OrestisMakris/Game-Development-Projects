using UnityEngine;
using DG.Tweening; // Import DOTween namespace

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 dPos;
    [SerializeField] private float duration = 2f; // Duration of the tween
    [SerializeField] private AudioClip doorSound; // Door sound clip

    private bool open;

    // Start is called before the first frame update
    private void Start()
    {
        // No local AudioSource; audio is now managed by AudioManager.
    }

    // Operates the door based on its current state
    public void Operate()
    {
        // Play door sound via AudioManager before operating
        if(doorSound != null)
        {
            AudioManager.Instance.PlaySound(doorSound);
        }

        Vector3 targetPos = open ? transform.position - dPos : transform.position + dPos;
        transform.DOMove(targetPos, duration).OnComplete(() => open = !open);
    }

    // Activates the door to open position
    public void Activate()
    {
        if (!open)
        {
            // Play door sound via AudioManager before activating
            if(doorSound != null)
            {
                AudioManager.Instance.PlaySound(doorSound);
            }
            
            transform.DOMove(transform.position + dPos, duration).OnComplete(() => open = true);
        }
    }

    // Deactivates the door to closed position
    public void Deactivate()
    {
        if (open)
        {
            // Play door sound via AudioManager before deactivating
            if(doorSound != null)
            {
                AudioManager.Instance.PlaySound(doorSound);
            }
            
            transform.DOMove(transform.position - dPos, duration).OnComplete(() => open = false);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Per-frame update logic if needed
    }
}