#if PLAYFAB
using UnityEngine;
using System.Collections;
using PlayFab;

public class PlayFabFriendsManager {

    public static void Init() {
        PlayFabManager.OnLoginEvent += GetFriends;
        LevelManager.OnMapState += PlaceFriendsPositionsOnMap;
        LevelManager.OnMenuPlay += GetLeadboardOnLevel;
        LevelManager.OnMenuComplete += GetLeadboardOnLevel;

    }


    /// <summary>
    /// Gets the friends list.
    /// </summary>
    public static void GetFriends() {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        PlayFab.ClientModels.GetFriendsListRequest request = new PlayFab.ClientModels.GetFriendsListRequest() {
            IncludeFacebookFriends = true
        };

        PlayFabClientAPI.GetFriendsList(request, (result) => {
            FriendData friend = new FriendData() { FacebookID = PlayFabManager.facebookUserID, PlayFabID = PlayFabManager.PlayFabId, picture = InitScript.profilePic };
            FacebookManager.FriendsPlayFab.Add(friend);

            foreach (var item in result.Friends) {
                friend = new FriendData() { FacebookID = item.FacebookInfo.FacebookId, PlayFabID = item.FriendPlayFabId };
                FacebookManager.FriendsPlayFab.Add(friend);
                FacebookManager.THIS.GetFriendsPicture();
                PlaceFriendsPositionsOnMap();
                //Debug.Log("    " + item.FacebookInfo.FullName + " == " + item.FriendPlayFabId);
            }


        }, (error) => {
            Debug.Log(error.ErrorDetails);
        });

    }

    /// <summary>
    /// Place the friends on map.
    /// </summary>
    public static void PlaceFriendsPositionsOnMap() {
        if (!PlayFabManager.THIS.IsLoggedIn)
            return;

        PlayFab.ClientModels.GetFriendLeaderboardRequest request = new PlayFab.ClientModels.GetFriendLeaderboardRequest() {
            StatisticName = "Level",
            IncludeFacebookFriends = true
        };

        PlayFabClientAPI.GetFriendLeaderboard(request, (result) => {
            foreach (var item in result.Leaderboard) {
                FriendData friend = FacebookManager.FriendsPlayFab.Find(delegate (FriendData bk) {
                    return bk.PlayFabID == item.PlayFabId && bk.PlayFabID != PlayFabManager.PlayFabId;
                }
                );
                if (friend != null) {
                    friend.level = item.StatValue;
                }
            }
            PlayFabManager.FriendsOnMapLoaded();

        }, (error) => {
            Debug.Log(error.ErrorDetails);
        });
    }

    /// <summary>
    /// Gets the leadboard on level.
    /// </summary>
    public static void GetLeadboardOnLevel() {

        if (!PlayFabManager.THIS.IsLoggedIn)
            return;
        int LevelNumber = PlayerPrefs.GetInt("OpenLevel");

        PlayFab.ClientModels.GetFriendLeaderboardAroundPlayerRequest request = new PlayFab.ClientModels.GetFriendLeaderboardAroundPlayerRequest() {
            StatisticName = "Level_" + LevelNumber,
            MaxResultsCount = 5,
            PlayFabId = PlayFabManager.PlayFabId,
            IncludeFacebookFriends = true
        };

        PlayFabClientAPI.GetFriendLeaderboardAroundPlayer(request, (result) => {
            if (LevelManager.THIS.gameStatus == GameState.Map)
                PlayFabManager.leadboardList.Clear();
            foreach (var item in result.Leaderboard) {
                LeadboardPlayerData pl = new LeadboardPlayerData();
                pl.Name = item.DisplayName;
                pl.PlayFabId = item.PlayFabId;
                pl.position = item.Position;
                pl.score = item.StatValue;
                FriendData friend = FacebookManager.FriendsPlayFab.Find(delegate (FriendData bk) {
                    return bk.PlayFabID == item.PlayFabId;
                }
                );
                if (friend != null) {
                    pl.picture = friend.picture;
                }

                LeadboardPlayerData leadboardPlayerData = PlayFabManager.leadboardList.Find(delegate (LeadboardPlayerData bk) {
                    return bk.PlayFabId == item.PlayFabId;
                }
                );
                if (leadboardPlayerData != null)
                    leadboardPlayerData = pl;
                else
                    PlayFabManager.leadboardList.Add(pl);
                //Debug.Log(item.DisplayName + " " + item.PlayFabId + " " + item.Position + " " + item.StatValue);
            }

            if (PlayFabManager.leadboardList.Count > 0) {
                PlayFabManager.LevelLeadboardLoaded();
            }

        }, (error) => {
            Debug.Log(error.ErrorDetails);
        });
    }

}

#endif