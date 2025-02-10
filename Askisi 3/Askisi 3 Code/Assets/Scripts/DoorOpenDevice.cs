using UnityEngine;
using DG.Tweening; // Import DOTween namespace

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 dPos;
    private bool open;
    [SerializeField] private float duration = 2f; // Duration of the tween

    // Operates the door based on its current state
    public void Operate()
    {
        Vector3 targetPos = open ? transform.position - dPos : transform.position + dPos;
        transform.DOMove(targetPos, duration).OnComplete(() => open = !open);
    }

    // Activates the door to open position
    public void Activate()
    {
        if (!open)
        {
            transform.DOMove(transform.position + dPos, duration).OnComplete(() => open = true);
        }
    }

    // Deactivates the door to closed position
    public void Deactivate()
    {
        if (open)
        {
            transform.DOMove(transform.position - dPos, duration).OnComplete(() => open = false);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Initialization code if needed
    }

    // Update is called once per frame
    private void Update()
    {
        // Per-frame update logic if needed
    }
}