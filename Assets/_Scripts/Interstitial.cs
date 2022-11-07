using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using GoogleMobileAds.Api;

public class Interstitial : MonoBehaviour {
  private InterstitialAd interstitial;
    public void loadInterstitialAd() {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdFailedToLoad += (sender, args) =>
        {
            Debug.Log("loadInterstitialAd.OnAdFailedToLoad");
        };
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.interstitial.LoadAd(request);
    }
    private void HandleOnAdClosed(object sender, EventArgs args) {
        this.interstitial.Destroy();
        this.loadInterstitialAd();
    }
    public void showInterstitialAd() {
        if (this.interstitial.IsLoaded()) {
            this.interstitial.Show();
        } else {
            Debug.Log("Interstitial Ad not load");
        }
    }
}
