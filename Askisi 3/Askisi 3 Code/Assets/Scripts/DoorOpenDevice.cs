using UnityEngine;
using DG.Tweening;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 dPos;
    [SerializeField] private float duration = 2f;
    [SerializeField] private AudioClip doorSound;
    
    private bool open;
    private bool isMoving;
    private Tween currentTween;

    private void Start()
    {
        // No local AudioSource; audio is now managed by AudioManager.
    }

    public void Operate()
    {
        if (isMoving)
            return;

        if (doorSound != null)
            AudioManager.Instance.PlaySound(doorSound);

        currentTween?.Kill();
        Vector3 targetPos = open ? transform.position - dPos : transform.position + dPos;
        isMoving = true;

        currentTween = transform.DOMove(targetPos, duration)
            .OnComplete(() =>
            {
                open = !open;
                isMoving = false;
                currentTween = null;
            });
    }

    public void Activate()
    {
        // Prevent activation if door is already open or moving
        if (open || isMoving)
            return;

        if (doorSound != null)
            AudioManager.Instance.PlaySound(doorSound);

        currentTween?.Kill();
        isMoving = true;
        
        currentTween = transform.DOMove(transform.position + dPos, duration)
            .OnComplete(() =>
            {
                open = true;
                isMoving = false;
                currentTween = null;
            });
    }

    public void Deactivate()
    {
        // Prevent deactivation if door is already closed or moving
        if (!open || isMoving)
            return;

        if (doorSound != null)
            AudioManager.Instance.PlaySound(doorSound);

        currentTween?.Kill();
        isMoving = true;

        currentTween = transform.DOMove(transform.position - dPos, duration)
            .OnComplete(() =>
            {
                open = false;
                isMoving = false;
                currentTween = null;
                // Optionally, auto-raise the door immediately after it goes down:
                // Invoke("Activate", 0.1f);
            });
    }

    private void OnDestroy()
    {
        // Clean up any running tween when object is destroyed
        currentTween?.Kill();
    }
}