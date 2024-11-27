using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtEnemy(int damage){
        EnemyAI behavior = GetComponent<EnemyAI>();
        
        health -= damage;
        Debug.Log("Enemy Hit! Remaining Health: " + health);
        if (health <= 0){
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
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
