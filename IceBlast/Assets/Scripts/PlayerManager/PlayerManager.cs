using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{

    public static PlayerManager Instance = new PlayerManager();

    public const string DAILY_REWARD_TIME = "daily_reward_time";
    public const string DAILY_REWARD_DAY = "daily_reward_day";

    public void Save(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();

    }

    public string GetValue(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public void Clear(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }

    public void ClearAll(string key)
    {
        PlayerPrefs.DeleteAll();
    }
}
