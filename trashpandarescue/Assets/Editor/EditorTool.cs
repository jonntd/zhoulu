using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorTool
{

    [MenuItem("工具/生命=5")]
    public static void AddLife()
    {

        PlayerPrefs.SetInt("Lifes", 5);
    }

    [MenuItem("工具/金币=10000")]
    public static void AddGem()
    {
        PlayerPrefs.SetInt("Gems", 10000);
    }

}
