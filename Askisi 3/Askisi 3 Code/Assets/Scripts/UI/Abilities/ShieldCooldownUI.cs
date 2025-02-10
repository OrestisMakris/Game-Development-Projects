using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShieldCooldownUI : MonoBehaviour
{
    [SerializeField] private Image shieldIcon; // Assign the Shield UI Icon Image

    private Coroutine cooldownRoutine;

    private void Start()
    {
        ResetCooldownUI();
    }

    public void StartCooldown(float shieldCooldown)
    {
        if (cooldownRoutine != null) StopCoroutine(cooldownRoutine);
        cooldownRoutine = StartCoroutine(CooldownRoutine(shieldCooldown));
    }

    private IEnumerator CooldownRoutine(float shieldCooldown)
    {
        //float shieldCooldown = useShield.GetShieldCooldown(); // Get cooldown dynamically

        shieldIcon.fillAmount = 0; // Start with an empty cooldown circle
        shieldIcon.color = new Color(1, 1, 1, 0.3f); // Make it transparent initially

        float elapsedTime = 0f;
        while (elapsedTime < shieldCooldown)
        {
            elapsedTime += Time.deltaTime;
            shieldIcon.fillAmount = elapsedTime / shieldCooldown; // Circular fill effect
            shieldIcon.color = new Color(1, 1, 1, Mathf.Lerp(0.3f, 1f, elapsedTime / shieldCooldown)); // Fade in
            yield return null;
        }

        shieldIcon.fillAmount = 1; // Fully visible after cooldown
        shieldIcon.color = new Color(1, 1, 1, 1f);
    }

    public void ResetCooldownUI()
    {
        shieldIcon.fillAmount = 1; // Fully visible
        shieldIcon.color = new Color(1, 1, 1, 1f); // Fully opaque
    }
}
