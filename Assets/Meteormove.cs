using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteormove : MonoBehaviour
{
    public float speed;
    public GameObject explosion;
    void Start()
    {
        speed = Random.Range(0.05f, 0.2f);
        float scl = Random.Range(10, 20);
        transform.localScale = new Vector3(scl, scl, scl);
    }

    void Update()
    {
        if (transform.position.y < -85)
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
    }
}
