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
        int rn = Random.Range(0, 3);

        // rn번째 축 회전 비활성화 (랜덤하게 하나 끄기)
        Animator animToDisable = rots[rn].GetComponent<Animator>();
        if (animToDisable != null)
            animToDisable.enabled = false;

        // 모든 축 랜덤 속도 설정
        for (int i = 0; i < rots.Length; i++)
        {
            Animator anim = rots[i].GetComponent<Animator>();
            if (anim != null)
                anim.speed = Random.Range(0.1f, 0.5f);
        }

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
        if (other.tag == "bullet")
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
