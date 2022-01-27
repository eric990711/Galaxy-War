using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    
    void Start()
    {
        Destroy(gameObject, 1);
    }

    
    void Update()
    {
        transform.Translate(Vector3.up * speed);
    }
}
