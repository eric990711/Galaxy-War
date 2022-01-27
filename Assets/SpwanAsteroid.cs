using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanAsteroid : MonoBehaviour
{
    public GameObject[] meteors;

    // Start is called before the first frame update
    void Start()
    {
        make();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void make()
    {
        transform.position = new Vector3(Random.Range(-50, 50), 80, 0);
        GameObject go = Instantiate(meteors[0]);
        go.transform.position = transform.position;
        Invoke("make", 0.5f);

    }
}
