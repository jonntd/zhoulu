using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DailyRewardItem : MonoBehaviour, IPointerClickHandler
{
    public int reward_num;
    public Text reward_lab;
    public GameObject get_reward;
    public bool _result;
    public int day;

    public void SetInfo(int reward)
    {
        reward_num = reward;
        reward_lab.text = "X " + reward;

        int get_ward_day = DialyReweardProxy.Instance.AlreadyGetWard();
        E_Reward state = DialyReweardProxy.Instance.RewardState(day);
        SetGetRweward(state);
        if (day == 7 && state == E_Reward.already_reward)
            gameObject.SetActive(false);
        if (day == 8)
        {
            if (get_ward_day == 7)
                gameObject.SetActive(true);
            else
            {

                gameObject.SetActive(false);
            }
        }
    }

    private void SetGetRweward(E_Reward state)
    {
        if (state == E_Reward.already_reward)
        {
            _result = true;
            get_reward.SetActive(_result);
        }
        else if (state == E_Reward.has_reward)
        {
            _result = false;
            get_reward.SetActive(false);
        }
        else
        {
            _result = false;
            get_reward.SetActive(false);
        }


    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_result) return;
        E_Reward state = DialyReweardProxy.Instance.RewardState(day);

        if (state == E_Reward.has_reward)
        {
            SetGetRweward(E_Reward.already_reward);
            DialyReweardProxy.Instance.GetReward();
            InitScript.Instance.AddGems(reward_num);
        }

    }
}
