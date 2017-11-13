using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LeadboardManager : MonoBehaviour {
    public GameObject playerIconPrefab;
    List<LeadboardObject> playerIconsList = new List<LeadboardObject>();

    void OnEnable() {
        GetComponent<Image>().enabled = false;
#if PLAYFAB
        if (PlayFabManager.THIS.IsLoggedIn)
            GetComponent<Image>().enabled = true;
        //PlayFabManager.OnLevelLeadboardLoaded += ShowLeadboard;
        StartCoroutine(WaitForLeadboard());
#endif
    }

    void OnDisable() {
#if PLAYFAB
        //PlayFabManager.OnLevelLeadboardLoaded -= ShowLeadboard;
#endif
        ResetLeadboard();
    }

    void ResetLeadboard() {
        transform.position = new Vector3(0, -40f, 0);
        foreach (LeadboardObject item in playerIconsList) {
            Destroy(item.gameObject);
        }
        playerIconsList.Clear();
    }

#if PLAYFAB
    IEnumerator WaitForLeadboard() {
        yield return new WaitUntil(() => PlayFabManager.leadboardList.Count > 0);
        ShowLeadboard();
    }

    void ShowLeadboard() {
        GetComponent<Animation>().Play();
        Vector2 leftPosition = new Vector2(-378f, -41f);
        float width = 200;
        PlayFabManager.leadboardList.Sort(CompareByScore);
        int i = 0;
        foreach (var item in PlayFabManager.leadboardList) {
            if (item.score <= 0)
                continue;
            GameObject gm = Instantiate(playerIconPrefab) as GameObject;
            LeadboardObject lo = gm.GetComponent<LeadboardObject>();
            item.position = i + 1;
            lo.PlayerData = item;
            playerIconsList.Add(lo);
            gm.transform.SetParent(transform);
            gm.transform.localScale = Vector3.one;
            gm.GetComponent<RectTransform>().anchoredPosition = leftPosition + Vector2.right * (width * i);
            i++;
        }
    }


    private int CompareByScore(LeadboardPlayerData x, LeadboardPlayerData y) {
        int retval = y.score.CompareTo(x.score);

        if (retval != 0) {
            return retval;
        }
        else {
            return y.score.CompareTo(x.score);
        }
    }
#endif
}
