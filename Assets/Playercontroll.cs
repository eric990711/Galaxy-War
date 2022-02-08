using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroll : MonoBehaviour
{
    bool isleft;
    bool isright;
    public float speed;
    public GameObject spawn_R;
    public GameObject spawn0_R;
    public GameObject spawn_2_R;
    public GameObject spawn_2_L;
    public GameObject spawn0_L;
    public GameObject spawn_L;
    public GameObject spawn_3;
    public float bulletspeed;

    void Start()
    {
        bulletspeed = 0.15f;
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
        if (GameManager.instance.round == 0)
        {
            spawn_R.GetComponent<SwpanBullet>().Fire();
            spawn_L.GetComponent<SwpanBullet>().Fire();
        }
        else if (GameManager.instance.round == 1)
        {
            spawn0_R.GetComponent<SwpanBullet>().Fire();
            spawn0_L.GetComponent<SwpanBullet>().Fire();
        }
        else if(GameManager.instance.round == 2)
        {
            spawn_R.GetComponent<SwpanBullet>().Fire();
            spawn_L.GetComponent<SwpanBullet>().Fire();
            spawn_2_R.GetComponent<SwpanBullet>().Fire();
            spawn_2_L.GetComponent<SwpanBullet>().Fire();
        }
        else if (GameManager.instance.round == 3)
        {
            spawn_R.GetComponent<SwpanBullet>().Fire();
            spawn_L.GetComponent<SwpanBullet>().Fire();
            spawn_2_R.GetComponent<SwpanBullet>().Fire();
            spawn_2_L.GetComponent<SwpanBullet>().Fire();
            spawn_3.GetComponent<SwpanBullet>().Fire();
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