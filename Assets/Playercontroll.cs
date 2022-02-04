using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroll : MonoBehaviour
{
    bool isleft;
    bool isright;
    public float speed;
    public GameObject spawn_R;
    public GameObject spawn_2_R;
    public GameObject spawn_L;
    public GameObject spawn_2_L;
    public float bulletspeed;

    void Start()
    {
        bulletspeed = 0.2f;
        Fire();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow) || isright == true)
        {
            if(transform.position.x <= 32)
                transform.Translate(Vector3.right * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || isleft == true)
        {
            if (transform.position.x >= -32)
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
        if (GameManager.instance.score <= 2000)
        {
            spawn_R.GetComponent<SwpanBullet>().Fire();
            spawn_L.GetComponent<SwpanBullet>().Fire();
        }
        if (GameManager.instance.score >= 2000)
        {
            spawn_R.GetComponent<SwpanBullet>().Fire();
            spawn_L.GetComponent<SwpanBullet>().Fire();
            spawn_2_R.GetComponent<SwpanBullet>().Fire();
            spawn_2_L.GetComponent<SwpanBullet>().Fire();
        }
        Invoke("Fire", bulletspeed);

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