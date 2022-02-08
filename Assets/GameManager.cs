using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameStart = false;
    public bool isGameWon = false;
    public int round;
    public GameObject[] enemys;
    public GameObject[] enemy_hps;
    public int score;
    public Text t_score;
    public GameObject final_score;
    public bool isGameOver;
    public bool isGameDone;
    public bool isGameWin;
    public bool isGamePlay;
    public GameObject gamewin;
    public GameObject playagain;
    public GameObject[] bosswin;
    public GameObject gamelost;
    private static GameManager _instance = null;
    public GameObject playercontrol;
    public GameObject spawnmeteor;
    public GameObject newgamebutton;
    public GameObject gamename;
    public GameObject timeguage;
    public static GameManager instance
    {
        get
        {
            return _instance;
        }
    }

    void Start()
    {
        if (instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        
    }
    public void INTI()
    {
        playercontrol.SetActive(true);
        spawnmeteor.SetActive(true);
        score = 0;
        t_score.text = "" + score;
        //gamewin.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        gamewin.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(false);
        }
        GamePlay();
    }
    public void GetScore()
    {
        t_score.text = "" + score;
    }
    public void GameWin()
    {
        isGameWin = true;

        round++;
        if (round < enemys.Length)
        {
            StartCoroutine(nextRound());
        }
        else
        {
            final_score.SetActive(true);
            final_score.GetComponent<Text>().text = t_score.text + " points";
            playagain.SetActive(true);
            isGameDone = true;
            gamewin.SetActive(true);
            gamewin.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            isGameWon = true;
        }
    }

    public void isGamelost()
    {
        final_score.SetActive(true);
        final_score.GetComponent<Text>().text = t_score.text + " points";
        playagain.SetActive(true);
        isGameDone = true;
        if (round == 0)
            enemys[round].SetActive(false);
        else if (round == 1)
            enemys[round].SetActive(false);
        gamelost.SetActive(true);
        gamelost.GetComponent<Image>().color = new Color32(255, 255, 225, 255);

    }


    public void GamePlay()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(false);
            enemy_hps[i].SetActive(false);
        }
        enemys[round].SetActive(true);
        enemy_hps[round].SetActive(true);
        StartCoroutine(enemyPlay());
    }
    IEnumerator enemyPlay()
    {
        yield return new WaitForSeconds(1);
        if(round == 0)
            enemys[round].GetComponent<Ememy>().isEnemyPlay = true;
        else if(round == 1)
            enemys[round].GetComponent<Enemy1_2>().isEnemyPlay = true;
        else if(round == 2)
            enemys[round].GetComponent<Enemy1_3>().isEnemyPlay = true;
        else if (round == 3)
            enemys[round].GetComponent<Enemy2>().isEnemyPlay = true;
    }


    IEnumerator nextRound()
    {
        bosswin[round-1].SetActive(true);
        bosswin[round-1].GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        yield return new WaitForSeconds(1);
        bosswin[round-1].GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
        StartCoroutine(enemyPlay());

        isGameOver = false;
        enemys[round].SetActive(true);
        enemy_hps[round].SetActive(true);
    }

    public void Game_started()
    {
        timeguage.GetComponent<TimeGauge>().Time_on();
        isGameStart = true;
        INTI();
        newgamebutton.SetActive(false);
        gamename.SetActive(false);
    }

    public void Play_again()
    {
        Application.LoadLevel(0);
    }
}
