#if PLAYFAB
using UnityEngine;
using System.Collections;
using PlayFab.ClientModels;
using PlayFab;
using PlayFab.AdminModels;
using System.Collections.Generic;

public class PlayFabManager : MonoBehaviour {
    public delegate void PlayFabEvents();
    public static event PlayFabEvents OnLoginEvent;
    public static event PlayFabEvents OnFriendsOnMapLoaded;
    public static event PlayFabEvents OnPlayerPictureLoaded;
    public static event PlayFabEvents OnLevelLeadboardLoaded;

    public static PlayFabManager THIS;
    [HideInInspector]
    public static string PlayFabId;
    public string titleId;
    public string DeveloperSecretKey;
    private bool isLoggedIn;

    public bool IsLoggedIn {
        get {
            return isLoggedIn;
        }

        set {
            isLoggedIn = value;
            if (value && OnLoginEvent != null)
                OnLoginEvent();
        }
    }

    public static List<LeadboardPlayerData> leadboardList = new List<LeadboardPlayerData>();
    public static string facebookUserID;

    // Use this for initialization
    void Start() {
        THIS = this;
        PlayFabSettings.TitleId = titleId;
        PlayFabSettings.DeveloperSecretKey = DeveloperSecretKey;
        PlayFabCurrencyManager.Init();
        PlayFabFriendsManager.Init();
        PlayFabDataManager.Init();
        //Login(titleId);
    }

    #region AUTHORIZATION
    public void LoginWithFB(string accessToken) {
        LoginWithFacebookRequest request = new LoginWithFacebookRequest() {
            TitleId = titleId,
            CreateAccount = true,
            AccessToken = accessToken
            //  CustomId = SystemInfo.deviceUniqueIdentifier
        };

        PlayFabClientAPI.LoginWithFacebook(request, (result) => {
            PlayFabId = result.PlayFabId;
            Debug.Log("Got PlayFabID: " + PlayFabId);

            if (result.NewlyCreated) {
                Debug.Log("(new account)");
            }
            else {
                Debug.Log("(existing account)");
            }
            IsLoggedIn = true;
        },
            (error) => {
                Debug.Log(error.ErrorMessage);
            });
    }


    void Login(string titleId) {
        LoginWithCustomIDRequest request = new LoginWithCustomIDRequest() {
            TitleId = titleId,
            CreateAccount = true,
            CustomId = SystemInfo.deviceUniqueIdentifier
        };

        PlayFabClientAPI.LoginWithCustomID(request, (result) => {
            PlayFabId = result.PlayFabId;
            Debug.Log("Got PlayFabID: " + PlayFabId);

            if (result.NewlyCreated) {
                Debug.Log("(new account)");
            }
            else {
                Debug.Log("(existing account)");
            }
            IsLoggedIn = true;
        },
        (error) => {
            Debug.Log(error.ErrorMessage);
        });
    }

    public void UpdateName(string userName) {
        PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest request = new PlayFab.ClientModels.UpdateUserTitleDisplayNameRequest() {
            DisplayName = userName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, (result) => {
        },
        (error) => {
            Debug.Log(error.ErrorMessage);
        });

    }

    public bool IsYou(string playFabId) {
        if (playFabId == PlayFabId)
            return true;
        return false;
    }


    #endregion

    #region EVENTS

    public static void LevelLeadboardLoaded() {
        //OnLevelLeadboardLoaded();
    }

    public static void PlayerPictureLoaded() {
        OnPlayerPictureLoaded();
    }

    public static void FriendsOnMapLoaded() {
        OnFriendsOnMapLoaded();
    }
    #endregion
}

public class LeadboardPlayerData {
    public string Name;
    public string PlayFabId;
    public int position;
    public int score;
    public Sprite picture;
}

#endif