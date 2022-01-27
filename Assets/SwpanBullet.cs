using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwpanBullet : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        GameObject go = Instantiate(bullet);
        go.transform.position = transform.position;
    }
}
