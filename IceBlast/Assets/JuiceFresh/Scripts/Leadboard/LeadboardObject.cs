using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeadboardObject : MonoBehaviour {
    public Image icon;
    public Text place;
    public Text playerName;
    public Text score;
#if PLAYFAB
    private LeadboardPlayerData playerData;

    public LeadboardPlayerData PlayerData {
        get {
            return playerData;
        }

        set {
            playerData = value;
            SetupIcon();
        }
    }

    void SetupIcon() {
        icon.sprite = PlayerData.picture;
        place.text = "" + PlayerData.position;
        playerName.text = PlayerData.Name;
        if (PlayFabManager.THIS.IsYou(PlayerData.PlayFabId)) {
            playerName.text = "YOU";
            playerName.color = Color.red;
        }
        score.text = "" + PlayerData.score;
    }

#endif

}
