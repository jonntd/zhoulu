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
    public Button btn_reward;
    void Awake()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetInfo(rewards[i]);
        }
        reward_8.SetInfo(rewards[6]);

        btn_reward.onClick.AddListener(on_get_reward);
    }

    public void on_get_reward()
    {
        int day = DialyReweardProxy.Instance._last_reward_day;
        if (day < 0)
        {
            items[0].OnPointerClick(null);
        }
            
        else if (day >= 0 && day <= 6)
        {
            items[day].OnPointerClick(null);
        }
        else
        {
            day = 8;
            reward_8.OnPointerClick(null);
        }
        gameObject.SetActive(false);
    }
}
