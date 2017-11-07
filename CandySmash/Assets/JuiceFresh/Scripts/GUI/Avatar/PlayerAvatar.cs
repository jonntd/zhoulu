using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerAvatar : MonoBehaviour, IAvatarLoader {
    public Image image;

    void Start() {
        image.enabled = false;
    }

#if PLAYFAB
    void OnEnable() {
        PlayFabManager.OnPlayerPictureLoaded += ShowPicture;
    }

    void OnDisable() {
        PlayFabManager.OnPlayerPictureLoaded -= ShowPicture;
    }


#endif
    public void ShowPicture() {
        image.sprite = InitScript.profilePic;
        image.enabled = true;
    }

}
