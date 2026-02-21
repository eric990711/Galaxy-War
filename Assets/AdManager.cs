using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

/// <summary>
/// Unity Ads 전면 광고(Interstitial) 매니저
///
/// [사용법]
/// 1. Unity Dashboard(dashboard.unity3d.com)에서 프로젝트 연결 후
///    Game ID와 Ad Unit ID를 발급받아 아래 상수에 입력하세요.
/// 2. 씬에 빈 GameObject를 만들고 이 스크립트를 붙여주세요.
/// 3. GameManager.isGamelost()에서 자동으로 호출됩니다.
/// </summary>
public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // ──────────────────────────────────────────────────────────
    // TODO: Unity Dashboard에서 발급받은 ID로 교체하세요
    // dashboard.unity3d.com → Monetization → Projects → 해당 프로젝트
    private const string GAME_ID_ANDROID = "6050514";
    private const string GAME_ID_IOS     = "";
    private const string AD_UNIT_ID      = "Interstitial_Android"; // Android 기본값
    // ──────────────────────────────────────────────────────────

    private const bool TEST_MODE = true; // 출시 전까지 true 유지. 출시 시 false로 변경
    private const int  AD_RETRY_DELAY = 30; // 광고 로드 실패 시 재시도 간격(초)

    public static AdManager instance { get; private set; }

    private string _gameId;
    private bool   _isAdLoaded   = false;
    private bool   _isInitialized = false;
    private Action _onAdClosed;  // 광고 닫힌 뒤 실행할 콜백

    // ──────────────────────────────────────────────────────────
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                  ? GAME_ID_IOS
                  : GAME_ID_ANDROID;

        Advertisement.Initialize(_gameId, TEST_MODE, this);
    }

    // ──────────────────────────────────────────────────────────
    // IUnityAdsInitializationListener
    public void OnInitializationComplete()
    {
        _isInitialized = true;
        Debug.Log("[AdManager] Unity Ads 초기화 완료");
        LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogWarning($"[AdManager] 초기화 실패: {error} - {message}");
    }

    // ──────────────────────────────────────────────────────────
    // 광고 미리 로드 (게임 시작 직후 호출해 준비해둠)
    private void LoadAd()
    {
        if (!_isInitialized) return;
        _isAdLoaded = false;
        Advertisement.Load(AD_UNIT_ID, this);
    }

    // IUnityAdsLoadListener
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        _isAdLoaded = true;
        Debug.Log("[AdManager] 광고 로드 완료");
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        _isAdLoaded = false;
        Debug.LogWarning($"[AdManager] 광고 로드 실패: {error} - {message}");
        StartCoroutine(RetryLoadAfterDelay());
    }

    private IEnumerator RetryLoadAfterDelay()
    {
        yield return new WaitForSeconds(AD_RETRY_DELAY);
        LoadAd();
    }

    // ──────────────────────────────────────────────────────────
    /// <summary>
    /// 전면 광고 표시.
    /// 광고가 준비되지 않았거나 실패해도 onClosed 콜백은 반드시 실행됩니다.
    /// </summary>
    /// <param name="onClosed">광고 종료 후(또는 광고 없을 때) 실행할 동작</param>
    public void ShowInterstitialAd(Action onClosed)
    {
        _onAdClosed = onClosed;

        if (_isAdLoaded)
        {
            Advertisement.Show(AD_UNIT_ID, this);
        }
        else
        {
            // 광고 준비 안 됐으면 바로 콜백 실행 (게임 흐름 막지 않음)
            Debug.Log("[AdManager] 광고 미준비 - 콜백 바로 실행");
            _onAdClosed?.Invoke();
            _onAdClosed = null;
        }
    }

    // IUnityAdsShowListener
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState completionState)
    {
        Debug.Log($"[AdManager] 광고 종료: {completionState}");
        _onAdClosed?.Invoke();
        _onAdClosed = null;
        LoadAd(); // 다음 게임오버를 위해 미리 로드
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogWarning($"[AdManager] 광고 표시 실패: {error} - {message}");
        _onAdClosed?.Invoke();
        _onAdClosed = null;
        LoadAd();
    }

    public void OnUnityAdsShowStart(string adUnitId)  { }
    public void OnUnityAdsShowClick(string adUnitId)  { }
}
