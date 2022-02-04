using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteormove : MonoBehaviour
{
    public float speed;
    public GameObject explosion;
    public GameObject[] rots;
    void Start()
    {
        int rn = Random.Range(0,3);
        rots[rn].GetComponent<Animation>().enabled = false;
        rots[0].GetComponent<Animation>()["Meteor_x"].speed = Random.Range(0.1f, 0.5f);
        rots[1].GetComponent<Animation>()["Meteor_y"].speed = Random.Range(0.1f, 0.5f);
        rots[2].GetComponent<Animation>()["Meteor_z"].speed = Random.Range(0.1f, 0.5f);
        speed = Random.Range(0.2f, 0.4f);
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
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
