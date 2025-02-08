using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        PointClickMovement player = other.GetComponent<PointClickMovement>();
        if (player != null)
        {
            Managers.Player.ChangeHealth(-damage);
        }
        Destroy(this.gameObject);
    }
}
