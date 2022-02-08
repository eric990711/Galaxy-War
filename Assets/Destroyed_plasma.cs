using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyed_plasma : MonoBehaviour
{
    public float delaytime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,delaytime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
