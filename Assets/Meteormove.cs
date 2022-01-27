using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteormove : MonoBehaviour
{
    public float speed;
    public GameObject explosion;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bullet")
        {
            GameObject go = Instantiate(explosion);
            go.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
