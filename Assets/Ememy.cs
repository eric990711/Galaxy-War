using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ememy : MonoBehaviour
{
    float rightMax = 25.0f; //좌로 이동가능한 (x)최대값
    float leftMax = -25.0f; //우로 이동가능한 (x)최대값
    float currentPosition; //현재 위치(x) 저장
    float direction = 15;
    public GameObject spawn_R;
    public GameObject spawn_L;
    public GameObject enemy_hp;
    public GameObject enemy_hp_0;
    public GameObject explosion;
    public bool isEnemyPlay;
    int gamescore = 0;
    void Start()
    {
        StartCoroutine(enemyStart());
    }
    IEnumerator enemyStart()
    {
        yield return new WaitForSeconds(1);
        Fire();
        currentPosition = transform.position.x;
    }
    // Update is called once per frame
    void Update()
    {
        if (isEnemyPlay == true)
        {
            currentPosition += direction * Time.deltaTime;

            if (currentPosition <= leftMax)
            {
                direction = Random.Range(15, 40);
                leftMax = currentPosition;
            }
            else if (currentPosition >= rightMax)
            {
                direction = Random.Range(15, 40);
                direction *= -1;
                rightMax = currentPosition;
            }

            transform.position = new Vector3(currentPosition, 43.6f, 0);
        }
    }

    void Fire()
    {
        spawn_R.GetComponent<SwpanBullet>().Fire();
        spawn_L.GetComponent<SwpanBullet>().Fire();
        if (GameManager.instance.isGameOver == false)
            Invoke("Fire", 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            enemy_hp_0.GetComponent<Image>().fillAmount -= 0.005f;

            GameManager.instance.score += 10;
            GameManager.instance.GetScore();

            if (enemy_hp_0.GetComponent<Image>().fillAmount == 0)
            {
                enemy_hp.GetComponent<Image>().fillAmount = 0;
                GameManager.instance.isGameOver = true;
                GameManager.instance.GameWin();
                GameObject go = Instantiate(explosion);
                gameObject.SetActive(false);
            }
        }
    }
}
