using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAdmobInterstitial : MonoBehaviour {

    private readonly string unitId = "ca-app-pub-3940256099942544/1033173712";

    private readonly string testId = "ca-app-pub-3940256099942544/1033173712";

    private readonly string testDeviceId = "33BE2250B43518CCDA7DE426D04EE231";

    private InterstitialAd interstitial;


    private void Start()
    {
        RequestInterstital();
        Invoke("InterstitalShow",10f);
    }

    private void RequestInterstital()
    {
        string id = Debug.isDebugBuild ? testId : unitId;

        interstitial = new InterstitialAd(id);

        AdRequest request = new AdRequest.Builder().Build();

        interstitial.LoadAd(request);
    }
    private void InterstitalShow()
    {
        StartCoroutine(InterstitalLoading());
    }
    IEnumerator InterstitalLoading()
    {
        while (!interstitial.IsLoaded())
        {
            yield return null;
        }

        interstitial.Show();
    }

} // class





