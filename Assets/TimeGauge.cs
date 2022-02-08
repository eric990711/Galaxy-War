using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeGauge : MonoBehaviour
{
    public Image gauge;
    public float palyTime;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Time_on()
    {
        gauge.fillAmount = 1;
        StartCoroutine(time_on());
    }
    IEnumerator time_on()
    {
        float t = 0;
        while (t < palyTime || GameManager.instance.isGameOver == false)
        {
            t += Time.deltaTime;
            gauge.fillAmount = 1 - (t / palyTime);
            yield return null;
        }
        print("GAMEOVER");

    }
}
