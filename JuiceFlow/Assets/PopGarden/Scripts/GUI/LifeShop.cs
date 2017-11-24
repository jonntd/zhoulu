using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeShop : MonoBehaviour
{
    public int CostIfRefill = 250;
    public int five_live = 250;
    // Use this for initialization
    void OnEnable()
    {
        int tmp_life = InitScript.lifes;
        CostIfRefill = (five_live) / 5 * (5 - tmp_life);
        transform.Find("Image/BuyLife/Price").GetComponent<Text>().text = "" + CostIfRefill;
        if (!LevelManager.THIS.enableInApps)
            transform.Find("Image/BuyLife").gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
