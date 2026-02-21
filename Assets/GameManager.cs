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
        get { return _instance; }
    }

    void Start()
    {
        if (instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void Update() { }

    // 게임 시작 시 모든 상태 초기화
    public void INTI()
    {
        round = 0;
        score = 0;
        isGameOver = false;
        isGameDone = false;
        isGameWin = false;
        isGameWon = false;

        playercontrol.SetActive(true);
        spawnmeteor.SetActive(true);
        t_score.text = "" + score;
        gamewin.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(false);
            enemy_hps[i].SetActive(false);
        }
        for (int i = 0; i < bosswin.Length; i++)
        {
            bosswin[i].SetActive(false);
        }

        StartRound();
    }

    public void GetScore()
    {
        t_score.text = "" + score;
    }

    // 현재 round의 보스를 활성화하고 시작
    void StartRound()
    {
        // 혹시 모를 이전 보스 전부 끄기
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(false);
            enemy_hps[i].SetActive(false);
        }

        enemys[round].SetActive(true);
        enemy_hps[round].SetActive(true);
        StartCoroutine(enemyPlay());
    }

    // 1초 후 현재 round 보스 움직임/공격 시작
    IEnumerator enemyPlay()
    {
        yield return new WaitForSeconds(1);

        // 비활성화 됐거나 게임 끝났으면 무시
        if (isGameDone) yield break;
        if (!enemys[round].activeInHierarchy) yield break;

        if (round == 0)
            enemys[round].GetComponent<Ememy>().isEnemyPlay = true;
        else if (round == 1)
            enemys[round].GetComponent<Enemy1_2>().isEnemyPlay = true;
        else if (round == 2)
            enemys[round].GetComponent<Enemy1_3>().isEnemyPlay = true;
        else if (round == 3)
            enemys[round].GetComponent<Enemy2>().isEnemyPlay = true;
    }

    // 보스 처치 시 호출
    public void GameWin()
    {
        if (isGameDone) return; // 중복 호출 방지

        isGameOver = true;

        int clearedRound = round; // 방금 잡은 보스 인덱스 저장

        // 잡은 보스와 HP바 즉시 비활성화
        enemys[clearedRound].SetActive(false);
        enemy_hps[clearedRound].SetActive(false);

        round++; // 다음 라운드로

        if (round < enemys.Length)
        {
            // 아직 보스가 남아있음 → 클리어 문구 보여주고 다음 라운드
            StartCoroutine(nextRound(clearedRound));
        }
        else
        {
            // 모든 보스 처치 → 게임 클리어
            isGameDone = true;
            isGameWon = true;
            final_score.SetActive(true);
            final_score.GetComponent<Text>().text = t_score.text + " points";
            playagain.SetActive(true);
            gamewin.SetActive(true);
            gamewin.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        }
    }

    // clearedRound = 방금 잡은 보스 인덱스
    IEnumerator nextRound(int clearedRound)
    {
        // 클리어 문구 표시
        if (clearedRound < bosswin.Length)
        {
            bosswin[clearedRound].SetActive(true);
            bosswin[clearedRound].GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
        }

        yield return new WaitForSeconds(1);

        // 문구 숨기기
        if (clearedRound < bosswin.Length)
        {
            bosswin[clearedRound].GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
            bosswin[clearedRound].SetActive(false);
        }

        // 다음 보스 시작
        isGameOver = false;
        StartRound();
    }

    // 플레이어 사망 시 호출
    public void isGamelost()
    {
        if (isGameDone) return; // 중복 호출 방지

        isGameDone = true;

        // 모든 보스 비활성화
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(false);
            enemy_hps[i].SetActive(false);
        }

        // 광고 먼저, 완료 후 게임오버 UI
        if (AdManager.instance != null)
            AdManager.instance.ShowInterstitialAd(ShowGameOverUI);
        else
            ShowGameOverUI();
    }

    public void ShowGameOverUI()
    {
        final_score.SetActive(true);
        final_score.GetComponent<Text>().text = t_score.text + " points";
        playagain.SetActive(true);
        gamelost.SetActive(true);
        gamelost.GetComponent<Image>().color = new Color32(255, 255, 225, 255);
    }

    // 구버전 호환용 (씬에서 버튼이 이 함수를 참조할 경우 대비)
    public void GamePlay()
    {
        StartRound();
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
