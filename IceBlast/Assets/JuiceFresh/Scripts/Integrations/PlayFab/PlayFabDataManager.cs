#if PLAYFAB
using UnityEngine;
using System.Collections;
using PlayFab.ClientModels;
using System.Collections.Generic;
using PlayFab;

public class PlayFabDataManager {
    static int LatestReachedLevel = 0;
    static int LevelScoreCurrentRecord = 0;

    public static void Init() {
        PlayFabManager.OnLoginEvent += GetPlayerLevel;
        PlayFabManager.OnLoginEvent += GetStars;
        LevelManager.OnEnterGame += GetPlayerScore;
    }


    public static void SetData(Dictionary<string, string> dic) {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        UpdateUserDataRequest request = new UpdateUserDataRequest() {
            Data = dic
        };

        PlayFabClientAPI.UpdateUserData(request, (result) => {
            Debug.Log("Successfully updated user data");
        }, (error) => {
            Debug.Log(error.ErrorDetails);
        });

    }


    #region SCORE
    public static void SetPlayerScore(int level, int score) {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;
        if (score <= LevelScoreCurrentRecord)
            return;

        UpdatePlayerScoreFoLeadboard(score);

        List<StatisticUpdate> stUpdateList = new List<StatisticUpdate>();
        StatisticUpdate stUpd = new StatisticUpdate();
        stUpd.StatisticName = "Level_" + level;
        stUpd.Value = score;
        stUpdateList.Add(stUpd);

        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest() {
            Statistics = stUpdateList
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, (result) => {
            Debug.Log("Successfully updated user score");
        }, (error) => {
            Debug.Log(error.ErrorDetails);
        });
    }

    public static void GetPlayerScore() {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest() {
            StatisticNames = new List<string>() { "Level_" + LevelManager.THIS.currentLevel }
        };

        PlayFabClientAPI.GetPlayerStatistics(request, (result) => {
            if ((result.Statistics == null)) {
                Debug.Log("No user data available");
            }
            else {
                foreach (var item in result.Statistics) {
                    if (item.StatisticName == "Level_" + LevelManager.THIS.currentLevel)
                        LevelScoreCurrentRecord = item.Value;
                    //Debug.Log("    " + item.StatisticName + " == " + item.Value);
                }
            }
        }, (error) => {
            Debug.Log(error.ErrorDetails);
        });


    }

    static void UpdatePlayerScoreFoLeadboard(int score) {
        LeadboardPlayerData leadboardPlayerData = PlayFabManager.leadboardList.Find(delegate (LeadboardPlayerData bk) {
            return bk.PlayFabId == PlayFabManager.PlayFabId;
        }
        );
        if (leadboardPlayerData != null)
            leadboardPlayerData.score = score;
    }


    #endregion


    #region LEVEL
    public static void SetPlayerLevel(int level) {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        if (level <= LatestReachedLevel)
            return;

        List<StatisticUpdate> stUpdateList = new List<StatisticUpdate>();
        StatisticUpdate stUpd = new StatisticUpdate();
        stUpd.StatisticName = "Level";
        stUpd.Value = level;
        stUpdateList.Add(stUpd);

        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest() {
            Statistics = stUpdateList
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, (result) => {
            Debug.Log("Successfully updated user level");
        }, (error) => {
            Debug.Log(error.ErrorDetails);
        });
    }

    public static void GetPlayerLevel() {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest() {
            StatisticNames = new List<string>() { "Level" }
        };

        PlayFabClientAPI.GetPlayerStatistics(request, (result) => {
            if ((result.Statistics == null)) {
                Debug.Log("No user data available");
            }
            else {
                foreach (var item in result.Statistics) {
                    if (item.StatisticName == "Level")
                        LatestReachedLevel = item.Value;
                    //Debug.Log("    " + item.StatisticName + " == " + item.Value);
                }
            }
        }, (error) => {
            Debug.Log(error.ErrorDetails);
        });


    }

    #endregion

    #region STARS
    public static void SetStars() {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        for (int i = 1; i <= LevelManager.THIS.currentLevel; i++) {
            dic.Add("StarsLevel_" + i, "" + PlayerPrefs.GetInt(string.Format("Level.{0:000}.StarsCount", i)));
        }
        SetData(dic);
    }

    public static void GetStars() {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        string PlayFabId = PlayFabManager.PlayFabId;

        GetUserDataRequest request = new GetUserDataRequest() {
            PlayFabId = PlayFabId,
            Keys = null
        };

        PlayFabClientAPI.GetUserData(request, (result) => {
            if ((result.Data == null) || (result.Data.Count == 0)) {
                Debug.Log("No user data available");
            }
            else {
                foreach (var item in result.Data) {
                    if (item.Key.Contains("StarsLevel_")) {
                        PlayerPrefs.SetInt(string.Format("Level.{0:000}.StarsCount", int.Parse(item.Key.Replace("StarsLevel_", ""))), int.Parse(item.Value.Value));
                    }
                }
                PlayerPrefs.Save();

                LevelsMap._instance.Reset();

            }
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.ErrorMessage);
        });
    }

    #endregion

    #region BOOSTS
    public static void SetBoosterData() {
        Dictionary<string, string> dic = new Dictionary<string, string>(){
          {"Boost_"+ (int)BoostType.Bomb,""+ PlayerPrefs.GetInt("" + BoostType.Bomb)},
          {"Boost_"+ (int)BoostType.Colorful_bomb,""+ PlayerPrefs.GetInt("" + BoostType.Colorful_bomb)},
          {"Boost_"+ (int)BoostType.Energy,""+ PlayerPrefs.GetInt("" + BoostType.Energy)},
          {"Boost_"+ (int)BoostType.ExtraMoves,""+ PlayerPrefs.GetInt("" + BoostType.ExtraMoves)},
          {"Boost_"+ (int)BoostType.ExtraTime,""+ PlayerPrefs.GetInt("" + BoostType.ExtraTime)},
          {"Boost_"+ (int)BoostType.Shovel,""+ PlayerPrefs.GetInt("" + BoostType.Shovel)},
          {"Boost_"+ (int)BoostType.Stripes,""+ PlayerPrefs.GetInt("" + BoostType.Stripes)} };
        SetData(dic);
    }


    public static void GetBoosterData() {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        string PlayFabId = PlayFabManager.PlayFabId;

        GetUserDataRequest request = new GetUserDataRequest() {
            PlayFabId = PlayFabId,
            Keys = null
        };

        PlayFabClientAPI.GetUserData(request, (result) => {
            if ((result.Data == null) || (result.Data.Count == 0)) {
                Debug.Log("No user data available");
            }
            else {
                foreach (var item in result.Data) {
                    if (item.Key.Contains("Boost_")) {
                        PlayerPrefs.SetInt("" + (BoostType)int.Parse(item.Key.Replace("Boost_", "")), int.Parse(item.Value.Value));
                        //Debug.Log("    " + item.Key + " == " + item.Value.Value);
                    }
                }
                PlayerPrefs.Save();
            }
        }, (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.ErrorMessage);
        });
    }

    #endregion



}

#endif