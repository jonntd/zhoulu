using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanelDailyReward : MonoBehaviour
{
    public List<int> rewards;
    public List<DailyRewardItem> items;
    public DailyRewardItem reward_8;
    public Button btn_close;
    void Awake()
    {
        btn_close.onClick.AddListener(on_close);
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetInfo(rewards[i]);
        }
        reward_8.SetInfo(rewards[6]);
    }

    public void on_close()
    {
        gameObject.SetActive(false);
    }

}
