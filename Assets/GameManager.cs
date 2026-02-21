using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        round = 0;
        isGameOver = false;
        isGameDone = false;
        isGameWin = false;
        isGameWon = false;
        t_score.text = "" + score;
        gamewin.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);

        // 모든 보스/HP바/클리어 문구 비활성화
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(false);
            enemy_hps[i].SetActive(false);
        }
        for (int i = 0; i < bosswin.Length; i++)
        {
            bosswin[i].SetActive(false);
        }

        GamePlay();
    }

    public void GetScore()
    {
        t_score.text = "" + score;
    }

    public void GameWin()
    {
        // 직전 보스와 HP바 비활성화
        enemys[round].SetActive(false);
        enemy_hps[round].SetActive(false);

        round++;

        if (round < enemys.Length)
        {
            // 중간 보스 클리어 → 클리어 문구 보여주고 다음 라운드
            StartCoroutine(nextRound());
        }
        else
        {
            // 최종 보스 클리어 → 게임 완료
            isGameDone = true;
            isGameWon = true;
            final_score.SetActive(true);
            final_score.GetComponent<Text>().text = t_score.text + " points";
            playagain.SetActive(true);
            gamewin.SetActive(true);
            gamewin.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        }
    }

    public void isGamelost()
    {
        isGameDone = true;

        // 현재 라운드 보스와 HP바 비활성화 (모든 라운드 대응)
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(false);
            enemy_hps[i].SetActive(false);
        }

        // 광고를 먼저 보여주고, 광고 완료 후 ShowGameOverUI() 호출
        if (AdManager.instance != null)
        {
            AdManager.instance.ShowInterstitialAd(ShowGameOverUI);
        }
        else
        {
            ShowGameOverUI();
        }
    }

    public void ShowGameOverUI()
    {
        final_score.SetActive(true);
        final_score.GetComponent<Text>().text = t_score.text + " points";
        playagain.SetActive(true);
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
        if (round == 0)
            enemys[round].GetComponent<Ememy>().isEnemyPlay = true;
        else if (round == 1)
            enemys[round].GetComponent<Enemy1_2>().isEnemyPlay = true;
        else if (round == 2)
            enemys[round].GetComponent<Enemy1_3>().isEnemyPlay = true;
        else if (round == 3)
            enemys[round].GetComponent<Enemy2>().isEnemyPlay = true;
    }

    IEnumerator nextRound()
    {
        // 클리어 문구 표시 (round는 이미 증가된 상태 → round-1이 방금 잡은 보스)
        int clearedRound = round - 1;
        bosswin[clearedRound].SetActive(true);
        bosswin[clearedRound].GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        yield return new WaitForSeconds(1);
        bosswin[clearedRound].GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
        bosswin[clearedRound].SetActive(false);

        // 상태 리셋 후 다음 보스 활성화
        isGameOver = false;
        isGameWin = false;
        enemys[round].SetActive(true);
        enemy_hps[round].SetActive(true);
        StartCoroutine(enemyPlay());
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
        SceneManager.LoadScene(0);
    }
}
