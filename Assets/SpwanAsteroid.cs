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
        transform.position = new Vector3(Random.Range(-35, 35), 80, 0);
        GameObject go = Instantiate(meteors[Random.Range(0, meteors.Length)]);

        go.transform.position = transform.position;

        if (GameManager.instance.isGameDone == false)
            Invoke("make", 0.5f);
    }
}
