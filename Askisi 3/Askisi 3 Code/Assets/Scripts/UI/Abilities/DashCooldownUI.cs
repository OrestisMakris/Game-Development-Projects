using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldownUI : MonoBehaviour
{
    [SerializeField] private Image dashIcon; // Assign the Dash UI Icon Image
    [SerializeField] private UseDash useDash; // To get the dash cooldown

    private Coroutine cooldownRoutine;

    private void Start()
    {
        ResetCooldownUI();
    }

    public void StartCooldown()
    {
        if (cooldownRoutine != null) StopCoroutine(cooldownRoutine);
        cooldownRoutine = StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        float dashCooldown = useDash.GetDashCooldown(); // Get cooldown dynamically

        dashIcon.fillAmount = 0; // Start with an empty cooldown circle
        dashIcon.color = new Color(1, 1, 1, 0.3f); // Make it transparent initially

        float elapsedTime = 0f;
        while (elapsedTime < dashCooldown)
        {
            elapsedTime += Time.deltaTime;
            dashIcon.fillAmount = elapsedTime / dashCooldown; // Circular fill effect
            dashIcon.color = new Color(1, 1, 1, Mathf.Lerp(0.3f, 1f, elapsedTime / dashCooldown)); // Fade in
            yield return null;
        }

        dashIcon.fillAmount = 1; // Fully visible after cooldown
        dashIcon.color = new Color(1, 1, 1, 1f);
    }

    public void ResetCooldownUI()
    {
        dashIcon.fillAmount = 1; // Fully visible
        dashIcon.color = new Color(1, 1, 1, 1f); // Fully opaque
    }
}
