using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum BoostType
{
    ExtraMoves = 1,
    Stripes = 2,
    ExtraTime = 3,
    Bomb = 4,
    Colorful_bomb = 5,
    Shovel = 6,
    Energy = 7,
    None
}

public class BoostTypeDes
{
    /*public static string GetBoosType(BoostType type)
    {
        if (type == BoostType.ExtraMoves)
        {
            return "ExtraMoves_移动步数";
        }
    }*/
}

public class BoostShop : MonoBehaviour
{
    public Sprite[] icons;
    public string[] descriptions;
    public int[] prices;
    public Image icon;
    public Text description;

    BoostType boostType;

    public List<BoostProduct> boostProducts = new List<BoostProduct>();

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
    }

    // Update is called once per frame
    public void SetBoost(BoostType _boostType)
    {
        Debug.Log("_boostType:" + _boostType);
        boostType = _boostType;
        gameObject.SetActive(true);
        icon.sprite = boostProducts[(int)_boostType - 1].icon;
        description.text = boostProducts[(int)_boostType - 1].description;
        for (int i = 0; i < 3; i++)
        {
            transform.Find("Image/BuyBoost" + (i + 1) + "/Count").GetComponent<Text>().text = "x" + boostProducts[(int)_boostType-1].count[i];
            transform.Find("Image/BuyBoost" + (i + 1) + "/Price").GetComponent<Text>().text = "" + boostProducts[(int)_boostType-1].GemPrices[i];
        }
    }

    public void BuyBoost(GameObject button)
    {
        int count = int.Parse(button.transform.Find("Count").GetComponent<Text>().text.Replace("x", ""));
        int price = int.Parse(button.transform.Find("Price").GetComponent<Text>().text);
        GetComponent<AnimationManager>().BuyBoost(boostType, price, count);
    }
}

[System.Serializable]
public class BoostProduct
{
    public Sprite icon;
    public string description;
    public int[] count;
    public int[] GemPrices;
}