using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvatarManager : MonoBehaviour {
    public List<GameObject> avatars = new List<GameObject>();

    void Start() {
#if PLAYFAB
        PlayFabManager.OnFriendsOnMapLoaded += CheckFriendsList;

#endif
    }

    //void OnDisable()
    //{
    //    PlayFabManager.OnFriendsOnMapLoaded -= CheckFriendsList;
    //}

    void CheckFriendsList() {
        List<FriendData> FriendsPlayFab = FacebookManager.FriendsPlayFab;

        for (int i = 0; i < FriendsPlayFab.Count; i++) {
            CreateAvatar(FriendsPlayFab[i]);
        }
    }

    /// <summary>
    /// Creates the friend's avatar.
    /// </summary>
    void CreateAvatar(FriendData friendData) {
        GameObject friendAvatar = friendData.avatar;
        if (friendAvatar == null) {
            friendAvatar = Instantiate(Resources.Load("Prefabs/FriendAvatar")) as GameObject;
            avatars.Add(friendAvatar);
            friendData.avatar = friendAvatar;
            friendAvatar.transform.SetParent(transform);
        }
        friendAvatar.GetComponent<FriendAvatar>().FriendData = friendData;
    }

}
