using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class RAD : MonoBehaviour
{
    public static RAD RADs;
    private RewardBasedVideoAd rewardBasedVideo;
    private float deltaTime = 0.0f;
    private static string outputMessage = string.Empty;
    public Button RADbtn;
    public int num = 10;

#if UNITY_ANDROID
    string adUnitId = "ca-app-pub-4966484504552142/9836610510";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-4966484504552142/2313343715";
#else
        string adUnitId = "unexpected_platform";
#endif

    void Awake()
    {
        RADs = this;
    }

    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

    private InterstitialAd ad;
    // Use this for initialization
    void Start()
    {
        //Button RADbtn = gameObject.GetComponent<Button>();
        if (RADbtn != null)
            RADbtn.gameObject.SetActive(false);
        num = 20;
        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
        this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
        this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
        this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
        this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
        this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
        this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;

        CreateAdRequest();
        this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), adUnitId);
    }

    // Update is called once per frame
    public void Update()
    {
        // Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
        // value.
        this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
    }

    private void btnshowctrl()
    {
        RADbtn.gameObject.SetActive(true);
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .Build();
    }

    public void ShowRewardBasedVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
            if (RADbtn != null)
                RADbtn.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Reward based video ad is not ready yet");
        }
    }

    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
        btnshowctrl();
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        StatisticsManager.UseLevel();
        if (StatisticsManager.enter_level <= 0)
            StatisticsManager.enter_level = 0;
        InitScript.Instance.AddGems(100);
        /*
        MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
        */

        //BAD.BADs.OnDestroy();
        //PlayerPrefs.SetInt("AdsNum", 10);
        //PlayerPrefs.Save();
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    #endregion
}
