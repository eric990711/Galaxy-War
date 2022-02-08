using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameover : MonoBehaviour
{
    public GameObject hp;
    public GameObject explosion;
    public GameObject overimage;
    public GameObject timegauge;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "asteroid" || other.tag == "Enemybullet")
        {
            hp.GetComponent<Image>().fillAmount -= 0.005f;
            if(hp.GetComponent<Image>().fillAmount == 0 || timegauge.GetComponent<Image>().fillAmount == 0)
            {
                GameManager.instance.isGameOver = true;
                GameObject go = Instantiate(explosion);
                GameManager.instance.isGamelost();
                Destroy(gameObject);
            }
        }
    }
}
