using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public PanelDailyReward panel_daily_reward;
    public GameObject panel_setting;
    void Awake()
    {
        /* bool result = DialyReweardProxy.Instance.HasRewardDay();
         panel_daily_reward.gameObject.SetActive(result);*/
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowSetting()
    {
        SoundBase.Instance.PlaySound(SoundBase.Instance.click);
        if (!panel_setting.activeSelf)
            panel_setting.SetActive(true);
        else
            panel_setting.SetActive(false);
    }
}
