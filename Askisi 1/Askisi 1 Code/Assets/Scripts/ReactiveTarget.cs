using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ReactiveTarget : MonoBehaviour
{

    public void ReactToHit()
    {
        EnemyHealth health = GetComponent<EnemyHealth>();
        health.HurtEnemy(1);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
