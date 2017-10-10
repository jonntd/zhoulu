
using UnityEngine;

public class PlayerPrefsMapProgressManager : IMapProgressManager
{
    private string GetLevelKey(int number)
    {
        //Debug.Log("PlayerPrefsMapProgressManager:" + number);
        return string.Format("Level.{0:000}.StarsCount", number);
    }

    public int LoadLevelStarsCount(int level)
    {
        //Debug.Log("LoadLevelStarsCount:" + level);
        return PlayerPrefs.GetInt(GetLevelKey(level), 0);
    }

    public void SaveLevelStarsCount(int level, int starsCount)
    {
        //Debug.Log("SaveLevelStarsCount:" + level + "starsCount:" + starsCount);
        PlayerPrefs.SetInt(GetLevelKey(level), starsCount);
    }

    public void ClearLevelProgress(int level)
    {
        //Debug.Log("ClearLevelProgress:" + level);
        PlayerPrefs.DeleteKey(GetLevelKey(level));
    }
}
