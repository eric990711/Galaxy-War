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
    public bool isGameOver;
    public bool isGameWin;
    public bool isGamePlay;
    public GameObject gamewin;
    private static GameManager _instance = null;
    public GameObject playercontrol;
    public GameObject spawnmeteor;
    public GameObject newgamebutton;
    public GameObject gamename;
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
            gamewin.SetActive(true);
            gamewin.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            isGameWon = true;
        }
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
        enemys[round].GetComponent<Ememy>().isEnemyPlay = true;
    }


    IEnumerator nextRound()
    {
        gamewin.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        yield return new WaitForSeconds(1);
        gamewin.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
        yield return new WaitForSeconds(1);

        isGameOver = false;
        enemys[round].SetActive(true);
        enemy_hps[round].SetActive(true);
    }

    public void Game_started()
    {
        isGameStart = true;
        INTI();
        newgamebutton.SetActive(false);
        gamename.SetActive(false);
    }
}
