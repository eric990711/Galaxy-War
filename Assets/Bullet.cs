using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public GameObject small_explosion;


    void Start()
    {

    }

    
    void Update()
    {
        if (transform.position.y > 80)
            Destroy(gameObject);
        transform.Translate(Vector3.up * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Enemy")
        {
            GameObject goo = Instantiate(small_explosion);
            goo.transform.position = transform.position;
            Destroy(gameObject);
        }
        if (other.tag == "asteroid")
        {
            Destroy(gameObject);
        }
    }
}
