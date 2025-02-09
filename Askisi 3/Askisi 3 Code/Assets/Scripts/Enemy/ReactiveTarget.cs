using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    [SerializeField] FOV FovBox;
    [SerializeField] private int health = 100;

    private Renderer objectRenderer;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color; // Capture the original color of the object
    }
    public void ReactToHit(int damage)
    {
        health -= damage;
        StartCoroutine(GetHurt());
        if (health < 1)
        {
            WanderingAI behavior = GetComponent<WanderingAI>();
            if (behavior != null)
            {
                behavior.SetAlive(false);
            }
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);
        FovBox.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    private IEnumerator GetHurt()
    {
        objectRenderer.material.color = Color.red;
        yield return new WaitForSeconds(.1f);
        objectRenderer.material.color = originalColor;
    }
}
