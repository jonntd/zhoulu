using UnityEngine;
using System.Collections;


[System.Serializable]
public class CollectedIngredients
{
    public string name;
    public Sprite sprite;
    public bool check;
    public int count;

    public void Set(CollectedIngredientsConfig config)
    {
        check = config.check;
        count = config.count;
    }
}

public class CollectedIngredientsConfig
{
    public bool check;
    public int count;
}
