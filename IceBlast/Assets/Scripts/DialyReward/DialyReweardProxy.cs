using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class DialyReweardProxy
{
    public int[] months = new int[] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    public static DialyReweardProxy Instance = new DialyReweardProxy();
    public int _last_day;               // 上一次登录的月
    public int _last_month;             // 上一个登录的日
    public int _last_reward_day;        // 上一个拿奖励的日期

    public int _curr_day;
    public int _curr_month;

    public DialyReweardProxy()
    {
        System.DateTime now = System.DateTime.Now;
        _curr_day = now.Day;
        _curr_month = now.Month;
        _find_last_time();
        _find_last_day();

        _init_dialy_reward();
    }

    // 今天是否有奖励
    public E_Reward HasRewardDay()
    {
        if (_last_reward_day == 0) return E_Reward.has_reward;
        if (_last_month == _curr_month && _curr_day == _last_day) return E_Reward.no_reward;
        if (_last_month == _curr_month && (_curr_day == _last_day + 1)) return E_Reward.has_reward;
        if (IsNextDay()) return E_Reward.has_reward;
        return E_Reward.no_reward;
        /*for (int i = 1; i < 8; i++)
        {
            E_Reward reward = RewardState(i);
            if (reward == E_Reward.has_reward)
            {
                Debug.Log("day:" + i);
                return E_Reward.has_reward;
            }
        }
        return E_Reward.no_reward;*/
    }

    // 已经那到第几天的奖励了
    public int AlreadyGetWard()
    {
        if (_last_reward_day >= 7) return 7;
        return _last_reward_day;
    }



    public E_Reward RewardState(int day)
    {

        if (_last_reward_day == 0 && day == 1) return E_Reward.has_reward;
        if (day <= _last_reward_day)
        {
            return E_Reward.already_reward;
        }
        else if (day == _last_reward_day + 1)
        {
            if ((_last_day + 1) == _curr_day && _last_month == _curr_month)
            {
                return E_Reward.has_reward;
            }
            else if (IsNextDay())
            {
                return E_Reward.has_reward;
            }
        }
        else if (day > 7 && _last_reward_day == 7)
        {
            if ((_last_day + 1) == _curr_day && _last_month == _curr_month)
            {
                return E_Reward.has_reward;
            }
            else if (IsNextDay())
            {
                return E_Reward.has_reward;
            }
        }
        return E_Reward.no_reward;
    }

    public void GetReward()
    {
        int tmp_reward_day = (_last_reward_day + 1);
        if (tmp_reward_day >= 7)
        {
            tmp_reward_day = 7;
        }
        PlayerManager.Instance.Save(PlayerManager.DAILY_REWARD_TIME, _curr_month + "_" + _curr_day);
        PlayerManager.Instance.Save(PlayerManager.DAILY_REWARD_DAY, tmp_reward_day + "");
        _last_reward_day = tmp_reward_day;
        _last_day = _curr_day;
        _last_month = _curr_month;
    }

    // 查询上一次登录的日期
    private void _find_last_time()
    {
        string time = PlayerManager.Instance.GetValue(PlayerManager.DAILY_REWARD_TIME);
        // 之前没有登录过
        if (string.IsNullOrEmpty(time)) return;
        string[] contents = time.Split('_');
        if (contents.Length != 2) return;

        // 之前登录过
        _last_month = int.Parse(contents[0]);
        _last_day = int.Parse(contents[1]);
    }

    // 查询上一次奖励的日期
    private void _find_last_day()
    {
        string str_day = PlayerManager.Instance.GetValue(PlayerManager.DAILY_REWARD_DAY);
        _last_reward_day = 0;
        int.TryParse(str_day, out _last_reward_day);
    }

    public void _init_dialy_reward()
    {
        if (_last_month == _curr_month)
        {
            if (_last_day + 1 == _curr_day)
            {

            }
            else if (_last_day == _curr_day)
            {

            }
            else
            {
                PlayerManager.Instance.Clear(PlayerManager.DAILY_REWARD_TIME);
                PlayerManager.Instance.Clear(PlayerManager.DAILY_REWARD_DAY);
                _last_day = 0;
                _last_reward_day = 0;
                _last_month = 0;
            }
        }
        else
        {
            PlayerManager.Instance.Clear(PlayerManager.DAILY_REWARD_TIME);
            PlayerManager.Instance.Clear(PlayerManager.DAILY_REWARD_DAY);
            _last_day = 0;
            _last_reward_day = 0;
            _last_month = 0;
        }
    }


    public bool IsNextDay()
    {

        if (_last_month + 1 == _curr_month)
        {
            if (months[_last_month] == _last_day && _curr_day == 1)
                return true;
        }
        else if (_last_month == 12 && _curr_month == 1)
        {
            if (months[_last_month] == _last_day && _curr_day == 1)
                return true;
        }
        return false;
    }

}

public enum E_Reward
{
    no_reward,
    has_reward,
    already_reward,
}