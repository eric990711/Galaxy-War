using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ememy : MonoBehaviour
{
    Vector3 pos; //현재위치
    float delta = 25.0f; // 좌(우)로 이동가능한 (x)최대값
    float speed = 1.0f;
    public GameObject spawn_R;
    public GameObject spawn_L;
    public GameObject enemy_hp;
    public GameObject explosion;
    public GameObject gamewin;
    void Start()
    {
        Fire();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = pos;
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }

    void Fire()
    {
        spawn_R.GetComponent<SwpanBullet>().Fire();
        spawn_L.GetComponent<SwpanBullet>().Fire();
        Invoke("Fire", 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bullet")
        {
            enemy_hp.GetComponent<Image>().fillAmount -= 0.005f;
            if(enemy_hp.GetComponent<Image>().fillAmount == 0)
            {
                GameObject go = Instantiate(explosion);
                Destroy(gameObject);
                gamewin.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
            }
        }
    }
}
