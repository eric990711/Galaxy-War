using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameover : MonoBehaviour
{
    public GameObject hp;
    public GameObject explosion;
    public GameObject overimage;
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
            hp.GetComponent<Image>().fillAmount -= 0.01f;
            if(hp.GetComponent<Image>().fillAmount == 0)
            {
                GameManager.instance.isGameOver = true;
                GameObject go = Instantiate(explosion);
                Destroy(gameObject);
                overimage.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
            }
        }
    }
}
