using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    
    void Start()
    {

    }

    
    void Update()
    {
        if (transform.position.y > 80)
            Destroy(gameObject);
        transform.Translate(Vector3.up * speed);
    }
}
