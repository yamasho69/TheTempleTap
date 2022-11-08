using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using UnityEngine;

public class RewardSystem : MonoBehaviour {
    private RewardedAd rewardedAd = null;

    public Text messegeText;
    int count = 0;
    bool isRewarded;
    [SerializeField] GameObject messegePanel;
    string[] messege = {
        "�����A���肪�Ƃ��B",
        "��݂ɂȂ�܂��B",
        "���ӂ��Ă��܂��B",
        "�����ɂ����҂��������B"
    };

    // �e�X�g�p�L�����j�b�gID
    private string adId = "ca-app-pub-3940256099942544/5224354917";
    private bool rewardedAdRetry = false;

    // Start is called before the first frame update
    void Start() {
    // Initialize the Google Mobile Ads SDK.
    MobileAds.Initialize(initStatus => { });
    LoadRewardAd();
    }

    //isRewarded=true�Ȃ�ShowRewardResult()�����s
    void Update() {
        if (isRewarded) {
        isRewarded = false;
        ShowRewardResult();
        }
    if (rewardedAdRetry) {
        LoadRewardAd();
        rewardedAdRetry = false;
        }
    }

    //�����[�h�L����������V�Ƃ��ă��b�Z�[�W��\��
    public void ShowRewardResult() {
        messegeText.text = messege[count];
        count++;
        if (count == 4) {
        count = 0;
        }
    }

    //�{�^���N���b�N�ɓo�^
    public void UserChoseToWatchAd() {
        if (this.rewardedAd.IsLoaded()) {
            this.rewardedAd.Show();
        }
    }

    void LoadRewardAd() {
        // Clean up banner ad before creating a new one.
        if (rewardedAd != null) {
        rewardedAd = null;
        }

        rewardedAd = new RewardedAd(adId);
        // Register for ad events.
        rewardedAd.OnAdLoaded += HandleRewardAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardAdFailedToLoad;
        rewardedAd.OnAdOpening += HandleRewardedAdAdOpened;
        rewardedAd.OnAdClosed += HandleRewardedAdAdClosed;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;

        AdRequest adRequest = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(adRequest);
    }
    public void HandleRewardAdLoaded(object sender, EventArgs args) {
        Debug.Log("HandleRewardAdLoaded event received with message: " + args);
        rewardedAdRetry = false;
    }

    public void HandleRewardAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        LoadAdError loadAdError = args.LoadAdError;
        int code = loadAdError.GetCode();
        string message = loadAdError.GetMessage();
        Debug.Log("Load error string: " + loadAdError.ToString());
        Debug.Log("code: " + code.ToString());
        MonoBehaviour.print(
        "HandleRewardedAdFailedToLoad event received with message: "
                            + message);
        if (code == 2 || code == 0) {
        Debug.Log("error");
        } else {
        Debug.Log("error no fill");
        }
        rewardedAdRetry = true;
    }
    public void HandleRewardedAdAdOpened(object sender, EventArgs args) {
        Debug.Log("HandleRewardedAdAdOpened event received");
    }
    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args) {
        MonoBehaviour.print(
        "HandleRewardedAdFailedToShow event received with message: "
                            + args.AdError.GetMessage());
    }

    //����̎�����������������s�����i�r���ŕ���ꂽ�ꍇ�͌Ă΂�Ȃ��j
    public void HandleUserEarnedReward(object sender, Reward args) {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
        "HandleRewardedAdRewarded event received for "
                    + amount.ToString() + " " + type);
        isRewarded = true;
    }
    public void HandleRewardedAdAdClosed(object sender, EventArgs args) {
        Debug.Log("HandleRewardedAdClosed event received");
        rewardedAdRetry = true;
    }
}
