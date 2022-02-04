using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletE : MonoBehaviour
{
    public float speed;
    public GameObject explosion;
    public GameObject small_explosion;

    void Start()
    {

    }


    void Update()
    {
        if (transform.position.y < -70)
            Destroy(gameObject);
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
        if(other.tag == "Player")
        {
            GameObject goo = Instantiate(small_explosion);
            goo.transform.position = transform.position;
            Destroy(gameObject);
        }
    }


}