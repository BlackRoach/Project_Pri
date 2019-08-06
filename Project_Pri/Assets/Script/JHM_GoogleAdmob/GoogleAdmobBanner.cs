using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;


public class GoogleAdmobBanner : MonoBehaviour {

    private readonly string unitId = "ca-app-pub-3940256099942544/6300978111";

    private readonly string testId = "ca-app-pub-3940256099942544/6300978111";

    private readonly string testDeviceId = "33BE2250B43518CCDA7DE426D04EE231";

    private BannerView banner;

    private void Start()
    {
        RequestBanner();
    }

    private void RequestBanner()
    {
        string id = Debug.isDebugBuild ? testId : unitId;

        banner = new BannerView(id, AdSize.SmartBanner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().AddTestDevice(testDeviceId).Build();

        banner.LoadAd(request);
    }
    public void ActivateBanner(bool open)
    {
        if (open)
        {
            banner.Show();
        }else
        {
            banner.Hide();
        }
    }
    public void DestroyBanner()
    {
        banner.Destroy();
    }
} // class




