using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroll : MonoBehaviour
{
    bool isleft;
    bool isright;
    public float speed;
    public GameObject spawn_R;
    public GameObject spawn_L;

    void Start()
    {
        Fire();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || isright == true)
        {
            transform.Translate(Vector3.right * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || isleft == true)
        {
            transform.Translate(Vector3.left * speed);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            spawn_R.GetComponent<SwpanBullet>().Fire();
            spawn_L.GetComponent<SwpanBullet>().Fire();

        }
    }
    void Fire()
    {
        spawn_R.GetComponent<SwpanBullet>().Fire();
        spawn_L.GetComponent<SwpanBullet>().Fire();
        Invoke("Fire",0.1f);

    }

    public void Moveleft_on()
    {
        isleft = true;

    }
    public void Moveleft_off()
    {
        isleft = false;
    }

    public void Moveright_on()
    {
        isright = true;
    }
    public void Moveright_off()
    {
        isright = false;
    }

    
}