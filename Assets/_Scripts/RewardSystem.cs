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
        "応援、ありがとう。",
        "励みになります。",
        "感謝しています。",
        "次回作にご期待ください。"
    };

    // テスト用広告ユニットID
    private string adId = "ca-app-pub-3940256099942544/5224354917";
    private bool rewardedAdRetry = false;

    // Start is called before the first frame update
    void Start() {
    // Initialize the Google Mobile Ads SDK.
    MobileAds.Initialize(initStatus => { });
    LoadRewardAd();
    }

    //isRewarded=trueならShowRewardResult()を実行
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

    //リワード広告を見た報酬としてメッセージを表示
    public void ShowRewardResult() {
        messegeText.text = messege[count];
        count++;
        if (count == 4) {
        count = 0;
        }
    }

    //ボタンクリックに登録
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

    //動画の視聴が完了したら実行される（途中で閉じられた場合は呼ばれない）
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
