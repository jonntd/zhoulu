using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class BAD : MonoBehaviour
{
#if UNITY_ANDROID
    string adUnitId = "ca-app-pub-4966484504552142/9744139684";
#elif UNITY_IPHONE
    string adUnitId = "ca-app-pub-4966484504552142/8220919425";
#else
    string adUnitId = "unexpected_platform";
#endif

    public GameObject BUI;
    private BannerView bannerView;

    void Start()
    {
        float num = (float)Screen.height / Screen.width;

        int num2 = PlayerPrefs.GetInt("AdmobInterstitial",0);

        if (num > 1.70f && num2 == 0)
        {
            UIChange();
            Showbanner();
        }
    }

    private void Showbanner()
    {

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }

    private void UIChange()
    {
        //BUI.transform.position = new Vector3(0,0,0);
        BUI.transform.Translate(new Vector3(0, 1.15f, 0));
    }
}