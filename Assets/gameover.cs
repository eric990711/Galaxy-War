using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameover : MonoBehaviour
{
    public GameObject explosion;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "asteroid")
        {
            Instantiate(explosion);
            Destroy(gameObject);
        }
    }
}
