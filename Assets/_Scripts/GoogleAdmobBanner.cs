using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAdmobBanner : MonoBehaviour {
    private BannerView bannerView;
    public void Start() {
        // Google AdMob Initial
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
    }
    private void RequestBanner() {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // �e�X�g�p�L�����j�b�gID
#elif UNITY_IPHONE
    string adUnitId = "ca-app-pub-7689051089863147/2788662322"; // �e�X�g�p�L�����j�b�gID
#else
    string adUnitId = "unexpected_platform";
#endif
        // Create a 320x50 banner at the bottom of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }
}