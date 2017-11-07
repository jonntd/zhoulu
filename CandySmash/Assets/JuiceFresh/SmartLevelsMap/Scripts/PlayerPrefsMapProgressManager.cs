
using UnityEngine;

public class PlayerPrefsMapProgressManager : IMapProgressManager
{
    private string GetLevelKey(int number)
    {
        string result = "Level.{0:000}.StarsCount";
        return string.Format(result, number);
    }

    public int LoadLevelStarsCount(int level)
    {
        string key = GetLevelKey(level);
        int result = PlayerPrefs.GetInt(key, 0);
        return result;
    }

    public void SaveLevelStarsCount(int level, int starsCount)
    {
        string key = GetLevelKey(level);
        int value = starsCount;
        PlayerPrefs.SetInt(key, value);
    }

    public void ClearLevelProgress(int level)
    {
        string key = GetLevelKey(level);
        PlayerPrefs.DeleteKey(key);
    }
}
