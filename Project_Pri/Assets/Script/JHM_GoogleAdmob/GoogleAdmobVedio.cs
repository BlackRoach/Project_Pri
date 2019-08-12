using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAdmobVedio : MonoBehaviour {

    private readonly string unitId = "ca-app-pub-3940256099942544/5224354917";

    private readonly string testId = "ca-app-pub-3940256099942544/5224354917";

    private readonly string testDeviceId = "33BE2250B43518CCDA7DE426D04EE231";



    private RewardBasedVideoAd rewardVedioAd;

    private void Start()
    {
        RequestRewardVedioAd();
        Invoke("RewardVedioAdShow", 20f);
    }

    private void RequestRewardVedioAd()
    {
        string id = Debug.isDebugBuild ? testId : unitId;

        rewardVedioAd = RewardBasedVideoAd.Instance;


        AdRequest request = new AdRequest.Builder().Build();

        rewardVedioAd.LoadAd(request, id);
    }
    private void RewardVedioAdShow()
    {
        StartCoroutine(RewardVedioAdLoading());
    }
    IEnumerator RewardVedioAdLoading()
    {
        while (!rewardVedioAd.IsLoaded())
        {
            yield return null;
        }

        rewardVedioAd.Show();
    }


} // class









